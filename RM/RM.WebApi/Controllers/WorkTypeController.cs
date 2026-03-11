using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RM.Api.DTOs.Requests;
using RM.Api.DTOs.Responses;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;

namespace RM.WebApi.Controllers;

/// <summary>
/// Контроллер работы с видами работ.
/// </summary>
[ApiController]
[Route("api/work-type")]
public class WorkTypeApiController : ControllerBase
{
    private readonly IWorkTypeService _workTypeService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeApiController"/>.
    /// </summary>
    /// <param name="workTypeService">Сервис работы с видами работ.</param>
    /// <param name="mapper">Маппер.</param>
    public WorkTypeApiController(
        IWorkTypeService workTypeService,
        IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(workTypeService, nameof(workTypeService));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

        _workTypeService = workTypeService;
        _mapper = mapper;
    }

    /// <summary>
    /// Предоставляет все виды работ.
    /// </summary>
    /// <param name="pageOptions">Настройки страницы.</param>
    /// <returns>Виды работ.</returns>
    [Route("get-all")]
    [HttpPost]
    public async Task<IEnumerable<WorkTypeResponse>> GetAllAsync(PageOptionsRequest? pageOptions = null)
    {
        var pageOptionsModel = _mapper.Map<PageOptionsModel>(pageOptions);

        var workTypes = await _workTypeService.GetAllAsync(pageOptionsModel);

        var result = workTypes?.Select(_mapper.Map<WorkTypeResponse>)?? [];

        return result;
    }

    /// <summary>
    /// Предоставляет вид работ по его индентификатору.
    /// </summary>
    /// <param name="workTypeGettingByIdRequest">Запрос для получения вида работ по его идентификатору.</param>
    /// <returns>Вид работ.</returns>
    [Route("get-by-id")]
    [HttpPost]
    public async Task<WorkTypeResponse?> GetByIdAsync(WorkTypeGettingByIdRequest workTypeGettingByIdRequest)
    {
        var workTypeGettingByIdModel = _mapper.Map<WorkTypeGettingByIdModel>(workTypeGettingByIdRequest);

        var workType = await _workTypeService.GetByIdAsync(workTypeGettingByIdModel);

        var result = _mapper.Map<WorkTypeResponse>(workType);

        return result;
    }
    
    /// <summary>
    /// Создаёт вид работ.
    /// </summary>
    /// <param name="workTypeCreationRequest">Запрос на создание вида работ.</param>
    /// <returns>Созданный вид работ.</returns>
    [Route("create")]
    [HttpPost]
    public async Task<Guid> CreateAsync(WorkTypeCreationRequest workTypeCreationRequest)
    {
        var workTypeCreationModel = _mapper.Map<WorkTypeCreationModel>(workTypeCreationRequest);

        var result = await _workTypeService.CreateAsync(workTypeCreationModel);

        return result;
    }

    /// <summary>
    /// Удаляет вид работ.
    /// </summary>
    /// <param name="workTypeDeletionRequest">Запрос на удаление вида работ.</param>
    /// <returns/>
    [Route("delete")]
    [HttpPost]
    public async Task DeleteAsync(WorkTypeDeletionRequest workTypeDeletionRequest)
    {
        var workTypeDeletionModel = _mapper.Map<WorkTypeDeletionModel>(workTypeDeletionRequest);

        await _workTypeService.DeleteAsync(workTypeDeletionModel);
    }

    /// <summary>
    /// Обновляет вид работ.
    /// </summary>
    /// <param name="workTypeUpdationRequest">Запрос на обновление вида работ.</param>
    /// <returns>Созданный вид работ.</returns>
    [Route("update")]
    [HttpPost]
    public async Task UpdateAsync(WorkTypeUpdationRequest workTypeUpdationRequest)
    {
        var workTypeUpdationModel = _mapper.Map<WorkTypeUpdationModel>(workTypeUpdationRequest);

        await _workTypeService.UpdateAsync(workTypeUpdationModel);
    }
}
