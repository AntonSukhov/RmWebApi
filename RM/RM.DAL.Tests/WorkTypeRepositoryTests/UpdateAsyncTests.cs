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
    public class UpdateAsyncTests : IClassFixture<WorkTypeRepositoryFixture>
    {
        #region Поля

        /// <summary>
        /// Репозиторий вида работ.
        /// </summary>
        private readonly IWorkTypeRepository _repository;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="fixture"></param>
        public UpdateAsyncTests(WorkTypeRepositoryFixture fixture)
        {
            _repository = fixture?.WorkTypeRepository ?? throw new ArgumentNullException(nameof(fixture));
        }

        #endregion

        #region Методы

        /// <summary>
        /// Тест обновления вида работ для корректных входных данных.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForCorrectInputDataTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task UpdateAsyncForCorrectInputDataTest(WorkTypeModel workTypeModel, string workTypeName, byte? workUnitId)
        {
            await _repository.CreateAsync(workTypeModel);

            workTypeModel.Name = workTypeName;
            workTypeModel.WorkUnitId = workUnitId;

            await _repository.UpdateAsync(workTypeModel);

            var expected = await _repository.GetByIdAsync(workTypeModel.Id);

            await _repository.DeleteAsync(expected.Id);

            expected.Should().NotBeNull()
                             .And
                             .Match<WorkTypeModel>(p => p.Id == workTypeModel.Id && 
                                                        p.Name == workTypeModel.Name && 
                                                        p.WorkUnitId == workTypeModel.WorkUnitId &&  
                                                        p.WorkUnitId != null ? p.WorkUnitId == p.WorkUnit.Id: p.WorkUnit == null);
        }

         /// <summary>
        /// Тест обновления вида работ для некорректного названия вида работ или некорректного индентификатора единиц работ.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForIncorrectWorkTypeNameOrWorkUnitIdTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task UpdateAsyncForIncorrectWorkTypeNameOrWorkUnitIdTest(WorkTypeModel workTypeModel, string? workTypeName, byte? workUnitId)
        {
            await _repository.CreateAsync(workTypeModel);

            workTypeModel.Name = workTypeName;
            workTypeModel.WorkUnitId = workUnitId;

            var action = async () => await _repository.UpdateAsync(workTypeModel);

            await action.Should().ThrowAsync<DbUpdateException>(); 
     
            await _repository.DeleteAsync(workTypeModel.Id);

        }

        #endregion
    }
}
