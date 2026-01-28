using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using RM.BLL.Exceptions;
using ValidationException = RM.BLL.Exceptions.ValidationException;

namespace RM.BLL.Extensions;

/// <summary>
/// Расширения для <see cref="IValidator{T}"/>.
/// </summary>
public static class ValidatorExtensions
{
    /// <summary>
    /// Валидирует объект и бросает исключение <see cref="ValidationException"/> или 
    /// исключение <see cref="ValidationAggregationException"/> при наличии ошибок.
    /// </summary>
    /// <typeparam name="T">Тип валидируемого объекта.</typeparam>
    /// <param name="validator">Экземпляр валидатора.</param>
    /// <param name="instance">Объект для валидации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public static async Task ValidateAndThrowCustomAsync<T>(
        this IValidator<T> validator,
        T instance,
        CancellationToken cancellationToken = default)
    {
        if (instance is null)
           throw new ArgumentNullException(nameof(instance));

        var result = await validator.ValidateAsync(instance, cancellationToken);

        if (!result.IsValid)
        {
            ThrowValidationException(result.Errors);
        }
    }

    /// <summary>
    /// Формирует и выбрасывает исключение на основе списка ошибок.
    /// </summary>
    /// <param name="errors">Список ошибок</param>
    private static void ThrowValidationException(List<ValidationFailure> errors)
    {
        if (errors.Count == 1)
        {
            var errorMessage = errors.First().ErrorMessage;
            throw new ValidationException(errorMessage);
        }

        var innerExceptions = errors
            .Select(error => new ValidationException(error.ErrorMessage))
            .ToList();

        throw new ValidationAggregationException(
            "Обнаружены ошибки валидации.",
            innerExceptions);
    }
}
