using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Converters;
using RM.BLL.Validators;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL;

/// <summary>
/// Сервис видов работ.
/// </summary>
public class WorkTypeService : IWorkTypeService
{

    #region Поля

    /// <summary>
    /// Репозиторий видов работ.
    /// </summary>
    private readonly IWorkTypeRepository _repository;

    /// <summary>
    /// Валидатор вида работ.
    /// </summary>
    private readonly WorkTypeValidator _workTypeValidator;

    /// <summary>
    /// Валидатор настроек страницы.
    /// </summary>
    private readonly PageOptionsValidator _pageOptionsValidator;

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="repository">Репозиторий видов работ.</param>
    /// <param name="workTypeValidator">Валидатор вида работ.</param>
    /// <param name="pageOptionsValidator">Валидатор настроек страницы.</param>
    public WorkTypeService(IWorkTypeRepository repository, WorkTypeValidator workTypeValidator, PageOptionsValidator pageOptionsValidator)
    {
        _repository = repository;
        _workTypeValidator = workTypeValidator;
        _pageOptionsValidator = pageOptionsValidator;
    }

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

        var results = await _repository.GetAllAsync(PageOptionsConverter.ConvertBllToDalModel(pageOptions));

        return results.Select(WorkTypeConverter.ConvertDalToBllModel);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Guid workTypeId, string workTypeName, byte? workUnitId = null)
    {
        throw new NotImplementedException();
    }

    #endregion
}
