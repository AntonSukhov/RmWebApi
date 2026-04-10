using Infrastructure.Disposable;
using Infrastructure.Mapping.Interfaces;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.DAL.Abstractions.Entities;
using RM.DAL.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RM.BLL.Services;

/// <summary>
/// Сервис единицы работ.
/// </summary>
public class WorkUnitService : DisposableBase, IWorkUnitService
{
    private readonly IWorkUnitRepository _workUnitRepository;
    private readonly IMapper<WorkUnitEntity, WorkUnitModel> _workUnitMapper;
 
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorkUnitRepository"/>.
    /// </summary>
    /// <param name="workUnitRepository">Репозиторий единицы работ.</param>
    /// <param name="workUnitMapper">Маппер единицы работ.</param>
    public WorkUnitService(
        IWorkUnitRepository workUnitRepository, 
        IMapper<WorkUnitEntity, WorkUnitModel> workUnitMapper)
    {
        ArgumentNullException.ThrowIfNull(workUnitRepository);
        ArgumentNullException.ThrowIfNull(workUnitMapper, nameof(workUnitMapper));

        _workUnitRepository = workUnitRepository;
        _workUnitMapper = workUnitMapper;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<WorkUnitModel>> GetAllAsync()
    {
        var workUnits = await _workUnitRepository.GetAllAsync();
        
        var results = workUnits.Select(_workUnitMapper.Map)
                                .ToList();
        return results;
    }

    /// <inheritdoc/>
    protected override void DisposeManagedResources()
    {
        _workUnitRepository?.Dispose();
    }

    /// <inheritdoc/>
    protected override async ValueTask DisposeManagedResourcesAsync()
    {
        if(_workUnitRepository != null)
        {
            await _workUnitRepository.DisposeAsync().ConfigureAwait(false);
        }
    }
}
