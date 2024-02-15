using FluentValidation;
using RM.BLL.Abstractions.Models;

namespace RM.BLL.Validators;

/// <summary>
/// Проверяет вид работ.
/// </summary>
public class WorkTypeValidator: AbstractValidator<WorkTypeModel>
{
    #region Конструкторы
 
    /// <summary>
    /// Конструктор.
    /// </summary>
    public WorkTypeValidator()
    {
        #region ИД вида работ

        var propertyName = "ИД вида работ";

        RuleFor(p => p.Id).NotEmpty()
                            .WithMessage("Значение поля '{PropertyName}' не должно быть пустым.")
                            .WithName(propertyName);

        #endregion

        #region Название вида работ

        propertyName = "Название вида работ";

        RuleFor(p => p.Name).NotEmpty()
                            .WithMessage("Значение поля '{PropertyName}' не должно быть пустым.")
                            .WithName(propertyName);

        RuleFor(p => p.Name).Length(1, 200)
                            .When(p => !string.IsNullOrWhiteSpace(p.Name))
                            .WithMessage("Значение поля '{PropertyName}' должно быть длиной от {MinLength} до {MaxLength} символов. Вы ввели {TotalLength} символов.")
                            .WithName(propertyName);

        #endregion
    }

    #endregion
}
