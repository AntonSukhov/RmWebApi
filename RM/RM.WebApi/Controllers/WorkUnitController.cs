using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;

namespace RM.WebApi.Controllers;

/// <summary>
/// Контроллер работы с единицами работ.
/// </summary>
/// <param name="workUnitService">Сервис получения данных о единицах работ.</param>
[ApiController]
[Route("api/work-unit")]
public class WorkUnitApiController(IWorkUnitService workUnitService) : ControllerBase
{
    #region Поля

    /// <summary>
    /// Сервис получения данных о единицах работ.
    /// </summary>
    private readonly IWorkUnitService _workUnitService = workUnitService;

    #endregion

    #region Методы

    /// <summary>
    /// Предоставляет все единицы работ.
    /// </summary>
    /// <returns>Единицы работ.</returns>
    [Route("get-all")]
    [HttpGet]
    public async Task<IEnumerable<WorkUnitModel>> GetAllAsync()
    {
        var result = await _workUnitService.GetAllAsync();

        return result;
    }

    #endregion
}
