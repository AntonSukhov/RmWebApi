using AutoMapper;
using Infrastructure.EntityFramework.Extensions;
using Infrastructure.Shared.Models;
using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.DbContexts;
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
    private readonly ContractGpdDbContextBase _dbContext;
    private readonly IMapper _workTypeMapper;

    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeRepository"/>.
    /// </summary>
    /// <param name="dbContext">Контекст работы с БД договоров ГПД.</param>
    /// <param name="workTypeMapper">Маппер типа работ.</param>
    public  WorkTypeRepository(ContractGpdDbContextBase dbContext, IMapper workTypeMapper)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        ArgumentNullException.ThrowIfNull(workTypeMapper, nameof(workTypeMapper));

        _dbContext = dbContext;
        _workTypeMapper = workTypeMapper;
    }
    

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<WorkTypeModel>> GetAllAsync(
        PageOptionsModel? pageOptions = null)
    {
        return await _dbContext.WorkTypes.AsNoTracking()
                                         .Include(p => p.WorkUnit)
                                         .OrderBy(p => p.Id)
                                         .ToListAsync(pageOptions);
    }

    /// <inheritdoc/>
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
        var workType = _workTypeMapper.Map<WorkTypeModel>(workTypeModel);

        await _dbContext.AddEntityAsync(workType);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(WorkTypeShortModel workTypeModel)
    {
        IEnumerable<string> updatedProperties = [nameof(WorkTypeModel.WorkUnitId)];

        if (workTypeModel.Name != null)
        {
            updatedProperties = updatedProperties.Append(nameof(WorkTypeModel.Name));
        }

        var workType = _workTypeMapper.Map<WorkTypeModel>(workTypeModel);

        await _dbContext.UpdateEntityAsync(workType, updatedProperties);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid workTypeId)
    {
        var workType = new WorkTypeModel
        {
            Id = workTypeId
        };

        await _dbContext.RemoveEntityAsync(workType);
    }
}