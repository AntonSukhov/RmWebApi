using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Base;
using RM.DAL.Tests.Fixtures;
using Xunit.Abstractions;

namespace RM.DAL.Tests.WorkUnitRepositoryTests
{
    /// <summary>
    /// Тесты для метода <see cref="IWorkUnitRepository.GetAll"/>
    /// </summary>
    public class GetAllTests : TestBase, IClassFixture<WorkUnitRepositoryFixture>
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
        public GetAllTests(WorkUnitRepositoryFixture repositoryFixture, ITestOutputHelper output) : base(output)
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
        public async Task GetAllTest()
        {
            _output.WriteLine($"Входные параметры метода: отсутствуют");

            var actual = new[]
            {
                new WorkUnitModel { Id = 1, Name = "машина" },
                new WorkUnitModel { Id = 2, Name = "шт." },
                new WorkUnitModel { Id = 3, Name = "Кв.м." }
            };

            var expected = await _workUnitRepository.GetAll();

            expected.Should().BeEquivalentTo(actual);
        }

        #endregion
    }
}
