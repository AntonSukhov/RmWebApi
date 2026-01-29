using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Extensions;
using RM.BLL.Validators.Constants;

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
        RuleFor(p => p.PageNumber).GreaterThan(0)
                                  .WithMessage(ValidationMessages.NotLessThanOne)
                                  .WithName(FieldNames.PageNumber);

        RuleFor(p => p.PageSize).GreaterThan(0)
                                .WithMessage(ValidationMessages.NotLessThanOne)
                                .WithName(FieldNames.PageSize);
    }

    /// <inheritdoc/>
    public async Task ValidateAndThrowAsync(PageOptionsModel model, 
                                            CancellationToken cancellationToken = default)
    {
        await this.ValidateAndThrowCustomAsync(model, cancellationToken);
    }

}
