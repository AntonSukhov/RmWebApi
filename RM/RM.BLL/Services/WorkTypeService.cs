using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Disposable;
using Infrastructure.Mapping.Extensions;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Exceptions;
using RM.BLL.Mapping.MapperSets;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Services;

/// <summary>
/// Сервис видов работ.
/// </summary>
public class WorkTypeService : DisposableBase, IWorkTypeService
{
    private readonly IWorkTypeRepository _workTypeRepository;
    private readonly IWorkUnitRepository _workUnitRepository;
    private readonly IWorkTypeNameValidator _workTypeNameValidator;
    private readonly IWorkTypeUpdationModelValidator _workTypeUpdationModelValidator;
    private readonly IPageOptionsValidator _pageOptionsValidator;
    private readonly IWorkTypeBllMappers _workTypeBllMappers;

    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeService"/>.
    /// </summary>
    /// <param name="workTypeRepository">Репозиторий вида работ.</param>
    /// <param name="workUnitRepository">Репозиторий единицы работ.</param>
    /// <param name="workTypeNameValidator">Валидатор названия вида работ.</param>
    /// <param name="workTypeUpdationModelValidator">Валидатор модели обновления вида работ.</param>
    /// <param name="pageOptionsValidator">Валидатор настроек страницы.</param>
    /// <param name="workTypeBllMappers">Контейнер мапперов для работы с видами работ.</param>
    public WorkTypeService(IWorkTypeRepository workTypeRepository, 
                            IWorkUnitRepository workUnitRepository,
                            IWorkTypeNameValidator workTypeNameValidator,
                            IWorkTypeUpdationModelValidator workTypeUpdationModelValidator,
                            IPageOptionsValidator pageOptionsValidator,
                            IWorkTypeBllMappers workTypeBllMappers)
    {
        ArgumentNullException.ThrowIfNull(workTypeRepository, nameof(workTypeRepository));
        ArgumentNullException.ThrowIfNull(workUnitRepository, nameof(workUnitRepository));
        ArgumentNullException.ThrowIfNull(workTypeNameValidator, nameof(workTypeNameValidator));
        ArgumentNullException.ThrowIfNull(workTypeUpdationModelValidator, nameof(workTypeUpdationModelValidator));
        ArgumentNullException.ThrowIfNull(pageOptionsValidator, nameof(pageOptionsValidator));
        ArgumentNullException.ThrowIfNull(workTypeBllMappers, nameof(workTypeBllMappers));

        _workTypeRepository = workTypeRepository;
        _workUnitRepository = workUnitRepository;
        _workTypeNameValidator = workTypeNameValidator;
        _workTypeUpdationModelValidator = workTypeUpdationModelValidator;
        _pageOptionsValidator = pageOptionsValidator;
        _workTypeBllMappers = workTypeBllMappers;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(WorkTypeCreationModel workTypeCreationModel)
    {
        ArgumentNullException.ThrowIfNull(workTypeCreationModel);

        var workTypeName = workTypeCreationModel.Name?? string.Empty;

        await _workTypeNameValidator.ValidateAndThrowAsync(workTypeName);

        var workTypeByName = await _workTypeRepository.GetByNameAsync(workTypeName);

        if (workTypeByName != null)
        {
            throw new ConflictException(
                $"Вид работ с названием '{workTypeCreationModel.Name}' (ИД '{workTypeByName.Id}') уже существует.");
        }

        if( workTypeCreationModel.WorkUnitId.HasValue && 
            await _workUnitRepository.GetByIdAsync(workTypeCreationModel.WorkUnitId.Value) == null)
        {
            throw new DataNotFoundException(
                $"Единицы работ с ИД'{workTypeCreationModel.WorkUnitId}' не существует.");
        }
        
        var workType = _workTypeBllMappers.ToWorkTypeShortEntity.Map(workTypeCreationModel);
        workType.Id = Guid.NewGuid();

        await _workTypeRepository.CreateAsync(workType);

        return workType.Id;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid workTypeId)
    {        
        _ = await _workTypeRepository.GetByIdAsync(workTypeId)
            ?? throw new DataNotFoundException($"Вид работ по ИД '{workTypeId}' не существует.");

        await _workTypeRepository.DeleteAsync(workTypeId);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<WorkTypeModel>> GetAllAsync(PageOptionsModel? pageOptions = null)
    {
        Infrastructure.Shared.Models.PageOptionsModel? pageOptionsLocal = null;

        if (pageOptions != null)
        {
             await _pageOptionsValidator.ValidateAndThrowAsync(pageOptions);

             pageOptionsLocal = _workTypeBllMappers.ToPageOptionsModel.Map(pageOptions);
        }
        
        var workTypes = await _workTypeRepository.GetAllAsync(pageOptionsLocal);

        var results = workTypes.Select(_workTypeBllMappers.ToWorkTypeModel.Map)
                               .ToList();

        return results;
    }

    /// <inheritdoc/>
    public async Task<WorkTypeModel?> GetByIdAsync(Guid workTypeId)
    {
        var workTypeEntity = await _workTypeRepository.GetByIdAsync(workTypeId);

        var result = workTypeEntity is not null? 
            _workTypeBllMappers.ToWorkTypeModel.Map(workTypeEntity) : null;

        return result;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(WorkTypeUpdationModel workTypeUpdationModel)
    {
        ArgumentNullException.ThrowIfNull(workTypeUpdationModel);

        await _workTypeUpdationModelValidator.ValidateAndThrowAsync(workTypeUpdationModel);

        if( await _workTypeRepository.GetByIdAsync(workTypeUpdationModel.Id) == null)
        {
            throw new DataNotFoundException(
                $"Вида работ с ИД '{workTypeUpdationModel.Id}' не существует.");
        }

        var workTypeByName = await _workTypeRepository.GetByNameAsync(workTypeUpdationModel.Name);

        if (workTypeByName != null && workTypeByName.Id != workTypeUpdationModel.Id)
        {
            throw new ConflictException(
                $"Вид работ с названием '{workTypeUpdationModel.Name}' уже существует для другого ИД '{workTypeByName.Id}'.");
        }

        if( workTypeUpdationModel.WorkUnitId.HasValue && 
            await _workUnitRepository.GetByIdAsync(workTypeUpdationModel.WorkUnitId.Value) == null)
        {
            throw new DataNotFoundException(
                $"Единица работ с ИД '{workTypeUpdationModel.WorkUnitId}' не существует.");
        }
        
        var workType = _workTypeBllMappers.ToWorkTypeShortEntityForUpdate.Map(workTypeUpdationModel);

        await _workTypeRepository.UpdateAsync(workType);
    }

    /// <inheritdoc/>
    protected override void DisposeManagedResources()
    {
        _workUnitRepository?.Dispose();
        _workTypeRepository?.Dispose();
    }

    /// <inheritdoc/>
    protected override async ValueTask DisposeManagedResourcesAsync()
    {
        if(_workUnitRepository != null)
        {
            await _workUnitRepository.DisposeAsync().ConfigureAwait(false);
        }

        if(_workTypeRepository != null)
        {
            await _workTypeRepository.DisposeAsync().ConfigureAwait(false);
        }
    }
}
