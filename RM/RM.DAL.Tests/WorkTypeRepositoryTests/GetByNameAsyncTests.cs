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
    /// Репозиторий вида работ.
    /// </summary>
    private readonly IWorkTypeRepository _repository = fixture.WorkTypeRepository;

    #endregion

    #region Методы

    /// <summary>
    /// Тест получения вида работ по его названию для существующего в источнике данных вида работ.
    /// </summary>
    [Fact]
    public async Task ForExistedWorkType()
    {     
        var workType = (await _repository.GetAllAsync()).First();

        var expected = await _repository.GetByNameAsync(workType.Name);

        expected.Should().NotBeNull()
                         .And
                         .Match<WorkTypeModel>(p => p.Id == workType.Id && p.Name == workType.Name && p.WorkUnitId == workType.WorkUnitId);
    }

    /// <summary>
    /// Тест получения вида работ по его названию для несуществующего в источнике данных вида работ.
    /// </summary>
    [Fact]
    public async Task ForNotExistedWorkType()
    {     

        var expected = await _repository.GetByNameAsync($"WorkTypeName{Guid.NewGuid()}");

        expected.Should().BeNull();
    }

    #endregion
}
