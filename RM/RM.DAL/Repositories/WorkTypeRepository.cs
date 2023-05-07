using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Converters;
using RM.DAL.DbContexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RM.DAL.Repositories
{
    /// <summary>
    /// Репозиторий вида работ
    /// </summary>
    public class WorkTypeRepository : IWorkTypeRepository
    {
        #region Поля

        /// <summary>
        /// Контекст работы с базой данных договоров ГПД
        /// </summary>
        private readonly IContractGpdDbContext _contractGpdDbContext;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contractGpdDbContext">Контекст работы с базой данных договоров ГПД</param>
        public WorkTypeRepository(IContractGpdDbContext contractGpdDbContext)
        {
            _contractGpdDbContext = contractGpdDbContext;
        }

        #endregion

        #region Методы

        ///<inheritdoc/>
        public async Task<IEnumerable<WorkTypeModel>> GetWorkTypes()
        {
            var result = await _contractGpdDbContext.WorkTypes.Include(p => p.WorkUnit).ToListAsync();

            return result.Select(p => p.ConvertEntityToModel());
        }

        #endregion
    }
}
