using System;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RM.BLL.Abstractions.Errors;
using RM.Common.Services;

namespace RM.WebApi.Middleware;

/// <summary>
/// Промежуточное программное обеспечение обработки ошибок.
/// </summary>
public class ErrorHandlingMiddleware : MiddlewareBase
{ 
    /// <summary>
    /// Опции JSON-сериализации.
    /// </summary>
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ErrorHandlingMiddleware"/>.
    /// </summary>
    /// <param name="next">Делегат обработки Http-запроса на следующем этапе конвейера обработки запроса.</param>
    /// <exception cref="ArgumentNullException"/>
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
    /// Метод обработки исключения.
    /// </summary>
    /// <param name="context">Контекст Http-запроса.</param>
    /// <param name="exception">Исключение.</param>
    /// <returns/>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        ApiError apiError;
        int statusCode;

        if (exception is IApiException apiException)
        {
            // Бизнес-ошибки:
            apiError = apiException.ToApiError();
            statusCode = (int)HttpStatusCode.BadRequest;
        }
        else if (exception is DbUpdateConcurrencyException)
        {
            // Конфликт параллельного изменения данных:
            apiError = new ApiError
            {
                Code = ErrorCodes.Concurrency,
                Message = "Данные были изменены или удалены другим процессом."
            };
            statusCode = (int)HttpStatusCode.Conflict;
        }
        else
        {
            // Непредвиденные внутренние ошибки:
            apiError = new ApiError
            {
                Code = ErrorCodes.Generic,
                Message = "Внутренняя ошибка сервера.",
                Details = [
                    $"Тип ошибки: {exception.GetType().Name}",
                    $"Трассировка: {exception.StackTrace}"
                ]
            };
            statusCode = (int)HttpStatusCode.InternalServerError;
        }

        await SetErrorResponseAsync(context, statusCode, apiError);
    }

    /// <summary>
    /// Устанавливает свойства ответа, которые соответствуют возникшей ошибке.
    /// </summary>
    /// <param name="context">Контекст Http-запроса.</param>
    /// <param name="statusCode">Сообщение об ошибке.</param>
    /// <param name="apiError"></param>
    /// <returns/>
    private async Task SetErrorResponseAsync(HttpContext context, int statusCode, ApiError apiError)
    {
        var result = JsonSerializer.Serialize(apiError, _jsonSerializerOptions);
        context.Response.ContentType = Constants.ApplicationJsonContentType;
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(result);
    }
}
