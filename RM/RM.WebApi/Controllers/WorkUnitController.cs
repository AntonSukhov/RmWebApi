using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Mapping.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RM.Api.DTOs.Responses;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RM.WebApi.Controllers;

/// <summary>
/// Контроллер работы с единицами работ.
/// </summary>
[ApiController]
[Route("api/work-unit")]
[Tags("WorkUnits")]
public class WorkUnitApiController : ControllerBase
{
    private readonly IWorkUnitService _workUnitService;
    private readonly IMapper<WorkUnitModel, WorkUnitResponse> _workUnitMapper;

    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkUnitApiController"/>.
    /// </summary>
    /// <param name="workUnitService">Сервис получения данных о единицах работ.</param>
    /// <param name="workUnitMapper">Преобразователь единицы работ.</param>
    public WorkUnitApiController(
        IWorkUnitService workUnitService, 
        IMapper<WorkUnitModel, WorkUnitResponse> workUnitMapper)
    {
        ArgumentNullException.ThrowIfNull(workUnitService, nameof(workUnitService));
        ArgumentNullException.ThrowIfNull(workUnitMapper, nameof(workUnitMapper));

        _workUnitService = workUnitService;
        _workUnitMapper = workUnitMapper;
    }

    /// <summary>
    /// Предоставляет все единицы работ.
    /// </summary>
    /// <returns>Единицы работ.</returns>
    [HttpGet]
    [SwaggerOperation(OperationId = "GetWorkUnitsAsync")]
    public async Task<IEnumerable<WorkUnitResponse>> GetAllAsync()
    {
        var workUnits = await _workUnitService.GetAllAsync();

        var result = workUnits?.Select(_workUnitMapper.Map) ?? [];

        return result;
    }
}
