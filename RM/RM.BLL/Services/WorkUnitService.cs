using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Converters;
using RM.DAL.Abstractions.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RM.BLL.Services;

/// <summary>
/// Сервис единицы работ.
/// </summary>
public class WorkUnitService : IWorkUnitService
{
    #region Поля

    /// <summary>
    /// Репозиторий единицы работ.
    /// </summary>
    private readonly IWorkUnitRepository _repository;

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="repository">Репозиторий единицы работ.</param>
    public WorkUnitService(IWorkUnitRepository repository)
    {
        _repository = repository;
    }

    #endregion

    #region Методы

    /// <inheritdoc/>
    public async Task<IEnumerable<WorkUnitModel>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();

        return result.Select(WorkUnitConverter.ConvertDalToBllModel);
    }

    #endregion
}
