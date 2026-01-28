using FluentValidation;

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
        var propertyName = "Название вида работ";

        RuleFor(p => p).NotEmpty()
                       .WithMessage("Значение поля '{PropertyName}' не должно быть пустым.")
                       .WithName(propertyName);

        RuleFor(p => p).Length(1, 200)
                        .When(p => !string.IsNullOrWhiteSpace(p))
                        .WithMessage("Значение поля '{PropertyName}' должно быть длиной от {MinLength} до {MaxLength} символов. Вы ввели {TotalLength} символов.")
                        .WithName(propertyName);
    }
}