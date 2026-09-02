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
namespace RM.DAL.Repositories
{
    /// <summary>
    /// Реализация репозитория исполнителей договоров.
    /// </summary>
    public class PerformerRepository : DisposableBase, IPerformerRepository
    {
        private readonly ContractGpdDbContextBase _dbContext;

        /// <summary>
        /// Инициализирует экземпляр <see cref="PerformerRepository"/>.
        /// </summary>
        /// <param name="dbContext">Контекст работы с БД договоров ГПД.</param>
        public  PerformerRepository(ContractGpdDbContextBase dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<PerformerEntity>> GetAllAsync(
            PageOptionsModel? pageOptions = null)
        {
            return await _dbContext.Performers.AsNoTracking()
                                              .OrderBy(p => p.Id)
                                              .ToListAsync(pageOptions);
        }

        /// <inheritdoc/>
        public async Task<PerformerEntity?> GetByIdAsync(Guid performerId)
        {
            return await _dbContext.Performers.AsNoTracking()
                                              .SingleOrDefaultAsync(p => p.Id == performerId);
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
}