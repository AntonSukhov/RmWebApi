using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RM.DAL.Repositories;

/// <summary>
/// Репозиторий вида работ.
/// </summary>
public class WorkTypeRepository : IWorkTypeRepository
{
    #region Поля

    /// <summary>
    /// Контекст работы с базой данных договоров ГПД.
    /// </summary>
    private readonly ContractGpdDbContextBase _dbContext;

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="dbContext">Контекст работы с базой данных договоров ГПД.</param>
    public WorkTypeRepository(ContractGpdDbContextBase dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Методы

    /// <inheritdoc/>
    public async Task<IEnumerable<WorkTypeModel>> GetAllAsync(PageOptionsModel pageOptions = null)
    {
        return await _dbContext.WorkTypes.AsNoTracking()
                                         .Include(p => p.WorkUnit)
                                         .OrderBy(p => p.Id)
                                         .ToListAsync(pageOptions);
    }

    /// <inheritdoc/>
    public async Task<WorkTypeModel> GetByIdAsync(Guid workTypeId)
    {
        return await _dbContext.WorkTypes.AsNoTracking()
                                         .Include(p => p.WorkUnit)
                                         .SingleOrDefaultAsync(p => p.Id == workTypeId);
    }

    /// <inheritdoc/>
    public async Task CreateAsync(WorkTypeModel workTypeModel)
    {
        ArgumentNullException.ThrowIfNull(workTypeModel);

        await _dbContext.AddAsync(workTypeModel);
        
        try
        {
           await _dbContext.SaveChangesAsync();
        } 
        finally
        {
           _dbContext.Entry(workTypeModel).State = EntityState.Detached;
        }
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(WorkTypeModel workTypeModel)
    {
        ArgumentNullException.ThrowIfNull(workTypeModel);

        _dbContext.Update(workTypeModel);

        try
        {
           await _dbContext.SaveChangesAsync();
        } 
        finally
        {
           _dbContext.Entry(workTypeModel).State = EntityState.Detached;
        }      
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid workTypeId)
    {
        var workTypeModel = new WorkTypeModel
        {
            Id = workTypeId
        };

        _dbContext.Remove(workTypeModel);

        try
        {
           await _dbContext.SaveChangesAsync();
        } 
        finally
        {
           _dbContext.Entry(workTypeModel).State = EntityState.Detached;
        }     
    }

    #endregion
}