using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RM.Api.DTOs.Requests;
using RM.Api.DTOs.Responses;
using RM.BLL.Abstractions.Services;
using RM.WebApi.Mapping.MapperSets;
using Swashbuckle.AspNetCore.Annotations;

namespace RM.WebApi.Controllers;

/// <summary>
/// Контроллер работы с исполнителями договоров.
/// </summary>
[ApiController]
[Route("api/performer")]
[Tags("Performers")]
public class PerformerApiController : ControllerBase
{
    private readonly IPerformerService _performerService;
    private readonly IPerformerApiMappers _performerApiMappers;
    private readonly IPageOptionsApiMappers _pageOptionsApiMappers;

    /// <summary>
    /// Инициализирует экземпляр <see cref="PerformerApiController"/>.
    /// </summary>
    /// <param name="performerService">Сервис работы с исполнителями договоров.</param>
    /// <param name="performerApiMappers">Контейнер мапперов для работы с исполнителями договоров.</param>
    /// <param name="pageOptionsApiMappers">Контейнер мапперов для работы с настройками страницы.</param>
    public PerformerApiController(
        IPerformerService performerService,
        IPerformerApiMappers performerApiMappers,
        IPageOptionsApiMappers pageOptionsApiMappers)
    {
        ArgumentNullException.ThrowIfNull(performerService, nameof(performerService));
        ArgumentNullException.ThrowIfNull(performerApiMappers, nameof(performerApiMappers));
        ArgumentNullException.ThrowIfNull(pageOptionsApiMappers, nameof(pageOptionsApiMappers));

        _performerService = performerService;
        _performerApiMappers = performerApiMappers;
        _pageOptionsApiMappers = pageOptionsApiMappers;
    }

    /// <summary>
    /// Предоставляет всех испольнителей договоров.
    /// </summary>
    /// <param name="pageOptions">Настройки страницы.</param>
    /// <returns>Испольнители договоров.</returns>
    [HttpGet("all")]
    [SwaggerOperation(OperationId = "GetPerformersAsync")]
    public async Task<IEnumerable<PerformerResponse>> GetAllAsync(
        [FromQuery] PageOptionsRequest pageOptions)
    {
        var pageOptionsModel = _pageOptionsApiMappers.ToPageOptionsModel.Map(pageOptions);

        var performers = await _performerService.GetAllAsync(pageOptionsModel);

        var result = performers?.Select(_performerApiMappers.ToPerformerResponse.Map)?? [];

        return result;
    }

    /// <summary>
    /// Предоставляет исполнителя договоров по его ИД.
    /// </summary>
    /// <param name="performerId">ИД исполнителя договоров.</param>
    /// <returns>Вид работ.</returns>
    [HttpGet("{performerId:guid}")]
    [SwaggerOperation(OperationId = "GetPerformerAsync")]
    public async Task<PerformerResponse?> GetByIdAsync(Guid performerId)
    {
        var workType = await _performerService.GetByIdAsync(performerId);

        var result = workType is not null? _performerApiMappers.ToPerformerResponse.Map(workType): null;

        return result;
    }
}
