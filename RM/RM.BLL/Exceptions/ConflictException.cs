using System;
using RM.BLL.Abstractions.Errors;

namespace RM.BLL.Exceptions;

/// <summary>
/// Исключение конфликта данных.
/// </summary>
/// <remarks>
/// Исключение возникает при попытке создать запись, 
/// которая конфликтует с существующими данными (например, нарушение уникальности).
/// </remarks>
public class ConflictException : Exception, IApiException
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ConflictException"/>.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public ConflictException(string message) : base(message) { }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ConflictException"/>.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="innerException">Исключение, которое вызвало текущее исключение.</param>
    public ConflictException(string message, Exception innerException) 
        : base(message, innerException) { }

    /// <inheritdoc/>
    public ApiError ToApiError() => new()
    {
        Code = ErrorCodes.Conflict,
        Message = Message
    };
}