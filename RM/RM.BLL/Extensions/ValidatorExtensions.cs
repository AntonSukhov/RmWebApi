using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace RM.BLL.Extensions;

/// <summary>
/// Расширения для <see cref="IValidator{T}"/>.
/// </summary>
public static class ValidatorExtensions
{
    /// <summary>
    /// Валидирует объект и бросает исключение <see cref="ValidationException"/> при наличии ошибок.
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
        var result = await validator.ValidateAsync(instance, cancellationToken);

        if (!result.IsValid)
        {
            var errors = result.Errors
                .Select(e => $"[{e.PropertyName}]: {e.ErrorMessage}")
                .ToList();

            var errorMessage = string.Join(" | ", errors);
            throw new ValidationException(errorMessage);
        }
    }
}
