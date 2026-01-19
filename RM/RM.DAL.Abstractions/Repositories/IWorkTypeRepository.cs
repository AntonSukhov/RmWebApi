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
    /// <summary>
    /// Предоставляет все виды работ.
    /// </summary>
    /// <param name="pageOptions">Настройки страницы.</param>
    /// <returns>Виды работ.</returns>
    Task<IReadOnlyCollection<WorkTypeModel>> GetAllAsync(
        PageOptionsModel pageOptions = null);

    /// <summary>
    /// Предоставляет вид работ по ИД.
    /// </summary>
    /// <param name="workTypeId">ИД вида работ.</param>
    /// <returns>Вид работ.</returns>
    #nullable enable
    Task<WorkTypeModel?> GetByIdAsync(Guid workTypeId);

    /// <summary>
    /// Предоставляет вид работ по названию.
    /// </summary>
    /// <param name="workTypeName">Название вида работ.</param>
    /// <returns>Вид работ.</returns>
    Task<WorkTypeModel?> GetByNameAsync(string workTypeName);

    /// <summary>
    /// Создаёт вид работ.
    /// </summary>
    /// <param name="workTypeModel">Модель создаваемого вида работ.</param>
    /// <returns/>
    Task CreateAsync(WorkTypeShortModel workTypeModel);

    /// <summary>
    /// Обновляет вид работ.
    /// </summary>
    /// <param name="workTypeModel">Модель обновляемого вида работ.</param>
    /// <returns/>
    Task UpdateAsync(WorkTypeShortModel workTypeModel);

    /// <summary>
    /// Удаление вида работ
    /// </summary>
    /// <param name="workTypeId">Идентификатор удаляемого вида работ.</param>
    /// <returns/>
    Task DeleteAsync(Guid workTypeId);

}
