using System;
using System.Collections.Generic;
using System.Linq;
using RM.BLL.Abstractions.Errors;

namespace RM.BLL.Exceptions;

// <summary>
/// Агрегированное исключение валидации данных.
/// </summary>
/// <remarks>
/// Исключение возникает при множественных ошибках валидации и содержит коллекцию 
/// вложенных исключений <see cref="ValidationException"/>.
/// </remarks>
public class ValidationAggregationException : Exception, IApiException
{
    
    /// <summary>
    /// Получает коллекцию вложенных исключений <see cref="ValidationException"/>.
    /// </summary>
    public IReadOnlyCollection<ValidationException> InnerValidationExceptions { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ValidationAggregationException"/>.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="innerValidationExceptions">Коллекция вложенных исключений <see cref="ValidationException"/>.</param>
    public ValidationAggregationException(string? message, IReadOnlyCollection<ValidationException> innerValidationExceptions)
        : base(message)
    {
        ArgumentNullException.ThrowIfNull(innerValidationExceptions, nameof(innerValidationExceptions));

        if (innerValidationExceptions.Count == 0)
        {
            throw new ArgumentException("Коллекция не должна быть пустой.", nameof(innerValidationExceptions));
        }

        InnerValidationExceptions = innerValidationExceptions;
    }

     /// <inheritdoc/>
    public ApiError ToApiError() => new()
    {
        Code = ErrorCodes.Validation,
        Message = Message,
        Details = InnerValidationExceptions.Select(p=>p.Message)
                                           .ToList()
    };
}
