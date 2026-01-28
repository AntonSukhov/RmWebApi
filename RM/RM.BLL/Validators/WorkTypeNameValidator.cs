using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Extensions;

namespace RM.BLL.Validators;

/// <summary>
/// Проверяет название вида работ.
/// </summary>
public class WorkTypeNameValidator: AbstractValidator<string>, IWorkTypeNameValidator
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeNameValidator"/>.
    /// </summary>
    public WorkTypeNameValidator()
    {
        var propertyName = "Название вида работ";

        RuleFor(p => p).NotEmpty()
                       .WithMessage("Значение поля '{PropertyName}' не должно быть пустым.")
                       .WithName(propertyName);

        RuleFor(p => p).Length(1, 200)
                        .When(p => !string.IsNullOrWhiteSpace(p))
                        .WithMessage("Значение поля '{PropertyName}' должно быть длиной от {MinLength} до {MaxLength} символов. Вы ввели {TotalLength} символов.")
                        .WithName(propertyName);
    }

    /// <inheritdoc/>
    public async Task ValidateAndThrowAsync(string model, CancellationToken cancellationToken = default)
    {
        await this.ValidateAndThrowCustomAsync(model, cancellationToken);
    }
}