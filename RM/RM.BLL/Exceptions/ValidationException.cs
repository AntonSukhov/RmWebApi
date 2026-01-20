using System;
using RM.BLL.Abstractions.Errors;

namespace RM.BLL.Exceptions;

/// <summary>
/// Исключение валидации.
/// </summary>
public class ValidationException : Exception, IApiException
{  
    /// <summary>
    /// Инициализирует экземпляр <see cref="ValidationException"/>.
    /// </summary>
    public ValidationException(): base() {}

    /// <summary>
    /// Инициализирует экземпляр <see cref="ValidationException"/>.
    /// </summary>
    /// <param name="message">Сообщение об ошибки.</param>
    public ValidationException(string message) : base(message) { }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ValidationException"/>.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="innerException">Исключение, которое вызвало текущее исключение.</param>
    public ValidationException(string message, Exception innerException) : base(message, innerException) { }

    /// <inheritdoc/>
    public ApiError ToApiError() => new()
    {
        Code = ErrorCodes.Validation,
        Message = Message
    };
}