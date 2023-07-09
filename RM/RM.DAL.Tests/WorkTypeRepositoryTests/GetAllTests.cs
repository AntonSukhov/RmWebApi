using FluentAssertions;
using RM.DAL.Abstractions.Models.Pagination;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Base;
using RM.DAL.Tests.Fixtures;
using RM.DAL.Tests.TestData;
using Xunit.Abstractions;

namespace RM.DAL.Tests.WorkTypeRepositoryTests
{
    /// <summary>
    /// Тесты для метода <see cref="IWorkTypeRepository.GetAll"/>
    /// </summary>
    public class GetAllTests : TestBase, IClassFixture<WorkTypeRepositoryFixture>
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
        public GetAllTests(WorkTypeRepositoryFixture repositoryFixture, ITestOutputHelper output) : base(output)
        {
            _workTypeRepository = repositoryFixture?.WorkTypeRepository ?? throw new ArgumentNullException(nameof(repositoryFixture));
        }

        #endregion

        #region Методы

        /// <summary>
        /// Тест получения видов работ из базы данных
        /// </summary>
        /// <returns></returns>
        [Theory]
        [MemberData(nameof(BaseTestData.GetPageOptions), MemberType = typeof(BaseTestData))]
        public async Task GetAllTest(PageOptionsModel pageOptions)
        {
            var pageNumberValueString = pageOptions?.PageNumber.ToString() ?? _missingValue;
            var pageSizeValueString = pageOptions?.PageSize.ToString() ?? _missingValue;

            _output.WriteLine($"Входные параметры метода:{Environment.NewLine}{nameof(PageOptionsModel.PageNumber)} = {pageNumberValueString}{Environment.NewLine}{nameof(PageOptionsModel.PageSize)} = {pageSizeValueString}");

            var expected = await _workTypeRepository.GetAll(pageOptions);

            expected.Should().NotBeNull()
                             .And
                             .HaveCountGreaterThan(0);
        }

        #endregion
    }
}
