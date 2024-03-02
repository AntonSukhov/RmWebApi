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
    [Route("all")]
    [HttpPost]
    public async Task<IEnumerable<WorkTypeModel>> GetAllAsync(PageOptionsModel pageOptions = null)
    {
        var result = await _workTypeService.GetAllAsync(pageOptions);

        return result;
    }

    #endregion
}
