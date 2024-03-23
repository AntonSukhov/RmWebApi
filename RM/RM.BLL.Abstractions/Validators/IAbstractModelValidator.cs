using System.Threading;
using System.Threading.Tasks;

namespace RM.BLL.Abstractions.Validators;

/// <summary>
/// Абстрактный валидатор модели.
/// </summary>
public interface IAbstractModelValidator
{
    #region Методы

    /// <summary>
    /// Проверяет модель и выбрасывает исключение в случае ошибки.
    /// </summary>
    /// <typeparam name="TModel">Тип данных проверяемой модели.</typeparam>
    /// <param name="model">Проверяемая модель.</param>
    /// <param name="cancellationToken">Токен отмены выполнения проверки модели.</param>
    /// <returns/>
    public Task ValidateAndThrowAsync<TModel>(TModel model, CancellationToken cancellationToken = default);

    #endregion
}
