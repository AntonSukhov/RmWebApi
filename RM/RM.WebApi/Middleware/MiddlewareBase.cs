using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RM.WebApi.Middleware;

/// <summary>
/// Базовое промежуточное программное обеспечение (middleware).
/// </summary>
public abstract class MiddlewareBase
{
    /// <summary>
    /// Делегат обработки Http-запроса на следующем этапе конвейера обработки запроса.
    /// </summary>
    protected readonly RequestDelegate _next;

    /// <summary>
    /// Инициализирует экземпляр <see cref="MiddlewareBase"/>.
    /// </summary>
    /// <param name="next">Делегат обработки Http-запроса на следующем этапе конвейера обработки запроса.</param>
    /// <exception cref="ArgumentNullException">Возникает, если <paramref name="next"/> равен <c>null</c>.</exception>
    protected MiddlewareBase(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    /// <summary>
    /// Выполняет обработку Http-запроса.
    /// </summary>
    /// <param name="context">Контекст Http-запроса.</param>
    /// <returns>Задача, представляющая асинхронную обработку Http-запроса.</returns>
    public abstract Task Invoke(HttpContext context);
}
