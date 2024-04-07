using RM.DAL.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RM.DAL.Abstractions.Repositories;

/// <summary>
/// Репозиторий единицы работ.
/// </summary>
public interface IWorkUnitRepository
{
    #region Методы

    /// <summary>
    /// Предоставляет все единицы работ.
    /// </summary>
    /// <returns>Единицы работ.</returns>
    Task<IEnumerable<WorkUnitModel>> GetAllAsync();

    /// <summary>
    /// Предоставляет единицу работ по его идентификатору.
    /// </summary>
    /// <param name="workUnitId">Идентификатор единицы работ.</param>
    /// <returns>Единица работ.</returns>
    #nullable enable
    Task<WorkUnitModel?> GetByIdAsync(byte workUnitId);

    #endregion
}

