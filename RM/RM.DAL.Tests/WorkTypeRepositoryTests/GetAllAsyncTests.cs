using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;

namespace RM.DAL.Tests.WorkTypeRepositoryTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeRepository.GetAllAsync"/>.
/// </summary>
public class GetAllAsyncTests : IClassFixture<WorkTypeRepositoryFixture>
{
    #region Поля

    private readonly WorkTypeRepositoryFixture _fixture;

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования репозитория видов работ.</param>
    public GetAllAsyncTests(WorkTypeRepositoryFixture fixture)
    {
        _fixture = fixture;
    }
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
        var expected = await _fixture.WorkTypeRepositoryMsSql.GetAllAsync(pageOptions);

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
        var expected = await _fixture.WorkTypeRepositoryPostgreSql.GetAllAsync(pageOptions);

        expected.Should().NotBeNull()
                         .And
                         .HaveCountGreaterThan(0);
    }

    /// <summary>
    /// Тест получения видов работ для некорректных настроек страницы из 
    /// источника данных MS SQL.
    /// </summary>
    [Fact]
    public async Task ForIncorrectPageOptionsFromMsSql()
    {
        await ForIncorrectPageOptions(_fixture.WorkTypeRepositoryMsSql);
    }

    /// <summary>
    /// Тест получения видов работ для некорректных настроек страницы из 
    /// источника данных PostgreSQL.
    /// </summary>
    [Fact]
    public async Task ForIncorrectPageOptionsFromPostgreSql()
    {
        await ForIncorrectPageOptions(_fixture.WorkTypeRepositoryPostgreSql);
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
                await action();
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
