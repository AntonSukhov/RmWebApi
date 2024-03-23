using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Validators;

namespace RM.BLL.Validators;

/// <summary>
/// Проверяет вид работ.
/// </summary>
public class WorkTypeValidator:  AbstractValidator<WorkTypeModel>, IWorkTypeValidator
{
    #region Конструкторы
 
    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public WorkTypeValidator()
    {
        #region Название вида работ

        var propertyName = "Название вида работ";

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

    #region Методы

    /// <inheritdoc/>
    public async Task ValidateAndThrowAsync<WorkTypeModel>(WorkTypeModel model, CancellationToken cancellationToken = default)
    {
        var validator = (IValidator<WorkTypeModel>)this;

        await validator.ValidateAndThrowAsync(model, cancellationToken);
    }

    #endregion
}