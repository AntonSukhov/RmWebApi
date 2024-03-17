using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;
using RM.DAL.Tests.TestData;

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

    /// <summary>
    /// Репозиторий вида работ, работающий с SQLite в памяти.
    /// </summary>
    private readonly IWorkTypeRepository _repositorySqliteInMemory = fixture.WorkTypeRepositorySqliteInMemory;

    #endregion

    #region Методы

    /// <summary>
    /// Тест получения вида работ по его названию для существующего вида работ в 
    /// источнике данных MS SQL.
    /// </summary>
    [Fact]
    public async Task ForExistedWorkTypeInMsSql()
    {     
        await ForExistedWorkType(_repositoryMsSql);
    }

    /// <summary>
    /// Тест получения вида работ по его названию для существующего вида работ в 
    /// источнике данных PostgreSQL.
    /// </summary>
    [Fact]
    public async Task ForExistedWorkTypeInPostgreSql()
    {     
        await ForExistedWorkType(_repositoryPostgreSql);
    }

    /// <summary>
    /// Тест получения вида работ по его названию для существующего вида работ в
    /// источнике данных SQLite в памяти.
    /// </summary>
    [Fact]
    public async Task ForExistedWorkTypeInSqliteInMemory()
    {     
        var workType = DataBaseTestData.WorkTypes.FirstOrDefault()?? 
                       new WorkTypeModel();

        var expected = await _repositorySqliteInMemory.GetByNameAsync(workType.Name);

        expected.Should().NotBeNull()
                         .And
                         .Match<WorkTypeModel>(p => p.Id == workType.Id && 
                                                    p.Name == workType.Name && 
                                                    p.WorkUnitId == workType.WorkUnitId);
    }


    /// <summary>
    /// Тест получения вида работ по его названию для несуществующего вида работ в
    /// источнике данных MS SQL.
    /// </summary>
    [Fact]
    public async Task ForNotExistedWorkTypeInMsSql()
    {     
        await ForNotExistedWorkType(_repositoryMsSql);
    }

    /// <summary>
    /// Тест получения вида работ по его названию для несуществующего вида работ в
    /// источнике данных PostgreSQL.
    /// </summary>
    [Fact]
    public async Task ForNotExistedWorkTypeInPostgreSql()
    {     
        await ForNotExistedWorkType(_repositoryPostgreSql);
    }

    /// <summary>
    /// Тест получения вида работ по его названию для несуществующего вида работ в 
    /// источнике данных SQLite в памяти.
    /// </summary>
    [Fact]
    public async Task ForNotExistedWorkTypeInSqliteInMemory()
    {     
        await ForNotExistedWorkType(_repositorySqliteInMemory);
    }

    #region Закрытые методы

    /// <summary>
    /// Тест получения вида работ по его названию для существующего в источнике данных вида работ.
    /// </summary>
    /// <param name="repository">Репозиторий вида работ.</param>
    /// <returns/>
    private static async Task ForExistedWorkType(IWorkTypeRepository repository)
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
    private static async Task ForNotExistedWorkType(IWorkTypeRepository repository)
    {     
        var expected = await repository.GetByNameAsync($"WorkTypeName{Guid.NewGuid()}");

        expected.Should().BeNull();
    }

    #endregion

    #endregion
}
