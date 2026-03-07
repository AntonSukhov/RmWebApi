using AutoMapper;
using Infrastructure.EntityFramework.Extensions;
using Infrastructure.Shared.Models;
using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Entities;
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
    public async Task<IReadOnlyCollection<WorkTypeEntity>> GetAllAsync(
        PageOptionsModel? pageOptions = null)
    {
        return await _dbContext.WorkTypes.AsNoTracking()
                                         .Include(p => p.WorkUnit)
                                         .OrderBy(p => p.Id)
                                         .ToListAsync(pageOptions);
    }

    /// <inheritdoc/>
    public async Task<WorkTypeEntity?> GetByIdAsync(Guid workTypeId)
    {
        return await _dbContext.WorkTypes.AsNoTracking()
                                         .Include(p => p.WorkUnit)
                                         .SingleOrDefaultAsync(p => p.Id == workTypeId);
    }

    /// <inheritdoc/>
    public async Task<WorkTypeEntity?> GetByNameAsync(string workTypeName)
    {
        return await _dbContext.WorkTypes.AsNoTracking()
                                         .Include(p => p.WorkUnit)
                                         .SingleOrDefaultAsync(p => p.Name == workTypeName);
    }

    /// <inheritdoc/>
    public async Task CreateAsync(WorkTypeShortEntity workType)
    {
        var workTypeLocal = _workTypeMapper.Map<WorkTypeEntity>(workType);

        await _dbContext.AddEntityAsync(workTypeLocal);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(WorkTypeShortEntity workType)
    {
        IEnumerable<string> updatedProperties = [nameof(WorkTypeEntity.WorkUnitId)];

        if (workType.Name != null)
        {
            updatedProperties = updatedProperties.Append(nameof(WorkTypeEntity.Name));
        }

        var workTypeLocal = _workTypeMapper.Map<WorkTypeEntity>(workType);

        await _dbContext.UpdateEntityAsync(workTypeLocal, updatedProperties);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid workTypeId)
    {
        var workType = new WorkTypeEntity
        {
            Id = workTypeId
        };

        await _dbContext.RemoveEntityAsync(workType);
    }
}