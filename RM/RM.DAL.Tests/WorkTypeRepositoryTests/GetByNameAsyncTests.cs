using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;

namespace RM.DAL.Tests.WorkTypeRepositoryTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeRepository.GetByNameAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования репозитория видов работ.</param>
public class GetByNameAsyncTests(WorkTypeRepositoryFixture fixture) : IClassFixture<WorkTypeRepositoryFixture>
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
    /// Тест получения вида работ по его названию для существующего в источнике данных вида работ. MS SQL.
    /// </summary>
    [Fact]
    public async Task ForExistedWorkTypeMsSql()
    {     
        await ForExistedWorkType(_repositoryMsSql);
    }

    /// <summary>
    /// Тест получения вида работ по его названию для существующего в источнике данных вида работ. PostgreSQL.
    /// </summary>
    [Fact]
    public async Task ForExistedWorkTypePostgreSql()
    {     
        await ForExistedWorkType(_repositoryPostgreSql);
    }


    /// <summary>
    /// Тест получения вида работ по его названию для несуществующего в источнике данных вида работ. MS SQL.
    /// </summary>
    [Fact]
    public async Task ForNotExistedWorkTypeMsSql()
    {     
        await ForNotExistedWorkType(_repositoryMsSql);
    }

    /// <summary>
    /// Тест получения вида работ по его названию для несуществующего в источнике данных вида работ. PostgreSQL.
    /// </summary>
    [Fact]
    public async Task ForNotExistedWorkTypePostgreSql()
    {     
        await ForNotExistedWorkType(_repositoryPostgreSql);
    }

    #region Закрытые методы

    /// <summary>
    /// Тест получения вида работ по его названию для существующего в источнике данных вида работ.
    /// </summary>
    /// <param name="repository">Репозиторий вида работ.</param>
    /// <returns/>
    private async Task ForExistedWorkType(IWorkTypeRepository repository)
    {     
        var workType = (await repository.GetAllAsync()).First();

        var expected = await repository.GetByNameAsync(workType.Name);

        expected.Should().NotBeNull()
                         .And
                         .Match<WorkTypeModel>(p => p.Id == workType.Id && p.Name == workType.Name && p.WorkUnitId == workType.WorkUnitId);
    }

    /// <summary>
    /// Тест получения вида работ по его названию для несуществующего в источнике данных вида работ.
    /// </summary>
    /// <param name="repository">Репозиторий вида работ.</param>
    /// <returns/>
    private async Task ForNotExistedWorkType(IWorkTypeRepository repository)
    {     
        var expected = await repository.GetByNameAsync($"WorkTypeName{Guid.NewGuid()}");

        expected.Should().BeNull();
    }

    #endregion

    #endregion
}
