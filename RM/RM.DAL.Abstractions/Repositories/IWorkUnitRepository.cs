using RM.DAL.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RM.DAL.Abstractions.Repositories;

/// <summary>
/// Репозиторий единицы работ.
/// </summary>
public interface IWorkUnitRepository : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Предоставляет все единицы работ.
    /// </summary>
    /// <returns>Единицы работ.</returns>
    Task<IReadOnlyCollection<WorkUnitEntity>> GetAllAsync();

    /// <summary>
    /// Предоставляет единицу работ по ИД.
    /// </summary>
    /// <param name="workUnitId">ИД единицы работ.</param>
    /// <returns>Единица работ.</returns>
    Task<WorkUnitEntity?> GetByIdAsync(byte workUnitId);
}

