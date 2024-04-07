using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using RM.BLL.Abstractions.Validators;

namespace RM.BLL.Validators;

/// <summary>
/// Проверяет название вида работ.
/// </summary>
public class WorkTypeNameValidator: AbstractValidator<string>, IWorkTypeNameValidator
{
    #region Конструкторы
 
    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public WorkTypeNameValidator()
    {
        #region Название вида работ

        var propertyName = "Название вида работ";

        RuleFor(p => p).NotEmpty()
                       .WithMessage("Значение поля '{PropertyName}' не должно быть пустым.")
                       .WithName(propertyName);

        RuleFor(p => p).Length(1, 200)
                        .When(p => !string.IsNullOrWhiteSpace(p))
                        .WithMessage("Значение поля '{PropertyName}' должно быть длиной от {MinLength} до {MaxLength} символов. Вы ввели {TotalLength} символов.")
                        .WithName(propertyName);

        #endregion
    }

    #endregion

    #region Методы

    /// <inheritdoc/>
    public async Task ValidateAndThrowAsync(string model, CancellationToken cancellationToken = default)
    {
        var validator = (IValidator<string>)this;

        await validator.ValidateAndThrowAsync(model, cancellationToken);
    }

    #endregion
}