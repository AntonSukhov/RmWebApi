using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Base;
using RM.DAL.Tests.Fixtures;
using Xunit.Abstractions;

namespace RM.DAL.Tests.WorkTypeRepositoryTests
{
    /// <summary>
    /// Тесты для метода <see cref="IWorkTypeRepository.Update"/>
    /// </summary>
    public class UpdateTests : TestBase, IClassFixture<WorkTypeRepositoryFixture>
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
        public UpdateTests(WorkTypeRepositoryFixture repositoryFixture, ITestOutputHelper output) : base(output)
        {
            _workTypeRepository = repositoryFixture?.WorkTypeRepository ?? throw new ArgumentNullException(nameof(repositoryFixture));
        }

        #endregion

        #region Методы

        /// <summary>
        /// Тест создания вида работ с корректными входными данными
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateWithCorrectDataTest()
        {
            _output.WriteLine($"Входные параметры метода: отсутствие");

            var workTypeName = $"Вид работ {Guid.NewGuid()}";
            var workUnitId = (byte)1;

            var input = await _workTypeRepository.Create($"Вид работ {DateTime.Now:yyyy.MM.dd HH:mm:ss ff}");

            await _workTypeRepository.Update(input.Id, workTypeName, workUnitId);

            var expected = (await _workTypeRepository.GetAll()).Where(p => p.Id == input.Id)
                                                               .SingleOrDefault();

            await _workTypeRepository.Delete(input.Id);

            expected.Should().NotBeNull()
                             .And
                             .Match<WorkTypeModel>(p => p.Id == input.Id && p.Name == workTypeName && p.WorkUnit.Id == workUnitId);
        }

        #endregion
    }
}
