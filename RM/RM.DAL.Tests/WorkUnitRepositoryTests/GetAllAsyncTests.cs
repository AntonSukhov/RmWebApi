using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;

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

    #endregion

    #region Методы

    /// <summary>
    /// Тест получения всех единиц работ из источника данных. MS SQL.
    /// </summary>
    [Fact(Skip = "На Linux нельзя установить MS SQL Server, поэтому отключил тест.")]
    public async Task GetAllTestMsSql()
    {
        await GetAllTest(_repositoryMsSql);
    }

    /// <summary>
    /// Тест получения всех единиц работ из источника данных. PostgreSQL.
    /// </summary>
    [Fact]
    public async Task GetAllTestPostgreSql()
    {
        await GetAllTest(_repositoryPostgreSql);
    }

    #region Закрытые методы

    /// <summary>
    /// Тест получения всех единиц работ из источника данных.
    /// </summary>
    /// <param name="repository">Репозиторий единицы работ.</param>
    /// <returns/>
    private async Task GetAllTest(IWorkUnitRepository repository)
    {
        var expected = await repository.GetAllAsync();

        expected.Should().BeEquivalentTo(new[]
        {
            new WorkUnitModel { Id = 1, Name = "машина" },
            new WorkUnitModel { Id = 2, Name = "шт." },
            new WorkUnitModel { Id = 3, Name = "Кв.м." }
        });
    }

    #endregion

    #endregion
}
