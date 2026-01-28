using System.Threading;
using System.Threading.Tasks;

namespace RM.BLL.Abstractions.Validators;

/// <summary>
/// Валидатор абстрактной модели.
/// </summary>
/// <typeparam name="TModel">Тип данных проверяемой модели.</typeparam>
public interface IAbstractModelValidator<TModel>
{    
    /// <summary>
    /// Проверяет модель и выбрасывает исключение в случае ошибки.
    /// </summary>
    /// <param name="model">Проверяемая модель.</param>
    /// <param name="cancellationToken">Токен отмены выполнения проверки модели.</param>
    /// <returns/>
    public Task ValidateAndThrowAsync(TModel model, CancellationToken cancellationToken = default);
}
