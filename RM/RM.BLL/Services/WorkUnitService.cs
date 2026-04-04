using AutoMapper;
using Infrastructure.Disposable;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
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
    private readonly IMapper _mapper;
 
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorkUnitRepository"/>.
    /// </summary>
    /// <param name="workUnitRepository">Репозиторий единицы работ.</param>
    /// <param name="mapper">Преобразователь классов.</param>
    public WorkUnitService(IWorkUnitRepository workUnitRepository, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(workUnitRepository);
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

        _workUnitRepository = workUnitRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<WorkUnitModel>> GetAllAsync()
    {
        var workUnits = await _workUnitRepository.GetAllAsync();
        
        var results = workUnits.Select(_mapper.Map<WorkUnitModel>)
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
