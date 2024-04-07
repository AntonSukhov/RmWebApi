using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;

namespace RM.WebApi.Controllers;

/// <summary>
/// Контроллер работы с видами работ.
/// </summary>
/// <param name="workTypeService">Сервис работы с видами работ.</param>
[ApiController]
[Route("api/work-type")]
public class WorkTypeApiController(IWorkTypeService workTypeService) : ControllerBase
{
   #region Поля

    /// <summary>
    /// Сервис работы с видами работ.
    /// </summary>
    private readonly IWorkTypeService _workTypeService = workTypeService;

    #endregion

    #region Методы

    /// <summary>
    /// Предоставляет все виды работ.
    /// </summary>
    /// <param name="pageOptions">Настройки страницы.</param>
    /// <returns>Виды работ.</returns>
    [Route("get-all")]
    [HttpPost]
    public async Task<IEnumerable<WorkTypeModel>> GetAllAsync(PageOptionsModel pageOptions = null)
    {
        var result = await _workTypeService.GetAllAsync(pageOptions);

        return result;
    }

    /// <summary>
    /// Предоставляет вид работ по его индентификатору.
    /// </summary>
    /// <param name="workTypeGettingByIdModel">Модель с данными для получения вида работ по его идентификатору.</param>
    /// <returns>Вид работ.</returns>
    [Route("get-by-id")]
    [HttpPost]
    public async Task<WorkTypeModel> GetByIdAsync(WorkTypeGettingByIdModel workTypeGettingByIdModel)
    {
        var result = await _workTypeService.GetByIdAsync(workTypeGettingByIdModel);

        return result;
    }
    
    /// <summary>
    /// Создаёт вид работ.
    /// </summary>
    /// <param name="workTypeCreationModel">Модель с данными для создания вида работ.</param>
    /// <returns>Созданный вид работ.</returns>
    [Route("create")]
    [HttpPost]
    public async Task<Guid> CreateAsync(WorkTypeCreationModel workTypeCreationModel)
    {
        var result = await _workTypeService.CreateAsync(workTypeCreationModel);

        return result;
    }

    /// <summary>
    /// Удаляет вид работ.
    /// </summary>
    /// <param name="workTypeDeletionModel">Модель с данными для удаления вида работ.</param>
    /// <returns/>
    [Route("delete")]
    [HttpPost]
    public async Task DeleteAsync(WorkTypeDeletionModel workTypeDeletionModel)
    {
        await _workTypeService.DeleteAsync(workTypeDeletionModel);
    }

    /// <summary>
    /// Обновляет вид работ.
    /// </summary>
    /// <param name="workTypeUpdationModel">Модель с данными для обновления вида работ.</param>
    /// <returns>Созданный вид работ.</returns>
    [Route("update")]
    [HttpPost]
    public async Task UpdateAsync(WorkTypeUpdationModel workTypeUpdationModel)
    {
        await _workTypeService.UpdateAsync(workTypeUpdationModel);
    }

    #endregion
}
