using System;
using System.Threading;
using System.Threading.Tasks;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Extensions;

namespace RM.BLL.Validators;

/// <summary>
/// Проверяет название вида работ.
/// </summary>
public class WorkTypeNameValidator: IWorkTypeNameValidator
{
    private readonly WorkTypeNamePropertyValidator _workTypeNamePropertyValidator;

    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeNameValidator"/>.
    /// </summary>
    public WorkTypeNameValidator(WorkTypeNamePropertyValidator workTypeNamePropertyValidator)
    {
        ArgumentNullException.ThrowIfNull(workTypeNamePropertyValidator, 
            nameof(workTypeNamePropertyValidator));

        _workTypeNamePropertyValidator = workTypeNamePropertyValidator;
    }

    /// <inheritdoc/>
    public async Task ValidateAndThrowAsync(string value, CancellationToken cancellationToken = default)
    {
        await _workTypeNamePropertyValidator.ValidateAndThrowCustomAsync(value, cancellationToken);
    }
}