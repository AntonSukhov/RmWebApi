using FluentAssertions;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Base;
using Xunit.Abstractions;

namespace RM.DAL.Tests.WorkUnitRepositoryTests
{
    /// <summary>
    /// Тесты для метода <see cref="IWorkUnitRepository.GetWorkUnits"/>
    /// </summary>
    public class GetWorkUnitsTests : TestBase, IClassFixture<WorkUnitRepositoryFixture>
    {
        #region Поля

        /// <summary>
        /// Репозиторий единицы работ
        /// </summary>
        private readonly IWorkUnitRepository _workUnitRepository;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="repositoryFixture"></param>
        public GetWorkUnitsTests(WorkUnitRepositoryFixture repositoryFixture, ITestOutputHelper output) : base(output)
        {
            _workUnitRepository = repositoryFixture?.WorkUnitRepository ?? throw new ArgumentNullException(nameof(repositoryFixture));
        }

        #endregion

        #region Методы

        /// <summary>
        /// Тест получения всех единиц работ из базы данных
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetWorkUnitsTest()
        {
            _output.WriteLine($"Входные параметры метода: отсутствуют");

            var expected = await _workUnitRepository.GetWorkUnits();

            expected.Should().NotBeNull()
                             .And
                             .HaveCountGreaterThan(0);
        }

        #endregion
    }
}
