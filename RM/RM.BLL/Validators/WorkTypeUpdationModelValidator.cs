using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Extensions;

namespace RM.BLL.Validators;

/// <summary>
/// Проверяет модель обновления вида работ.
/// </summary>
public class WorkTypeUpdationModelValidator: AbstractValidator<WorkTypeUpdationModel>, 
                                             IWorkTypeUpdationModelValidator
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeUpdationModelValidator"/>.
    /// </summary>
    public WorkTypeUpdationModelValidator()
    {

        var propertyName = "Идентификатор вида работ";

        RuleFor(p => p.Id).NotEmpty()
                          .WithMessage("Значение поля '{PropertyName}' не должно быть пустым.")
                          .WithName(propertyName);

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

    }

    /// <inheritdoc/>
    public async Task ValidateAndThrowAsync(WorkTypeUpdationModel model, 
                                            CancellationToken cancellationToken = default)
    {
        var validator = (IValidator<WorkTypeUpdationModel>)this;

        await validator.ValidateAndThrowCustomAsync(model, cancellationToken);
    }
}
