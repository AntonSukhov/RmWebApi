using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Extensions;
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
/// <param name="dbContext">Контекст работы с базой данных договоров ГПД.</param>
public class WorkTypeRepository(ContractGpdDbContextBase dbContext) : IWorkTypeRepository
{
    /// <summary>
    /// Контекст работы с базой данных договоров ГПД.
    /// </summary>
    private readonly ContractGpdDbContextBase _dbContext = dbContext;

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<WorkTypeModel>> GetAllAsync(PageOptionsModel pageOptions = null)
    {
        return await _dbContext.WorkTypes.AsNoTracking()
                                         .Include(p => p.WorkUnit)
                                         .OrderBy(p => p.Id)
                                         .ToListAsync(pageOptions);
    }

    /// <inheritdoc/>
    #nullable enable
    public async Task<WorkTypeModel?> GetByIdAsync(Guid workTypeId)
    {
        return await _dbContext.WorkTypes.AsNoTracking()
                                         .Include(p => p.WorkUnit)
                                         .SingleOrDefaultAsync(p => p.Id == workTypeId);
    }

    /// <inheritdoc/>
    public async Task<WorkTypeModel?> GetByNameAsync(string workTypeName)
    {
        return await _dbContext.WorkTypes.AsNoTracking()
                                         .Include(p => p.WorkUnit)
                                         .SingleOrDefaultAsync(p => p.Name == workTypeName);
    }

    /// <inheritdoc/>
    public async Task CreateAsync(WorkTypeShortModel workTypeModel)
    {
        await _dbContext.AddEntityAsync(workTypeModel.ToDal());
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(WorkTypeShortModel workTypeModel)
    {
        IEnumerable<string> updatedProperties = [nameof(WorkTypeModel.WorkUnitId)];

        if (workTypeModel.Name != null)
        {
            updatedProperties = updatedProperties.Append(nameof(WorkTypeModel.Name));
        }

        await _dbContext.UpdateEntityAsync(workTypeModel.ToDal(), updatedProperties);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid workTypeId)
    {
        var workTypeModel = new WorkTypeModel
        {
            Id = workTypeId
        };

        await _dbContext.RemoveEntityAsync(workTypeModel);
    }
}