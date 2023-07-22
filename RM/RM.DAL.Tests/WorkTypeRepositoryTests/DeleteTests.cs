using FluentAssertions;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Base;
using RM.DAL.Tests.Fixtures;
using Xunit.Abstractions;

namespace RM.DAL.Tests.WorkTypeRepositoryTests
{
    /// <summary>
    /// Тесты для метода <see cref="IWorkTypeRepository.Delete"/>
    /// </summary>
    public class DeleteTests : TestBase, IClassFixture<WorkTypeRepositoryFixture>
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
        public DeleteTests(WorkTypeRepositoryFixture repositoryFixture, ITestOutputHelper output) : base(output)
        {
            _workTypeRepository = repositoryFixture?.WorkTypeRepository ?? throw new ArgumentNullException(nameof(repositoryFixture));
        }

        #endregion

        #region Методы

        /// <summary>
        /// Тест удаления вида работ из базы данных
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteTest()
        {

            _output.WriteLine($"Входные параметры метода: отсутствуют");

            var input = await _workTypeRepository.Create($"Вид работ {DateTime.Now:yyyy.MM.dd HH:mm:ss ff}");

            await _workTypeRepository.Delete(input.Id);

            var expected = (await _workTypeRepository.GetAll()).Where(p => p.Id == input.Id)
                                                               .SingleOrDefault();
            expected.Should().BeNull();
        }

        #endregion
    }
}
