using FluentAssertions;
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
    /// <param name="fixture">Настройка контекста для тестирования репозитория видов работ.</param>
    public class UpdateAsyncTests(WorkTypeRepositoryFixture fixture) : IClassFixture<WorkTypeRepositoryFixture>
    {
        #region Поля

        /// <summary>
        /// Репозиторий вида работ, работающий с MS SQL.
        /// </summary>
        private readonly IWorkTypeRepository _repositoryMsSql = fixture.WorkTypeRepositoryMsSql;

        /// <summary>
        /// Репозиторий вида работ, работающий с PostgreSQL.
        /// </summary>
        private readonly IWorkTypeRepository _repositoryPostgreSql = fixture.WorkTypeRepositoryPostgreSql;

        /// <summary>
        /// Репозиторий вида работ, работающий с SQLite в памяти.
        /// </summary>
        private readonly IWorkTypeRepository _repositorySqliteInMemory = fixture.WorkTypeRepositorySqliteInMemory;

        #endregion

        #region Методы

        /// <summary>
        /// Тест обновления вида работ для корректных входных данных в
        /// источнике данных MS SQL.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForCorrectInputDataTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task ForCorrectInputDataInMsSql(WorkTypeModel workTypeModel, 
                                                     string workTypeName, 
                                                     byte? workUnitId)
        {
            await ForCorrectInputData(workTypeModel, workTypeName, workUnitId, 
                                      _repositoryMsSql);
        }

        /// <summary>
        /// Тест обновления вида работ для корректных входных данных в
        /// источнике данных PostgreSQL.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForCorrectInputDataTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task ForCorrectInputDataInPostgreSql(WorkTypeModel workTypeModel, 
                                                          string workTypeName, 
                                                          byte? workUnitId)
        {
            await ForCorrectInputData(workTypeModel, workTypeName, workUnitId,
                                      _repositoryPostgreSql);
        }

        /// <summary>
        /// Тест обновления вида работ для корректных входных данных в
        /// источнике данных SQLite в памяти.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForCorrectInputDataInSqliteInMemoryTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task ForCorrectInputDataInSqliteInMemory(WorkTypeModel workTypeModel, 
                                                              string workTypeName, 
                                                              byte? workUnitId)
        {
            workTypeModel.Name = workTypeName;
            workTypeModel.WorkUnitId = workUnitId;

            await _repositorySqliteInMemory.UpdateAsync(workTypeModel);

            var expected = await _repositorySqliteInMemory.GetByIdAsync(workTypeModel.Id);

            expected.Should().NotBeNull()
                             .And
                             .Match<WorkTypeModel>(p => p.Id == workTypeModel.Id && 
                                                        p.Name == workTypeModel.Name && 
                                                        p.WorkUnitId == workTypeModel.WorkUnitId &&  
                                                        p.WorkUnitId != null ? p.WorkUnitId == p.WorkUnit.Id: 
                                                                               p.WorkUnit == null);
        }


        /// <summary>
        /// Тест обновления вида работ для некорректного названия вида работ или 
        /// некорректного индентификатора единиц работ в источнике данных MS SQL.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForIncorrectWorkTypeNameOrWorkUnitIdTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task ForIncorrectWorkTypeNameOrWorkUnitIdInMsSql(WorkTypeModel workTypeModel, 
                                                                      string? workTypeName, 
                                                                      byte? workUnitId)
        {
            await ForIncorrectWorkTypeNameOrWorkUnitId(workTypeModel, workTypeName, 
                                                       workUnitId, _repositoryMsSql);
        }

        /// <summary>
        /// Тест обновления вида работ для некорректного названия вида работ или 
        /// некорректного индентификатора единиц работ в источнике данных PostgreSQL.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForIncorrectWorkTypeNameOrWorkUnitIdTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task ForIncorrectWorkTypeNameOrWorkUnitIdInPostgreSql(WorkTypeModel workTypeModel, 
                                                                           string? workTypeName, 
                                                                           byte? workUnitId)
        {
            await ForIncorrectWorkTypeNameOrWorkUnitId(workTypeModel, workTypeName, 
                                                       workUnitId, _repositoryPostgreSql);
        }

        /// <summary>
        /// Тест обновления вида работ для некорректного названия вида работ или 
        /// некорректного индентификатора единиц работ в источнике данных SQLite в памяти.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForIncorrectWorkTypeNameOrWorkUnitIdTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task ForIncorrectWorkTypeNameOrWorkUnitIdInSqliteInMemory(WorkTypeModel workTypeModel, 
                                                                               string? workTypeName, 
                                                                               byte? workUnitId)
        {
            workTypeModel.Name = workTypeName;
            workTypeModel.WorkUnitId = workUnitId;

            var action = async () => await _repositorySqliteInMemory.UpdateAsync(workTypeModel);

            await action.Should().ThrowAsync<DbUpdateException>(); 
        }

        #region Закрытые методы

        /// <summary>
        /// Тест обновления вида работ для корректных входных данных.
        /// </summary>
        /// <param name="workTypeModel">Создаваемый вид работ.</param>
        /// <param name="workTypeName">Название вида работ.</param>
        /// <param name="workUnitId">ИД единицы работ.</param>
        /// <param name="repository">Репозиторий вида работ.</param>
        /// <returns/>
        private static async Task ForCorrectInputData(WorkTypeModel workTypeModel, 
                                                      string workTypeName, 
                                                      byte? workUnitId, 
                                                      IWorkTypeRepository repository)
        {
            await repository.CreateAsync(workTypeModel);

            workTypeModel.Name = workTypeName;
            workTypeModel.WorkUnitId = workUnitId;

            await repository.UpdateAsync(workTypeModel);

            var expected = await repository.GetByIdAsync(workTypeModel.Id);

            await repository.DeleteAsync(expected.Id);

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
        /// <param name="workUnitId">ИД единицы работ.</param>
        /// <param name="repository">Репозиторий вида работ.</param>
        /// <returns/>
        private static async Task ForIncorrectWorkTypeNameOrWorkUnitId(WorkTypeModel workTypeModel, 
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
