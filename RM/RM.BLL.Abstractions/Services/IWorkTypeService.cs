using RM.BLL.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RM.BLL.Abstractions.Services;

/// <summary>
/// Сервис видов работ.
/// </summary>
public interface IWorkTypeService
{
    #region Методы

    /// <summary>
    /// Предоставляет все виды работ.
    /// </summary>
    /// <param name="pageOptions">Настройки страницы.</param>
    /// <returns>Виды работ.</returns>
    Task<IEnumerable<WorkTypeModel>> GetAllAsync(PageOptionsModel pageOptions = null);

    /// <summary>
    /// Создаёт вид работ
    /// </summary>
    /// <param name="workTypeName">Название создаваемого вида работ.</param>
    /// <param name="workUnitId">ИД единицы работ создаваемого вида работ.</param>
    /// <returns>ИД созданного вида работ.</returns>
    Task<Guid> CreateAsync(string workTypeName, byte? workUnitId = null);

    /// <summary>
    /// Обновляет вид работ.
    /// </summary>
    /// <param name="workTypeId">ИД обновляемого вида работ.</param>
    /// <param name="workTypeName">Название обновляемого вида работ.</param>
    /// <param name="workUnitId">ИД единицы работ обновляемого вида работ./param>
    /// <returns/>
    Task UpdateAsync(Guid workTypeId, string workTypeName, byte? workUnitId = null);

    /// <summary>
    /// Удаление вида работ.
    /// </summary>
    /// <param name="workTypeId">ИД удаляемого вида работ.</param>
    /// <returns/>
    Task DeleteAsync(Guid workTypeId);

    #endregion
}
