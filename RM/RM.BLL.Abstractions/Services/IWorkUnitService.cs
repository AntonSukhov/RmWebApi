using RM.BLL.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RM.BLL.Abstractions.Services;

/// <summary>
/// Сервис единиц работ.
/// </summary>
public interface IWorkUnitService: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Предоставляет все единицы работ.
    /// </summary>
    /// <returns>Единицы работ.</returns>
    Task<IReadOnlyCollection<WorkUnitModel>> GetAllAsync();
}
