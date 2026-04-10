using Infrastructure.Mapping.Interfaces;
using Infrastructure.Disposable;
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
public class WorkTypeRepository : DisposableBase, IWorkTypeRepository
{
    private readonly ContractGpdDbContextBase _dbContext;
    private readonly IMapper<WorkTypeShortEntity, WorkTypeEntity> _workTypeMapper;

    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeRepository"/>.
    /// </summary>
    /// <param name="dbContext">Контекст работы с БД договоров ГПД.</param>
    /// <param name="workTypeMapper">Маппер вида работ.</param>
    public  WorkTypeRepository(
        ContractGpdDbContextBase dbContext, 
        IMapper<WorkTypeShortEntity, WorkTypeEntity> workTypeMapper)
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
        var workTypeEntity = _workTypeMapper.Map(workType);

        await _dbContext.AddEntityAsync(workTypeEntity);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(WorkTypeShortEntity workType)
    {
        IEnumerable<string> updatedProperties = [nameof(WorkTypeEntity.WorkUnitId)];

        if (workType.Name != null)
        {
            updatedProperties = updatedProperties.Append(nameof(WorkTypeEntity.Name));
        }

        var workTypeEntity = _workTypeMapper.Map(workType);

        await _dbContext.UpdateEntityAsync(workTypeEntity, updatedProperties);
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

    /// <inheritdoc/>
    protected override void DisposeManagedResources()
    {
        _dbContext?.Dispose();
    }

    /// <inheritdoc/>
    protected override async ValueTask DisposeManagedResourcesAsync()
    {
        if (_dbContext != null)
        {
            await _dbContext.DisposeAsync().ConfigureAwait(false);
        }
    }    
}