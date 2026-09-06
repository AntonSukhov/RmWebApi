using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RM.BLL.Abstractions.Errors;
using RM.BLL.Exceptions;
using RM.Common.Constants;

namespace RM.WebApi.Middleware;

/// <summary>
/// Промежуточное программное обеспечение обработки ошибок.
/// </summary>
/// <remarks>
/// Перехватывает исключения, возникшие в конвейере обработки HTTP-запроса, и формирует
/// ответ в формате <see cref="ProblemDetails"/> (RFC 7807/9457) с соответствующим HTTP-статусом:
/// <list type="bullet">
/// <item><description><see cref="ValidationException"/> и <see cref="ValidationAggregationException"/> — <c>422 Unprocessable Entity</c>;</description></item>
/// <item><description><see cref="DataNotFoundException"/> — <c>404 Not Found</c>;</description></item>
/// <item><description><see cref="ConflictException"/> и <see cref="DbUpdateConcurrencyException"/> — <c>409 Conflict</c>;</description></item>
/// <item><description>остальные <see cref="IApiException"/> — <c>400 Bad Request</c>;</description></item>
/// <item><description>непредвиденные исключения — <c>500 Internal Server Error</c> с обобщённым сообщением.</description></item>
/// </list>
/// Регистрируется в конвейере первым, чтобы охватить все последующие этапы.
/// </remarks>
public class ErrorHandlingMiddleware : MiddlewareBase
{
    private const string Code = "code";

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

        var mappingResult = MapException(exception, context);

        await SetErrorResponseAsync(context, mappingResult.StatusCode, mappingResult.ProblemDetails);
    }

    /// <summary>
    /// Сопоставляет тип исключения с HTTP-статусом и описанием ошибки <see cref="ProblemDetails"/>.
    /// </summary>
    /// <param name="exception">Возникшее исключение.</param>
    /// <param name="context">Контекст HTTP-запроса.</param>
    /// <returns>Кортеж с HTTP-статусом и описанием ошибки.</returns>
    private static (int StatusCode, ProblemDetails ProblemDetails) MapException(
        Exception exception, 
        HttpContext context)
    {
        int statusCode;
        ProblemDetails problemDetails;

        switch (exception)
        {
            case ValidationAggregationException vae:
                statusCode = (int)HttpStatusCode.UnprocessableEntity;
                
                var vaeErrors = vae.InnerValidationExceptions
                    .GroupBy(v => v.FieldName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(v => v.Message).ToArray());

                problemDetails = new ValidationProblemDetails(vaeErrors)
                {
                    Status = statusCode,
                    Title = vae.Message,
                    Instance = context.Request.Path
                };

                problemDetails.Extensions[Code] = vae.Code; 
                break;

            case ValidationException ve:
                statusCode = (int)HttpStatusCode.UnprocessableEntity;

                var veErrors = new Dictionary<string, string[]>
                {
                    { ve.FieldName, new[] { ve.Message } }
                };
                
                problemDetails = new ValidationProblemDetails(veErrors)
                {
                    Status = statusCode,
                    Title = ErrorMessages.Validation,
                    Instance = context.Request.Path
                };
                problemDetails.Extensions[Code] = ve.Code;
                break;

            case DataNotFoundException dnfe:
                statusCode = (int)HttpStatusCode.NotFound;
                problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = dnfe.Message,
                    Instance = context.Request.Path
                };
                problemDetails.Extensions[Code] = dnfe.Code;
                break;

            case ConflictException ce:
                statusCode = (int)HttpStatusCode.Conflict;
                problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = ce.Message,
                    Instance = context.Request.Path
                };
                problemDetails.Extensions[Code] = ce.Code;
                break;

            case IApiException apiException:
                statusCode = (int)HttpStatusCode.BadRequest;
                problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = exception.Message,
                    Instance = context.Request.Path
                };
                problemDetails.Extensions[Code] = apiException.Code;
                break;

            case DbUpdateConcurrencyException:
                statusCode = (int)HttpStatusCode.Conflict;
                problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = "Данные были изменены или удалены другим процессом.",
                    Instance = context.Request.Path
                };
                problemDetails.Extensions[Code] = ErrorCodes.Concurrency;
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = "Произошла непредвиденная ошибка при обработке запроса.",
                    Instance = context.Request.Path
                };
                problemDetails.Extensions[Code] = ErrorCodes.Generic;
                break;
        }

        return (statusCode, problemDetails);
    }

    /// <summary>
    /// Формирует и записывает в ответ JSON-представление ошибки API.
    /// </summary>
    /// <param name="context">Контекст Http-запроса.</param>
    /// <param name="statusCode">HTTP-статус-код ответа.</param>
    /// <param name="problemDetails">Описание ошибок.</param>
    /// <returns>Задача, представляющая асинхронную запись ответа.</returns>
    private async Task SetErrorResponseAsync(
        HttpContext context, 
        int statusCode, 
        ProblemDetails problemDetails)
    {
        var result = JsonSerializer.Serialize(problemDetails, problemDetails.GetType(),
           _jsonSerializerOptions);

        context.Response.ContentType = HttpConstants.ApplicationProblemJsonContentType;
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(result);
    }
}
