using RM.BLL.Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RM.BLL.Abstractions.Services;

/// <summary>
/// Сервис единицы работ.
/// </summary>
public interface IWorkUnitService
{
    #region Методы

    /// <summary>
    /// Предоставляет все единицы работ.
    /// </summary>
    /// <returns>Единицы работ.</returns>
    Task<IEnumerable<WorkUnitModel>> GetAllAsync();

    #endregion
}
