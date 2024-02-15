using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RM.DAL.Repositories
{
    /// <summary>
    /// Репозиторий единицы работ.
    /// </summary>
    public class WorkUnitRepository : IWorkUnitRepository
    {
        #region Поля

        /// <summary>
        /// Контекст работы с базой данных договоров ГПД.
        /// </summary>
        private readonly ContractGpdDbContextBase _dbContext;

        #endregion

        #region Конструкторы.

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dbContext">Контекст работы с базой данных договоров ГПД<./param>
        public WorkUnitRepository(ContractGpdDbContextBase dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Методы

        /// <inheritdoc/>
        public async Task<IEnumerable<WorkUnitModel>> GetAllAsync()
        {
            return await _dbContext.WorkUnits.AsNoTracking()
                                             .ToListAsync();
        }

        #endregion
    }
}
