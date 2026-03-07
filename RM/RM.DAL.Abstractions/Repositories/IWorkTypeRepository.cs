using Infrastructure.Shared.Models;
using RM.DAL.Abstractions.Entities;
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
    Task<IReadOnlyCollection<WorkTypeEntity>> GetAllAsync(
        PageOptionsModel? pageOptions = null);

    /// <summary>
    /// Предоставляет вид работ по ИД.
    /// </summary>
    /// <param name="workTypeId">ИД вида работ.</param>
    /// <returns>Вид работ.</returns>
    Task<WorkTypeEntity?> GetByIdAsync(Guid workTypeId);

    /// <summary>
    /// Предоставляет вид работ по названию.
    /// </summary>
    /// <param name="workTypeName">Название вида работ.</param>
    /// <returns>Вид работ.</returns>
    Task<WorkTypeEntity?> GetByNameAsync(string workTypeName);

    /// <summary>
    /// Создаёт вид работ.
    /// </summary>
    /// <param name="workType">Вид работ.</param>
    /// <returns/>
    Task CreateAsync(WorkTypeShortEntity workType);

    /// <summary>
    /// Обновляет вид работ.
    /// </summary>
    /// <param name="workType">Вид работ.</param>
    /// <returns/>
    Task UpdateAsync(WorkTypeShortEntity workType);

    /// <summary>
    /// Удаление вида работ
    /// </summary>
    /// <param name="workTypeId">Идентификатор удаляемого вида работ.</param>
    /// <returns/>
    Task DeleteAsync(Guid workTypeId);

}
