﻿using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Models.Pagination;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.DbContexts;
using RM.DAL.Extensions;
using RM.DAL.Extensions.Converters;
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
                                                        .OrderBy(p => p.Id)
                                                        .Page(pageOptions)
                                                        .MapWorkTypeEntityToModel()
                                                        .ToListAsync();
        }

        ///<inheritdoc/>
        public async Task<WorkTypeModel> Create(string workTypeName, byte? workUnitId)
        {
            if (workTypeName == null)
            {
                throw new ArgumentNullException(nameof(workTypeName));
            }

            var workTypeModel = new WorkTypeModel
            {
                Id = Guid.NewGuid(),
                Name = workTypeName,
                WorkUnit = workUnitId != null ? new WorkUnitModel { Id = workUnitId.Value } : null
            };
            var workTypeEntity = workTypeModel.ConvertModelToEntity(true);

            await _contractGpdDbContext.WorkTypes.AddAsync(workTypeEntity);
            await _contractGpdDbContext.SaveChangesAsync();
            return workTypeModel;
        }

        #endregion
    }
}