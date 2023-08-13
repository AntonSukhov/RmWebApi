using FluentAssertions;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Base;
using RM.DAL.Tests.Fixtures;
using RM.DAL.Tests.TestData;
using Xunit.Abstractions;

namespace RM.DAL.Tests.WorkTypeRepositoryTests
{
    /// <summary>
    /// Тесты для метода <see cref="IWorkTypeRepository.Create"/>
    /// </summary>
    public class CreateTests : TestBase, IClassFixture<WorkTypeRepositoryFixture>
    {
        #region Поля

        /// <summary>
        /// Репозиторий вида работ
        /// </summary>
        private readonly IWorkTypeRepository _workTypeRepository;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="repositoryFixture"></param>
        public CreateTests(WorkTypeRepositoryFixture repositoryFixture, ITestOutputHelper output) : base(output)
        {
            _workTypeRepository = repositoryFixture?.WorkTypeRepository ?? throw new ArgumentNullException(nameof(repositoryFixture));
        }

        #endregion

        #region Методы

        /// <summary>
        /// Тест создания вида работ с корректными входными данными
        /// </summary>
        /// <returns></returns>
        [Theory]
        [MemberData(nameof(WorkTypeRepositoryTestData.CreateWithCorrectDataTestData), MemberType = typeof(WorkTypeRepositoryTestData))]
        public async Task CreateWithCorrectDataTest(string workTypeName, byte? workUnitId)
        {
            var workTypeNameParam = workTypeName ?? _missingValue;
            var workUnitIdParam = workUnitId != null ? workUnitId.ToString() : _missingValue;

            _output.WriteLine($"Входные параметры метода:{Environment.NewLine}{nameof(workTypeName)} = {workTypeNameParam}{Environment.NewLine}{nameof(workUnitId)} = {workUnitIdParam}");

            var expected = await _workTypeRepository.Create(workTypeName, workUnitId);

            await _workTypeRepository.Delete(expected.Id);

            expected.Should().NotBeNull();
        }

        #endregion
    }
}
