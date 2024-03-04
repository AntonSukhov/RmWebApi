using System;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RM.Common.Services;

namespace RM.WebApi.Middleware;

/// <summary>
/// Промежуточное программное обеспечение обработки ошибок.
/// </summary>
public class ErrorHandlingMiddleware : MiddlewareBase
{ 
        #region Поля

        /// <summary>
        /// Опции JSON-сериализации.
        /// </summary>
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию.
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

        #endregion

        #region Методы

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

            var baseException = exception.GetBaseException();
            var message = $"{exception.GetType().Name}: {exception.Message}";
                
            if (baseException != null && exception.Message != baseException.Message)
            {
                message = $"{exception.GetType().Name}: {exception.Message}, Base {baseException.GetType().Name}: {baseException.Message}";
            }

            await SetErrorResponseAsync(context, message);
        }

        /// <summary>
        /// Устанавливает свойства ответа, которые соответствуют возникшей ошибке.
        /// </summary>
        /// <param name="context">Контекст Http-запроса.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <returns/>
        private async Task SetErrorResponseAsync(HttpContext context, string message)
        {
            var result = JsonSerializer.Serialize(new { error = message }, _jsonSerializerOptions);
            context.Response.ContentType = Constants.ApplicationJsonContentType;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(result);
        }

        #endregion
}
