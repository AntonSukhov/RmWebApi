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
        /// Репозиторий вида работ.
        /// </summary>
        private readonly IWorkTypeRepository _repository = fixture.WorkTypeRepository;

        #endregion

        #region Методы

        /// <summary>
        /// Тест обновления вида работ для корректных входных данных.
        /// </summary>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.UpdateAsyncForCorrectInputDataTestData),
                    MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task ForCorrectInputData(WorkTypeModel workTypeModel, string workTypeName, byte? workUnitId)
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
        public async Task ForIncorrectWorkTypeNameOrWorkUnitId(WorkTypeModel workTypeModel, string? workTypeName, 
                                                               byte? workUnitId)
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
