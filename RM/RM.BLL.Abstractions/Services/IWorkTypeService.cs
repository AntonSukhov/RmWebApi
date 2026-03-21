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
    /// <summary>
    /// Предоставляет все виды работ.
    /// </summary>
    /// <param name="pageOptions">Настройки страницы.</param>
    /// <returns>Виды работ.</returns>
    Task<IReadOnlyCollection<WorkTypeModel>> GetAllAsync(PageOptionsModel? pageOptions = null);

    /// <summary>
    /// Предоставляет вид работ по его ИД.
    /// </summary>
    /// <param name="workTypeId">ИД вида работ.</param>
    /// <returns>Вид работ.</returns>
    Task<WorkTypeModel?> GetByIdAsync(Guid workTypeId);

    /// <summary>
    /// Создаёт вид работ.
    /// </summary>
    /// <param name="workTypeCreationModel">Модель создания вида работ.</param>
    /// <returns>Идентификатор созданного вида работ.</returns>
    Task<Guid> CreateAsync(WorkTypeCreationModel workTypeCreationModel);

    /// <summary>
    /// Обновляет вид работ.
    /// </summary>
    /// <param name="workTypeUpdationModel">Модель обновления вида работ.</param>
    /// <returns/>
    Task UpdateAsync(WorkTypeUpdationModel workTypeUpdationModel);

    /// <summary>
    /// Удаление вида работ.
    /// </summary>
    /// <param name="workTypeDeletionModel">Модель удаления вида работ.</param>
    /// <returns/>
    Task DeleteAsync(WorkTypeDeletionModel workTypeDeletionModel);

}
