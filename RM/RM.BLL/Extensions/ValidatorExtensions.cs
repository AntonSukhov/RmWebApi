using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
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
            if(result.Errors.Count == 1)
            {
                var errorMessage = result.Errors.Single().ErrorMessage;
                throw new ValidationException(errorMessage);
            }

            var errors = result.Errors.Select(p => new ValidationException(p.ErrorMessage))
                                      .ToList();
                                      
            throw new ValidationAggregationException("Обнаружены ошибки валидации.", errors);
        }
    }
}
