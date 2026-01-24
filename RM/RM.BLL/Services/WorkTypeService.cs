using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Exceptions;
using RM.BLL.Extensions;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Services;

/// <summary>
/// Сервис видов работ.
/// </summary>
public class WorkTypeService : IWorkTypeService
{
    private readonly IWorkTypeRepository _workTypeRepository;

    private readonly IWorkUnitRepository _workUnitRepository;

    private readonly IWorkTypeNameValidator _workTypeNameValidator;

    private readonly IWorkTypeUpdationModelValidator _workTypeUpdationModelValidator;

    private readonly IPageOptionsValidator _pageOptionsValidator;

    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeService"/>.
    /// </summary>
    /// <param name="workTypeRepository">Репозиторий вида работ.</param>
    /// <param name="workUnitRepository">Репозиторий единицы работ.</param>
    /// <param name="workTypeNameValidator">Валидатор названия вида работ.</param>
    /// <param name="workTypeUpdationModelValidator">Валидатор модели обновления вида работ.</param>
    /// <param name="pageOptionsValidator">Валидатор настроек страницы.</param>
    public WorkTypeService(IWorkTypeRepository workTypeRepository, 
                            IWorkUnitRepository workUnitRepository,
                            IWorkTypeNameValidator workTypeNameValidator,
                            IWorkTypeUpdationModelValidator workTypeUpdationModelValidator,
                            IPageOptionsValidator pageOptionsValidator)
    {
        ArgumentNullException.ThrowIfNull(workTypeRepository, nameof(workTypeRepository));
        ArgumentNullException.ThrowIfNull(workUnitRepository, nameof(workUnitRepository));
        ArgumentNullException.ThrowIfNull(workTypeNameValidator, nameof(workTypeNameValidator));
        ArgumentNullException.ThrowIfNull(workTypeUpdationModelValidator, nameof(workTypeUpdationModelValidator));
        ArgumentNullException.ThrowIfNull(pageOptionsValidator, nameof(pageOptionsValidator));

        _workTypeRepository = workTypeRepository;
        _workUnitRepository = workUnitRepository;
        _workTypeNameValidator = workTypeNameValidator;
        _workTypeUpdationModelValidator = workTypeUpdationModelValidator;
        _pageOptionsValidator = pageOptionsValidator;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(WorkTypeCreationModel workTypeCreationModel)
    {
        ArgumentNullException.ThrowIfNull(workTypeCreationModel);

        await _workTypeNameValidator.ValidateAndThrowAsync(workTypeCreationModel.Name?? string.Empty);

        var workTypeByName = await _workTypeRepository.GetByNameAsync(workTypeCreationModel.Name);

        if (workTypeByName != null)
        {
            throw new ValidationException($"Вид работ с названием '{workTypeCreationModel.Name}' (идентификатор '{workTypeByName.Id}') уже существует.");
        }

        if( workTypeCreationModel.WorkUnitId.HasValue && 
            await _workUnitRepository.GetByIdAsync(workTypeCreationModel.WorkUnitId.Value) == null)
        {
            throw new ValidationException($"Единицы работ с идентификатором '{workTypeCreationModel.WorkUnitId}' не существует.");
        }
        
        var workType = workTypeCreationModel.ToDal(workTypeId: Guid.NewGuid());

        await _workTypeRepository.CreateAsync(workType);

        return workType.Id;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(WorkTypeDeletionModel workTypeDeletionModel)
    {
        ArgumentNullException.ThrowIfNull(workTypeDeletionModel);
        
        await _workTypeRepository.DeleteAsync(workTypeDeletionModel.Id);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<WorkTypeModel>> GetAllAsync(PageOptionsModel pageOptions = null)
    {
        if (pageOptions != null)
        {
             await _pageOptionsValidator.ValidateAndThrowAsync(pageOptions);
        }

        var workTypes = await _workTypeRepository.GetAllAsync(pageOptions.ToDal());
        var results = workTypes.Select(p => p.ToBll())
                               .ToList();

        return results;
    }

    /// <inheritdoc/>
    public async Task<WorkTypeModel?> GetByIdAsync(WorkTypeGettingByIdModel workTypeGettingByIdModel)
    {
        ArgumentNullException.ThrowIfNull(workTypeGettingByIdModel);

        var result = await _workTypeRepository.GetByIdAsync(workTypeGettingByIdModel.Id);

        return result?.ToBll();
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(WorkTypeUpdationModel workTypeUpdationModel)
    {
        ArgumentNullException.ThrowIfNull(workTypeUpdationModel);

        await _workTypeUpdationModelValidator.ValidateAndThrowAsync(workTypeUpdationModel);

        var workTypeByName = await _workTypeRepository.GetByNameAsync(workTypeUpdationModel.Name);

        if (workTypeByName != null && workTypeByName.Id != workTypeUpdationModel.Id)
        {
            throw new ValidationException($"Вид работ с названием '{workTypeUpdationModel.Name}' и идентификатором '{workTypeByName.Id}' уже существует.");
        }

        if( workTypeUpdationModel.WorkUnitId.HasValue && 
            await _workUnitRepository.GetByIdAsync(workTypeUpdationModel.WorkUnitId.Value) == null)
        {
            throw new ValidationException($"Единица работ с идентификатором '{workTypeUpdationModel.WorkUnitId}' не существует.");
        }
        
        var workType = workTypeUpdationModel.ToDal();

        await _workTypeRepository.UpdateAsync(workType);
    }
}
