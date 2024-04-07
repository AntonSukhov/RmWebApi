using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Validators;

namespace RM.BLL.Validators;

/// <summary>
/// Проверяет модель обновления вида работ.
/// </summary>
public class WorkTypeUpdationModelValidator: AbstractValidator<WorkTypeUpdationModel>, 
                                             IWorkTypeUpdationModelValidator
{
   #region Конструкторы
 
    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public WorkTypeUpdationModelValidator()
    {
        #region Идентификатор вида работ

        var propertyName = "Идентификатор вида работ";

        RuleFor(p => p.Id).NotEmpty()
                          .WithMessage("Значение поля '{PropertyName}' не должно быть пустым.")
                          .WithName(propertyName);

        #endregion
        
        #region Название вида работ и идентификатор единицы работ

        propertyName = "Название вида работ";

        RuleFor(p => p).Custom((p, context) =>
        {
            context.MessageFormatter.AppendArgument("PropertyName", propertyName);
            
            if (!p.WorkUnitId.HasValue && string.IsNullOrWhiteSpace(p.Name))
            {           
                context.AddFailure(propertyName, "Значение поля '{PropertyName}' не должно быть пустым.");
            }
            else if(!string.IsNullOrWhiteSpace(p.Name) && !(p.Name.Length >= 1 && p.Name.Length <= 200))
            {
                context.MessageFormatter.AppendArgument("MinLength", 1);
                context.MessageFormatter.AppendArgument("MaxLength", 200);
                context.MessageFormatter.AppendArgument("TotalLength", p.Name.Length);

                context.AddFailure(propertyName, "Значение поля '{PropertyName}' должно быть длиной от {MinLength} до {MaxLength} символов. Вы ввели {TotalLength} символов.");
            }   
        });

        #endregion
    }

    #endregion

    #region Методы

    /// <inheritdoc/>
    public async Task ValidateAndThrowAsync(WorkTypeUpdationModel model, 
                                            CancellationToken cancellationToken = default)
    {
        var validator = (IValidator<WorkTypeUpdationModel>)this;

        await validator.ValidateAndThrowAsync(model, cancellationToken);
    }

    #endregion
}
