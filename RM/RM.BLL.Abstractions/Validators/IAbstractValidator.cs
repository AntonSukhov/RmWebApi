using System.Threading;
using System.Threading.Tasks;

namespace RM.BLL.Abstractions.Validators;

/// <summary>
/// Абстрактный валидатор значения.
/// </summary>
/// <typeparam name="TValue">Тип данных проверяемого значения.</typeparam>
public interface IAbstractValidator<TValue>
{    
    /// <summary>
    /// Проверяет значение и выбрасывает исключение в случае ошибки.
    /// </summary>
    /// <param name="value">Проверяемое значение.</param>
    /// <param name="cancellationToken">Токен отмены выполнения проверки.</param>
    /// <returns/>
    public Task ValidateAndThrowAsync(TValue value, CancellationToken cancellationToken = default);
}
