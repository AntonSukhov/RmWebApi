using FluentValidation;
using RM.BLL.Validators.Constants;

namespace RM.BLL.Validators;

/// <summary>
/// Проверяет свойство название вида работ.
/// </summary>
public class WorkTypeNamePropertyValidator: AbstractValidator<string>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeNamePropertyValidator"/>.
    /// </summary>
    public WorkTypeNamePropertyValidator()
    {
        RuleFor(p => p).NotEmpty()
                       .WithMessage(ValidationMessages.NotEmpty)
                       .WithName(FieldNames.WorkTypeName);

        RuleFor(p => p).Length(1, 200)
                        .When(p => !string.IsNullOrWhiteSpace(p))
                        .WithMessage(ValidationMessages.LengthRange)
                        .WithName(FieldNames.WorkTypeName);
    }
}