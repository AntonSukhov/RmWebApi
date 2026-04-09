using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RM.Api.DTOs.Requests;
using RM.Api.DTOs.Responses;
using RM.BLL.Abstractions.Services;
using RM.WebApi.Mapping.MapperSets;

namespace RM.WebApi.Controllers;

/// <summary>
/// Контроллер работы с видами работ.
/// </summary>
[ApiController]
[Route("api/work-type")]
public class WorkTypeApiController : ControllerBase
{
    private readonly IWorkTypeService _workTypeService;
    private readonly IWorkTypeApiMappers _workTypeApiMappers;

    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeApiController"/>.
    /// </summary>
    /// <param name="workTypeService">Сервис работы с видами работ.</param>
    /// <param name="workTypeApiMappers">Контейнер мапперов для работы с видами работ. </param>
    public WorkTypeApiController(
        IWorkTypeService workTypeService,
        IWorkTypeApiMappers workTypeApiMappers)
    {
        ArgumentNullException.ThrowIfNull(workTypeService, nameof(workTypeService));
        ArgumentNullException.ThrowIfNull(workTypeApiMappers, nameof(workTypeApiMappers));

        _workTypeService = workTypeService;
        _workTypeApiMappers = workTypeApiMappers;
    }

    /// <summary>
    /// Предоставляет все виды работ.
    /// </summary>
    /// <param name="pageOptions">Настройки страницы.</param>
    /// <returns>Виды работ.</returns>
    [HttpGet("all")]
    public async Task<IEnumerable<WorkTypeResponse>> GetAllAsync(
        [FromQuery] PageOptionsRequest pageOptions)
    {
        var pageOptionsModel = _workTypeApiMappers.ToPageOptionsModel.Map(pageOptions);

        var workTypes = await _workTypeService.GetAllAsync(pageOptionsModel);

        var result = workTypes?.Select(_workTypeApiMappers.ToWorkTypeResponse.Map)?? [];

        return result;
    }

    /// <summary>
    /// Предоставляет вид работ по его ИД.
    /// </summary>
    /// <param name="workTypeId">ИД вида работ.</param>
    /// <returns>Вид работ.</returns>
    [HttpGet("{workTypeId:guid}")]
    public async Task<WorkTypeResponse?> GetByIdAsync(Guid workTypeId)
    {
        var workType = await _workTypeService.GetByIdAsync(workTypeId);

        var result = workType is not null? _workTypeApiMappers.ToWorkTypeResponse.Map(workType): null;

        return result;
    }
    
    /// <summary>
    /// Создаёт вид работ.
    /// </summary>
    /// <param name="workTypeCreationRequest">Запрос на создание вида работ.</param>
    /// <returns>Созданный вид работ.</returns>
    [HttpPost]
    public async Task<Guid> CreateAsync(WorkTypeCreationRequest workTypeCreationRequest)
    {
        var workTypeCreationModel = _workTypeApiMappers.ToWorkTypeCreationModel.Map(workTypeCreationRequest);

        var result = await _workTypeService.CreateAsync(workTypeCreationModel);

        return result;
    }

    /// <summary>
    /// Удаляет вид работ.
    /// </summary>
    /// <param name="workTypeId">ИД удаляемого вида работ.</param>
    /// <returns/>
    [HttpDelete("{workTypeId:guid}")]
    public async Task DeleteAsync(Guid workTypeId)
    {
        await _workTypeService.DeleteAsync(workTypeId);
    }

    /// <summary>
    /// Обновляет вид работ.
    /// </summary>
    /// <param name="workTypeUpdationRequest">Запрос на обновление вида работ.</param>
    /// <returns>Созданный вид работ.</returns>
    [HttpPut]
    public async Task UpdateAsync(WorkTypeUpdationRequest workTypeUpdationRequest)
    {
        var workTypeUpdationModel = _workTypeApiMappers.ToWorkTypeUpdationModel.Map(workTypeUpdationRequest);

        await _workTypeService.UpdateAsync(workTypeUpdationModel);
    }
}
