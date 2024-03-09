using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;

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

    #endregion

    #region Методы

    /// <summary>
    /// Тест получения видов работ. MS SQL.
    /// </summary>
    [Theory(Skip = "На Linux нельзя установить MS SQL Server, поэтому отключил тест.")]
    [MemberData(nameof(PaginationTestData.GetCorrectPageOptions), MemberType = typeof(PaginationTestData))]
    public async Task ForCorrectPageOptionsMsSql(PageOptionsModel? pageOptions)
    {
        await ForCorrectPageOptions(pageOptions, _repositoryMsSql);
    }

    /// <summary>
    /// Тест получения видов работ. PostgreSQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(PaginationTestData.GetCorrectPageOptions), MemberType = typeof(PaginationTestData))]
    public async Task ForCorrectPageOptionsPostgreSql(PageOptionsModel? pageOptions)
    {
        await ForCorrectPageOptions(pageOptions, _repositoryPostgreSql);
    }

    /// <summary>
    /// Тест получения видов работ для некорректных настроек страницы. MS SQL.
    /// </summary>
    [Fact(Skip = "На Linux нельзя установить MS SQL Server, поэтому отключил тест.")]
    public async Task ForIncorrectPageOptionsMsSql()
    {
        await ForIncorrectPageOptions(_repositoryMsSql);
    }

    /// <summary>
    /// Тест получения видов работ для некорректных настроек страницы. PostgreSQL.
    /// </summary>
    [Fact]
    public async Task ForIncorrectPageOptionsPostgreSql()
    {
        await ForIncorrectPageOptions(_repositoryPostgreSql);
    }

    #region Закрытые методы

    /// <summary>
    /// Тест получения видов работ.
    /// </summary>
    /// <param name="pageOptions">Модель настроек стараницы.</param>
    /// <param name="repository">Репозиторий вида работ.</param>
    /// <returns/>
    private async Task ForCorrectPageOptions(PageOptionsModel? pageOptions, IWorkTypeRepository repository)
    {
        var expected = await repository.GetAllAsync(pageOptions);

        expected.Should().NotBeNull()
                         .And
                         .HaveCountGreaterThan(0);
    }

    /// <summary>
    /// Тест получения видов работ для некорректных настроек страницы.
    /// </summary>
    /// <param name="repository">Репозиторий вида работ.</param>
    /// <returns/>
    private async Task ForIncorrectPageOptions(IWorkTypeRepository repository)
    {
        var errors = new List<Exception>();

        foreach (var pageOptions in PaginationTestData.GetIncorrectPageOptions())
        {
            try
            {
                var action = async () => await repository.GetAllAsync(pageOptions);
                await action.Invoke();
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
