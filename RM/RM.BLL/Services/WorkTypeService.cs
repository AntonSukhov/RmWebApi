using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Extensions;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL;

/// <summary>
/// Сервис видов работ.
/// </summary>
/// <param name="repository">Репозиторий видов работ.</param>
/// <param name="workTypeValidator">Валидатор вида работ.</param>
/// <param name="pageOptionsValidator">Валидатор настроек страницы.</param>
public class WorkTypeService(IWorkTypeRepository repository, 
                             IWorkTypeValidator workTypeValidator,
                             IPageOptionsValidator pageOptionsValidator) : IWorkTypeService
{

    #region Поля

    /// <summary>
    /// Репозиторий видов работ.
    /// </summary>
    private readonly IWorkTypeRepository _repository = repository;

    /// <summary>
    /// Валидатор вида работ.
    /// </summary>
    private readonly IWorkTypeValidator _workTypeValidator = workTypeValidator;

    /// <summary>
    /// Валидатор настроек страницы.
    /// </summary>
    private readonly IPageOptionsValidator _pageOptionsValidator = pageOptionsValidator;

    #endregion

    #region Методы

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(string workTypeName, byte? workUnitId = null)
    {
       throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid workTypeId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<WorkTypeModel>> GetAllAsync(PageOptionsModel pageOptions = null)
    {
        if (pageOptions != null)
        {
             await _pageOptionsValidator.ValidateAndThrowAsync(pageOptions);
        }

        var results = await _repository.GetAllAsync(pageOptions.ToDal());

        return results.Select(p => p.ToBll());
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Guid workTypeId, string workTypeName, byte? workUnitId = null)
    {
        throw new NotImplementedException();
    }

    #endregion
}
