using FluentAssertions;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Base;
using Xunit.Abstractions;

namespace RM.DAL.Tests.WorkUnitRepositoryTests
{
    /// <summary>
    /// Тесты для метода <see cref="IWorkTypeRepository.GetWorkTypes"/>
    /// </summary>
    public class GetWorkTypesTests : TestBase, IClassFixture<WorkTypeRepositoryFixture>
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
        public GetWorkTypesTests(WorkTypeRepositoryFixture repositoryFixture, ITestOutputHelper output) : base(output)
        {
            _workTypeRepository = repositoryFixture?.WorkTypeRepository ?? throw new ArgumentNullException(nameof(repositoryFixture));
        }

        #endregion

        #region Методы

        /// <summary>
        /// Тест получения всех видов работ из базы данных
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetWorkTypesTest()
        {
            _output.WriteLine($"Входные параметры метода: отсутствуют");

            var expected = await _workTypeRepository.GetWorkTypes();

            expected.Should().NotBeNull()
                             .And
                             .HaveCountGreaterThan(0);
        }

        #endregion
    }
}
