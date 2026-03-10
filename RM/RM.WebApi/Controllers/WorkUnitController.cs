using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RM.Api.DTOs.Responses;
using RM.BLL.Abstractions.Services;

namespace RM.WebApi.Controllers;

/// <summary>
/// Контроллер работы с единицами работ.
/// </summary>
[ApiController]
[Route("api/work-unit")]
public class WorkUnitApiController : ControllerBase
{
    private readonly IWorkUnitService _workUnitService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkUnitApiController"/>.
    /// </summary>
    /// <param name="workUnitService">Сервис получения данных о единицах работ.</param>
    /// <param name="mapper">Маппер.</param>
    public WorkUnitApiController(IWorkUnitService workUnitService, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(workUnitService, nameof(workUnitService));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

        _workUnitService = workUnitService;
        _mapper = mapper;
    }

    /// <summary>
    /// Предоставляет все единицы работ.
    /// </summary>
    /// <returns>Единицы работ.</returns>
    [Route("get-all")]
    [HttpGet]
    public async Task<IEnumerable<WorkUnitResponse>> GetAllAsync()
    {
        var workUnits = await _workUnitService.GetAllAsync();

        var result = workUnits?.Select(_mapper.Map<WorkUnitResponse>) ?? [];

        return result;
    }
}
