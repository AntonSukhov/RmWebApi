using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;
using RM.Tests.Common.TestData;

namespace RM.DAL.Tests.WorkTypeRepositoryTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeRepository.GetAllAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования репозитория видов работ.</param>
public class GetAllAsyncTests(WorkTypeRepositoryFixture fixture) : IClassFixture<WorkTypeRepositoryFixture>
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
    /// Тест получения видов работ для корректных настроек страницы из 
    /// источника данных MS SQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(PaginationTestData.GetCorrectPageOptions), 
                MemberType = typeof(PaginationTestData))]
    public async Task ForCorrectPageOptionsFromMsSql(PageOptionsModel? pageOptions)
    {
        var expected = await _repositoryMsSql.GetAllAsync(pageOptions);

        expected.Should().NotBeNull()
                         .And
                         .HaveCountGreaterThan(0);
    }

    /// <summary>
    /// Тест получения видов работ для корректных настроек страницы из 
    /// источника данных PostgreSQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(PaginationTestData.GetCorrectPageOptions), 
                MemberType = typeof(PaginationTestData))]
    public async Task ForCorrectPageOptionsFromPostgreSql(PageOptionsModel? pageOptions)
    {
        var expected = await _repositoryPostgreSql.GetAllAsync(pageOptions);

        expected.Should().NotBeNull()
                         .And
                         .HaveCountGreaterThan(0);
    }

    /// <summary>
    /// Тест получения видов работ для корректных настроек страницы из 
    /// источника данных SQLite в памяти.
    /// </summary>
    [Theory]
    [MemberData(nameof(PaginationTestData.GetCorrectPageOptions), 
                MemberType = typeof(PaginationTestData))]
    public async Task ForCorrectPageOptionsFromSqliteInMemory(PageOptionsModel? pageOptions)
    {
        var expected = await _repositorySqliteInMemory.GetAllAsync(pageOptions);

        expected.Should().NotBeNull()
                         .And
                         .HaveCount(DataSourceTestData.WorkTypes.Count());
    }

    /// <summary>
    /// Тест получения видов работ для некорректных настроек страницы из 
    /// источника данных MS SQL.
    /// </summary>
    [Fact]
    public async Task ForIncorrectPageOptionsFromMsSql()
    {
        await ForIncorrectPageOptions(_repositoryMsSql);
    }

    /// <summary>
    /// Тест получения видов работ для некорректных настроек страницы из 
    /// источника данных PostgreSQL.
    /// </summary>
    [Fact]
    public async Task ForIncorrectPageOptionsFromPostgreSql()
    {
        await ForIncorrectPageOptions(_repositoryPostgreSql);
    }

    /// <summary>
    /// Тест получения видов работ для некорректных настроек страницы из 
    /// источника данных SQLite в памяти.
    /// </summary>
    [Fact]
    public async Task ForIncorrectPageOptionsFromSqliteInMemory()
    {
        var expectedRowCount = 0;

        foreach (var pageOptions in PaginationTestData.GetIncorrectPageOptions())
        {
            async Task<IEnumerable<WorkTypeModel>> action() => await _repositorySqliteInMemory.GetAllAsync(pageOptions);
            var expected = await action();
            expectedRowCount += expected.Count();
        }

        expectedRowCount.Should().Be(DataSourceTestData.WorkTypes.Count() * 2);
    }

    #region Закрытые методы

    /// <summary>
    /// Тест получения видов работ для некорректных настроек страницы.
    /// </summary>
    /// <param name="repository">Репозиторий вида работ.</param>
    /// <returns/>
    private static async Task ForIncorrectPageOptions(IWorkTypeRepository repository)
    {
        var errors = new List<Exception>();

        foreach (var pageOptions in PaginationTestData.GetIncorrectPageOptions())
        {
            try
            {
                async Task<IEnumerable<WorkTypeModel>> action() => await repository.GetAllAsync(pageOptions);
                var expected = await action();
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }
        }

        errors.Should().HaveCount(4);
    }

    #endregion

    #endregion
}
