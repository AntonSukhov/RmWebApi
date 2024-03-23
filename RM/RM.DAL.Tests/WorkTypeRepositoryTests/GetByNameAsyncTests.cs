using FluentAssertions;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;

namespace RM.DAL.Tests.WorkTypeRepositoryTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeRepository.GetByNameAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования репозитория видов работ.</param>
public class GetByNameAsyncTests: IClassFixture<WorkTypeRepositoryFixture>
{
    #region Поля

    private readonly WorkTypeRepositoryFixture _fixture;

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования репозитория видов работ.</param>
    public GetByNameAsyncTests(WorkTypeRepositoryFixture fixture)
    {
        _fixture = fixture;
    }

    #endregion

    #region Методы

    /// <summary>
    /// Тест получения вида работ по его названию для существующего вида работ в 
    /// источнике данных MS SQL.
    /// </summary>
    [Fact]
    public async Task ForExistedWorkTypeInMsSql()
    {     
        await ForExistedWorkType(_fixture.WorkTypeRepositoryMsSql);
    }

    /// <summary>
    /// Тест получения вида работ по его названию для существующего вида работ в 
    /// источнике данных PostgreSQL.
    /// </summary>
    [Fact]
    public async Task ForExistedWorkTypeInPostgreSql()
    {     
        await ForExistedWorkType(_fixture.WorkTypeRepositoryPostgreSql);
    }

    /// <summary>
    /// Тест получения вида работ по его названию для несуществующего вида работ в
    /// источнике данных MS SQL.
    /// </summary>
    [Fact]
    public async Task ForNotExistedWorkTypeInMsSql()
    {     
        await ForNotExistedWorkType(_fixture.WorkTypeRepositoryMsSql);
    }

    /// <summary>
    /// Тест получения вида работ по его названию для несуществующего вида работ в
    /// источнике данных PostgreSQL.
    /// </summary>
    [Fact]
    public async Task ForNotExistedWorkTypeInPostgreSql()
    {     
        await ForNotExistedWorkType(_fixture.WorkTypeRepositoryPostgreSql);
    }

    #region Закрытые методы

    /// <summary>
    /// Тест получения вида работ по его названию для существующего в источнике данных вида работ.
    /// </summary>
    /// <param name="repository">Репозиторий вида работ.</param>
    /// <returns/>
    private static async Task ForExistedWorkType(IWorkTypeRepository repository)
    {     
        var actual = (await repository.GetAllAsync()).FirstOrDefault();

        var expected = await repository.GetByNameAsync(actual?.Name);

        expected.Should().BeEquivalentTo(actual);
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
