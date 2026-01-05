using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Extensions;
using RM.DAL.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RM.BLL.Services;

/// <summary>
/// Сервис единицы работ.
/// </summary>
public class WorkUnitService : IWorkUnitService
{
    /// <summary>
    /// Репозиторий единицы работ.
    /// </summary>
    private readonly IWorkUnitRepository _workUnitRepository;
 
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorkUnitRepository"/>.
    /// </summary>
    /// <param name="workUnitRepository">Репозиторий единицы работ.</param>
    public WorkUnitService(IWorkUnitRepository workUnitRepository)
    {
        ArgumentNullException.ThrowIfNull(workUnitRepository);

        _workUnitRepository = workUnitRepository;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<WorkUnitModel>> GetAllAsync()
    {
        var workUnits = await _workUnitRepository.GetAllAsync();
        
        var result = workUnits.Select(p => p.ToBll())
                              .ToList();
        return result;
    }

}
