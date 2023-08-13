using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Models.Pagination;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.DbContexts;
using RM.DAL.Extensions;
using System;
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
        public async Task<IEnumerable<WorkTypeModel>> GetAll(PageOptionsModel pageOptions = null)
        {
            return await _contractGpdDbContext.WorkTypes.AsNoTracking()
                                                        .Include(p => p.WorkUnit)
                                                        .OrderBy(p => p.Id)
                                                        .Page(pageOptions)
                                                        .ToListAsync();
        }

        ///<inheritdoc/>
        public async Task<WorkTypeModel> Create(string workTypeName, byte? workUnitId = null)
        {
            if (workTypeName == null)
            {
                throw new ArgumentNullException(nameof(workTypeName));
            }

            var workTypeModel = new WorkTypeModel
            {
                Id = Guid.NewGuid(),
                Name = workTypeName,
                WorkUnitId = workUnitId
            };

            await _contractGpdDbContext.WorkTypes.AddAsync(workTypeModel);
            await _contractGpdDbContext.SaveChangesAsync();

            return workTypeModel;
        }

        ///<inheritdoc/>
        public async Task Update(Guid workTypeId, string workTypeName, byte? workUnitId = null)
        {
            if (workTypeName == null)
            {
                throw new ArgumentNullException(nameof(workTypeName));
            }

            var workType = await _contractGpdDbContext.WorkTypes.SingleOrDefaultAsync(p => p.Id == workTypeId) ?? throw new Exception($"Вид работ с ИД '{workTypeId}' не найден");

            workType.Name = workTypeName;
            workType.WorkUnitId = workUnitId;

            await _contractGpdDbContext.SaveChangesAsync();
        }

        ///<inheritdoc/>
        public async Task Delete(Guid workTypeId)
        {
            var workType = await _contractGpdDbContext.WorkTypes.SingleOrDefaultAsync(p => p.Id == workTypeId) ?? throw new Exception($"Вид работ с ИД '{workTypeId}' не найден");

            _contractGpdDbContext.WorkTypes.Remove(workType);

            await _contractGpdDbContext.SaveChangesAsync();
        }

        #endregion
    }
}