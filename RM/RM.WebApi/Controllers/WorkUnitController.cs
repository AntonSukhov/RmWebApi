using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;

namespace RM.WebApi.Controllers;

/// <summary>
/// Контроллер работы с единицами работ.
/// </summary>
[ApiController]
[Route("api/work-unit")]
public class WorkUnitApiController : ControllerBase
{
    #region Поля

    /// <summary>
    /// Сервис получения данных о единицах работ.
    /// </summary>
    private readonly IWorkUnitService _workUnitService;

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    /// <param name="workUnitService">Сервис получения данных о единицах работ.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public WorkUnitApiController(IWorkUnitService workUnitService) 
    {  
        _workUnitService = workUnitService ?? throw new ArgumentNullException(nameof(workUnitService));
    }

    #endregion

    #region Методы

    /// <summary>
    /// Предоставляет все единицы работ.
    /// </summary>
    /// <returns>Единицы работ.</returns>
    [Route("all")]
    [HttpGet]
    public async Task<IEnumerable<WorkUnitModel>> GetAllAsync()
    {
        var result = await _workUnitService.GetAllAsync();

        return result;
    }

    #endregion
}
