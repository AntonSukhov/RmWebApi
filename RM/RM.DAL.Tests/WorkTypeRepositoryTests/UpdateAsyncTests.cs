﻿using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;
using RM.DAL.Tests.TestData;

namespace RM.DAL.Tests.WorkTypeRepositoryTests
{
    /// <summary>
    /// Тесты для метода <see cref="IWorkTypeRepository.UpdateAsync"/>.
    /// </summary>
    public class UpdateAsyncTests : IClassFixture<WorkTypeRepositoryFixture>
    {
       #region Поля

        private readonly WorkTypeRepositoryFixture _fixture;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="fixture">Настройка контекста для тестирования репозитория видов работ.</param>
        public UpdateAsyncTests(WorkTypeRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Тест обновления вида работ для корректных входных данных в
        /// источнике данных MS SQL.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForCorrectInputDataTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task ForCorrectInputDataInMsSql(WorkTypeShortModel workTypeModel, 
                                                     string workTypeName, 
                                                     byte? workUnitId)
        {
            await ForCorrectInputData(workTypeModel, workTypeName, workUnitId, 
                                     _fixture.WorkTypeRepositoryMsSql);
        }

        /// <summary>
        /// Тест обновления вида работ для корректных входных данных в
        /// источнике данных PostgreSQL.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForCorrectInputDataTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task ForCorrectInputDataInPostgreSql(WorkTypeShortModel workTypeModel, 
                                                          string workTypeName, 
                                                          byte? workUnitId)
        {
            await ForCorrectInputData(workTypeModel, workTypeName, workUnitId,
                                      _fixture.WorkTypeRepositoryPostgreSql);
        }

        /// <summary>
        /// Тест обновления вида работ для некорректного названия вида работ или 
        /// некорректного индентификатора единиц работ в источнике данных MS SQL.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForIncorrectWorkTypeNameOrWorkUnitIdTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task ForIncorrectWorkTypeNameOrWorkUnitIdInMsSql(WorkTypeShortModel workTypeModel, 
                                                                      string? workTypeName, 
                                                                      byte? workUnitId)
        {
            await ForIncorrectWorkTypeNameOrWorkUnitId(workTypeModel, workTypeName, 
                                                      workUnitId, _fixture.WorkTypeRepositoryMsSql);
        }

        /// <summary>
        /// Тест обновления вида работ для некорректного названия вида работ или 
        /// некорректного индентификатора единиц работ в источнике данных PostgreSQL.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForIncorrectWorkTypeNameOrWorkUnitIdTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task ForIncorrectWorkTypeNameOrWorkUnitIdInPostgreSql(WorkTypeShortModel workTypeModel, 
                                                                           string? workTypeName, 
                                                                           byte? workUnitId)
        {
            await ForIncorrectWorkTypeNameOrWorkUnitId(workTypeModel, workTypeName, 
                                                        workUnitId, _fixture.WorkTypeRepositoryPostgreSql);
        }

        #region Закрытые методы

        /// <summary>
        /// Тест обновления вида работ для корректных входных данных.
        /// </summary>
        /// <param name="workTypeModel">Создаваемый вид работ.</param>
        /// <param name="workTypeName">Название вида работ.</param>
        /// <param name="workUnitId">Идентификатор единицы работ.</param>
        /// <param name="repository">Репозиторий вида работ.</param>
        /// <returns/>
        private static async Task ForCorrectInputData(WorkTypeShortModel workTypeModel, 
                                                      string workTypeName, 
                                                      byte? workUnitId, 
                                                      IWorkTypeRepository repository)
        {
            await repository.CreateAsync(workTypeModel);

            workTypeModel.Name = workTypeName;
            workTypeModel.WorkUnitId = workUnitId;

            await repository.UpdateAsync(workTypeModel);

            var expected = await repository.GetByIdAsync(workTypeModel.Id);

            await repository.DeleteAsync(expected?.Id?? Guid.NewGuid());

            expected.Should().NotBeNull()
                             .And
                             .Match<WorkTypeModel>(p => p.Id == workTypeModel.Id && 
                                                        p.Name == workTypeModel.Name && 
                                                        p.WorkUnitId == workTypeModel.WorkUnitId &&  
                                                        p.WorkUnitId != null ? p.WorkUnitId == p.WorkUnit.Id: p.WorkUnit == null);
        }

        /// <summary>
        /// Тест обновления вида работ для некорректного названия вида работ или 
        /// некорректного индентификатора единиц работ.
        /// </summary>
        /// <param name="workTypeModel">Создаваемый вид работ.</param>
        /// <param name="workTypeName">Название вида работ.</param>
        /// <param name="workUnitId">Идентификатор единицы работ.</param>
        /// <param name="repository">Репозиторий вида работ.</param>
        /// <returns/>
        private static async Task ForIncorrectWorkTypeNameOrWorkUnitId(WorkTypeShortModel workTypeModel, 
                                                                       string? workTypeName, 
                                                                       byte? workUnitId, 
                                                                       IWorkTypeRepository repository)
        {
            await repository.CreateAsync(workTypeModel);

            workTypeModel.Name = workTypeName;
            workTypeModel.WorkUnitId = workUnitId;

            var action = async () => await repository.UpdateAsync(workTypeModel);

            await action.Should().ThrowAsync<DbUpdateException>(); 
        
            await repository.DeleteAsync(workTypeModel.Id);
        }

        #endregion

        #endregion
    }
}
