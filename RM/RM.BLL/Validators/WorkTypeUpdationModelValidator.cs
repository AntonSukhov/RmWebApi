using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Extensions;
using RM.BLL.Validators.Constants;

namespace RM.BLL.Validators;

/// <summary>
/// Проверяет модель обновления вида работ.
/// </summary>
public class WorkTypeUpdationModelValidator: AbstractValidator<WorkTypeUpdationModel>, 
                                             IWorkTypeUpdationModelValidator
{
     private readonly WorkTypeNamePropertyValidator _workTypeNamePropertyValidator;

    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeUpdationModelValidator"/>.
    /// </summary>
    public WorkTypeUpdationModelValidator(WorkTypeNamePropertyValidator workTypeNamePropertyValidator)
    {

        ArgumentNullException.ThrowIfNull(workTypeNamePropertyValidator, 
            nameof(workTypeNamePropertyValidator));

        _workTypeNamePropertyValidator = workTypeNamePropertyValidator;

        RuleFor(p => p.Id).NotEmpty()
                          .WithMessage(ValidationMessages.NotEmpty)
                          .WithName(FieldNames.WorkTypeId);

        RuleFor(p=>p.Name).SetValidator(_workTypeNamePropertyValidator);
    }

    /// <inheritdoc/>
    public async Task ValidateAndThrowAsync(WorkTypeUpdationModel model, 
                                            CancellationToken cancellationToken = default)
    {
        await this.ValidateAndThrowCustomAsync(model, cancellationToken);
    }
}
