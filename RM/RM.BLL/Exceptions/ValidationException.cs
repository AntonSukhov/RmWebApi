using System;
using RM.BLL.Abstractions.Errors;

namespace RM.BLL.Exceptions;

/// <summary>
/// Исключение валидации.
/// </summary>
public class ValidationException : Exception, IApiException
{  
    /// <summary>
    /// Получает имя поля, в котором обнаружена ошибка валидации.
    /// </summary>
    /// <value>Имя поля, в котором обнаружена ошибка валидации.</value>
    public string FieldName { get; }

    /// <inheritdoc/>
    public string Code => ErrorCodes.Validation;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ValidationException"/>.
    /// </summary>
    /// <param name="fieldName">Имя поля, в котором обнаружена ошибка валидации.</param>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="innerException">Исключение, которое вызвало текущее исключение.</param>
    public ValidationException(string fieldName, string message, Exception? innerException = null) 
        : base(message, innerException)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fieldName, nameof(fieldName));

        FieldName = fieldName;
    }
}