using FluentAssertions;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;
using RM.Tests.Common.TestData;

namespace RM.DAL.Tests.WorkUnitRepositoryTests;

/// <summary>
/// Тесты для метода <see cref="IWorkUnitRepository.GetAllAsync"/>.
/// </summary>
public class GetAllTests : IClassFixture<WorkUnitRepositoryFixture>
{
    #region Поля

    private readonly WorkUnitRepositoryFixture _fixture;
    
    #endregion

    #region Конструкторы

    
    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования репозитория единиц работ.</param>
    public GetAllTests(WorkUnitRepositoryFixture fixture)
    {
        _fixture = fixture;
    }

    #endregion

    #region Методы

    /// <summary>
    /// Тест получения всех единиц работ из источника данных MS SQL.
    /// </summary>
    [Fact]
    public async Task FromMsSql()
    {
        await GetAllTest(_fixture.WorkUnitRepositoryMsSql);
    }

    /// <summary>
    /// Тест получения всех единиц работ из источника данных PostgreSQL.
    /// </summary>
    [Fact]
    public async Task FromPostgreSql()
    {
        await GetAllTest(_fixture.WorkUnitRepositoryPostgreSql);
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

        expected.Should().Equal(DataSourceTestData.WorkUnits, (e, a) => e.Id == a.Id && 
                                                                        e.Name == a.Name);
    }
    
    #endregion

    #endregion
}
