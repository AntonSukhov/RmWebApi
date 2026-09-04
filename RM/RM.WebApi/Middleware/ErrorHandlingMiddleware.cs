using System;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RM.BLL.Abstractions.Errors;
using RM.Common.Constants;

namespace RM.WebApi.Middleware;

/// <summary>
/// Промежуточное программное обеспечение обработки ошибок.
/// </summary>
/// <remarks>
/// Перехватывает исключения, возникшие в конвейере обработки Http-запроса, и формирует
/// JSON-ответ с описанием ошибки (<see cref="ApiError"/>) и соответствующим HTTP-статусом:
/// <list type="bullet">
/// <item><description>исключения, реализующие <see cref="IApiException"/>, — <c>400 Bad Request</c>;</description></item>
/// <item><description><see cref="DbUpdateConcurrencyException"/> — <c>409 Conflict</c>;</description></item>
/// <item><description>остальные исключения — <c>500 Internal Server Error</c> с обобщённым сообщением.</description></item>
/// </list>
/// Регистрируется в конвейере первым (см. <c>Startup.Configure</c>), чтобы охватить все последующие этапы.
/// </remarks>
public class ErrorHandlingMiddleware : MiddlewareBase
{
    /// <summary>
    /// Опции JSON-сериализации, применяемые при формировании тела ответа с ошибкой.
    /// </summary>
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ErrorHandlingMiddleware"/>.
    /// </summary>
    /// <param name="next">Делегат обработки Http-запроса на следующем этапе конвейера обработки запроса.</param>
    /// <exception cref="ArgumentNullException">Возникает, если <paramref name="next"/> равен <c>null</c>.</exception>
    public ErrorHandlingMiddleware(RequestDelegate next) : base(next)
    {
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
    }

    /// <inheritdoc/>
    public override async Task Invoke(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    /// <summary>
    /// Преобразует возникшее исключение в ошибку API и записывает её в ответ.
    /// </summary>
    /// <param name="context">Контекст Http-запроса.</param>
    /// <param name="exception">Возникшее исключение.</param>
    /// <returns>Задача, представляющая асинхронную обработку исключения.</returns>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        ApiError apiError;
        int statusCode;

        if (exception is IApiException apiException)
        {
            // Бизнес-ошибка:
            apiError = apiException.ToApiError();
            statusCode = (int)HttpStatusCode.BadRequest;
        }
        else if (exception is DbUpdateConcurrencyException)
        {
            // Ошибка "Конфликт параллельного изменения данных":
            apiError = new ApiError
            {
                Code = ErrorCodes.Concurrency,
                Message = "Данные были изменены или удалены другим процессом."
            };
            statusCode = (int)HttpStatusCode.Conflict;
        }
        else
        {
            // Непредвиденная ошибка (клиенту — обобщённое сообщение)
            apiError = new ApiError
            {
                Code = ErrorCodes.Generic,
                Message = "Произошла непредвиденная ошибка при обработке запроса."
            };
            statusCode = (int)HttpStatusCode.InternalServerError;

            // TODO: Добавить логирование полной информации для разработчиков.
        }

        await SetErrorResponseAsync(context, statusCode, apiError);
    }

    /// <summary>
    /// Формирует и записывает в ответ JSON-представление ошибки API.
    /// </summary>
    /// <param name="context">Контекст Http-запроса.</param>
    /// <param name="statusCode">HTTP-статус-код ответа.</param>
    /// <param name="apiError">Ошибка API, сериализуемая в тело ответа.</param>
    /// <returns>Задача, представляющая асинхронную запись ответа.</returns>
    private async Task SetErrorResponseAsync(HttpContext context, int statusCode, ApiError apiError)
    {
        var result = JsonSerializer.Serialize(apiError, _jsonSerializerOptions);
        context.Response.ContentType = HttpConstants.ApplicationJsonContentType;
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(result);
    }
}
