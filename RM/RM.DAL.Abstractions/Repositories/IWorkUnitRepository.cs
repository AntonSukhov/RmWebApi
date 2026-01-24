using RM.DAL.Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RM.DAL.Abstractions.Repositories;

/// <summary>
/// Репозиторий единицы работ.
/// </summary>
public interface IWorkUnitRepository
{
    /// <summary>
    /// Предоставляет все единицы работ.
    /// </summary>
    /// <returns>Единицы работ.</returns>
    Task<IReadOnlyCollection<WorkUnitModel>> GetAllAsync();

    /// <summary>
    /// Предоставляет единицу работ по ИД.
    /// </summary>
    /// <param name="workUnitId">ИД единицы работ.</param>
    /// <returns>Единица работ.</returns>
    Task<WorkUnitModel?> GetByIdAsync(byte workUnitId);
}

