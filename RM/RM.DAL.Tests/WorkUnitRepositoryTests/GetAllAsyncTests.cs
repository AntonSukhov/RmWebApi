using FluentAssertions;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;
using RM.Tests.Common.TestData;

namespace RM.DAL.Tests.WorkUnitRepositoryTests;

/// <summary>
/// Тесты для метода <see cref="IWorkUnitRepository.GetAllAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования репозитория единиц работ.</param>
public class GetAllTests(WorkUnitRepositoryFixture fixture) : IClassFixture<WorkUnitRepositoryFixture>
{
    #region Поля

    /// <summary>
    /// Репозиторий единицы работ, работающий с MS SQL.
    /// </summary>
    private readonly IWorkUnitRepository _repositoryMsSql = fixture.WorkUnitRepositoryMsSql;

    /// <summary>
    /// Репозиторий единицы работ, работающий с PostgreSQL.
    /// </summary>
    private readonly IWorkUnitRepository _repositoryPostgreSql = fixture.WorkUnitRepositoryPostgreSql;

    /// <summary>
    /// Репозиторий единицы работ, работающий с SQLite в памяти.
    /// </summary>
    private readonly IWorkUnitRepository _repositorySqliteInMemory = fixture.WorkUnitRepositorySqliteInMemory;
    
    #endregion

    #region Методы

    /// <summary>
    /// Тест получения всех единиц работ из источника данных MS SQL.
    /// </summary>
    [Fact]
    public async Task FromMsSql()
    {
        await GetAllTest(_repositoryMsSql);
    }

    /// <summary>
    /// Тест получения всех единиц работ из источника данных PostgreSQL.
    /// </summary>
    [Fact]
    public async Task FromPostgreSql()
    {
        await GetAllTest(_repositoryPostgreSql);
    }

    /// <summary>
    /// Тест получения всех единиц работ из источника данных SQLite в памяти.
    /// </summary>
    [Fact]
    public async Task FromSqliteInMemory()
    {
        await GetAllTest(_repositorySqliteInMemory);
    }

    #region Закрытые методы

    /// <summary>
    /// Тест получения всех единиц работ из источника данных.
    /// </summary>
    /// <param name="repository">Репозиторий единицы работ.</param>
    /// <returns/>
    private static async Task GetAllTest(IWorkUnitRepository repository)
    {
        var expected = await repository.GetAllAsync();

        expected.Should().BeEquivalentTo(DataSourceTestData.WorkUnits);
    }
    
    #endregion

    #endregion
}
