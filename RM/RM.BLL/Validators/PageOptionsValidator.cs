using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Extensions;

namespace RM.BLL.Validators;

/// <summary>
/// Проверяет настройки страницы.
/// </summary>
public class PageOptionsValidator: AbstractValidator<PageOptionsModel>, IPageOptionsValidator
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="PageOptionsValidator"/>.
    /// </summary>
    public PageOptionsValidator()
    {
        var propertyName = "Порядковый номер страницы";

        RuleFor(p => p.PageNumber).GreaterThan(0)
                                  .WithMessage("Значение поля '{PropertyName}' не должно быть меньше единицы.")
                                  .WithName(propertyName);

        propertyName = "Кол-во элементов страницы";

        RuleFor(p => p.PageSize).GreaterThan(0)
                                .WithMessage("Значение поля '{PropertyName}' не должно быть меньше единицы.")
                                .WithName(propertyName);
    }

    /// <inheritdoc/>
    public async Task ValidateAndThrowAsync(PageOptionsModel model, 
                                            CancellationToken cancellationToken = default)
    {
        await this.ValidateAndThrowCustomAsync(model, cancellationToken);
    }

}
