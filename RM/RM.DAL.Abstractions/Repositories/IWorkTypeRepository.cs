using RM.DAL.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RM.DAL.Abstractions.Repositories;

/// <summary>
/// Репозиторий видов работ.
/// </summary>
public interface IWorkTypeRepository
{
    #region Методы

    /// <summary>
    /// Предоставляет все виды работ.
    /// </summary>
    /// <param name="pageOptions">Настройки страницы.</param>
    /// <returns>Виды работ.</returns>
    Task<IEnumerable<WorkTypeModel>> GetAllAsync(PageOptionsModel pageOptions = null);

    /// <summary>
    /// Предоставляет вид работ по его ИД.
    /// </summary>
    /// <param name="workTypeId">ИД вида работ.</param>
    /// <returns>Вид работ.</returns>
    Task<WorkTypeModel> GetByIdAsync(Guid workTypeId);

    /// <summary>
    /// Создаёт вид работ.
    /// </summary>
    /// <param name="workTypeModel">Модель создаваемого вида работ.</param>
    /// <returns/>
    Task CreateAsync(WorkTypeModel workTypeModel);

    /// <summary>
    /// Обновляет вид работ.
    /// </summary>
    /// <param name="workTypeModel">Модель обновляемого вида работ.</param>
    /// <returns/>
    Task UpdateAsync(WorkTypeModel workTypeModel);

    /// <summary>
    /// Удаление вида работ
    /// </summary>
    /// <param name="workTypeId">ИД удаляемого вида работ.</param>
    /// <returns/>
    Task DeleteAsync(Guid workTypeId);

    #endregion
}
