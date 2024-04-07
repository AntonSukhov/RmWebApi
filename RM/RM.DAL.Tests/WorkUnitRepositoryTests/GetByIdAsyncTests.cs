using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;

namespace RM.DAL.Tests.WorkUnitRepositoryTests;

/// <summary>
/// Тесты для метода <see cref="IWorkUnitRepository.GetByIdAsync"/>.
/// </summary>
public class GetByIdAsyncTests: IClassFixture<WorkUnitRepositoryFixture>
{
    #region Поля

    private readonly WorkUnitRepositoryFixture _fixture;
    
    #endregion
    
    #region Конструкторы

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования репозитория
    /// единиц работ.</param>
    public GetByIdAsyncTests(WorkUnitRepositoryFixture fixture)
    {
        _fixture = fixture;
    }
    
    #endregion
    
    #region Методы

    /// <summary>
    /// Тест получения единицы работ по её ИД для существующей единицы работ из 
    /// источнике данных MS SQL.
    /// </summary>
    [Fact]
    public async Task ForExistedWorkUnitFromMsSql()
    {     
        await ForExistedWorkUnit(_fixture.WorkUnitRepositoryMsSql);
    }

    /// <summary>
    /// Тест получения единицы работ по её ИД для существующей единицы работ из 
    /// источнике данных PostgreSQL.
    /// </summary>
    [Fact]
    public async Task ForExistedWorkUnitFromPostgreSql()
    {     
        await ForExistedWorkUnit(_fixture.WorkUnitRepositoryPostgreSql);
    }


    /// <summary>
    /// Тест получения единицы работ по её ИД для несуществующей единицы работ из 
    /// источнике данных MS SQL.
    /// </summary>
    [Fact]
    public async Task ForNotExistedWorkUnitFromMsSql()
    {     
        await ForNotExistedWorkUnit(_fixture.WorkUnitRepositoryMsSql);
    }

    /// <summary>
    /// Тест получения единицы работ по её ИД для несуществующей единицы работ из 
    /// источнике данных PostgreSQL.
    /// </summary>
    [Fact]
    public async Task ForNotExistedWorkUnitFromPostgreSql()
    {     
        await ForNotExistedWorkUnit(_fixture.WorkUnitRepositoryPostgreSql);
    }

    #region Закрытые методы

    /// <summary>
    /// Тест получения единицы работ по его ИД для существующей в источнике данных
    /// единицы работ.
    /// </summary>
    /// <param name="repository">Репозиторий единиц работ.</param>
    /// <returns/>
    private static async Task ForExistedWorkUnit(IWorkUnitRepository repository)
    {     
        var actual = (await repository.GetAllAsync()).FirstOrDefault()??
                       new WorkUnitModel{ Id = 0 };

        var expected = await repository.GetByIdAsync(actual.Id);

        expected.Should().BeEquivalentTo(actual);
    }

    /// <summary>
    /// Тест получения единицы работ по его ИД для несуществующей в источнике данных
    /// единицы работ.
    /// </summary>
    /// <param name="repository">Репозиторий единиц работ.</param>
    /// <returns/>
    private static async Task ForNotExistedWorkUnit(IWorkUnitRepository repository)
    {     
        var expected = await repository.GetByIdAsync(byte.MaxValue);

        expected.Should().BeNull();
    }

    #endregion

    #endregion
}
