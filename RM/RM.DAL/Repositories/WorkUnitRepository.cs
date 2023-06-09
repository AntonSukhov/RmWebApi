﻿using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.DbContexts;
using RM.DAL.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RM.DAL.Repositories
{
    /// <summary>
    /// Репозиторий единицы работ
    /// </summary>
    public class WorkUnitRepository : IWorkUnitRepository
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
        public WorkUnitRepository(IContractGpdDbContext contractGpdDbContext)
        {
            _contractGpdDbContext = contractGpdDbContext;
        }

        #endregion

        #region Методы

        ///<inheritdoc/>
        public async Task<IEnumerable<WorkUnitModel>> GetAll()
        {
            return await _contractGpdDbContext.WorkUnits.AsNoTracking()
                                                        .MapWorkUnitEntityToModel()
                                                        .ToListAsync();
        }

        #endregion
    }
}
