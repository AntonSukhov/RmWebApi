using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RM.DAL.Repositories
{
    /// <summary>
    /// Репозиторий единицы работ.
    /// </summary>
    public class WorkUnitRepository : IWorkUnitRepository
    {
        /// <summary>
        /// Контекст работы с базой данных договоров ГПД.
        /// </summary>
        private readonly ContractGpdDbContextBase _dbContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="WorkUnitRepository"/>.
        /// </summary>
        /// <param name="dbContext">Контекст работы с базой данных договоров ГПД<./param>
        public  WorkUnitRepository(ContractGpdDbContextBase dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<WorkUnitModel>> GetAllAsync()
        {
            return await _dbContext.WorkUnits.AsNoTracking()
                                             .ToListAsync();
        }

        /// <inheritdoc/>
        #nullable enable
        public async Task<WorkUnitModel?> GetByIdAsync(byte workUnitId)
        {
            return await _dbContext.WorkUnits.AsNoTracking()
                                             .SingleOrDefaultAsync(p => p.Id == workUnitId);
        }
    }
}
