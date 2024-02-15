using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;

namespace RM.DAL.Tests.WorkTypeRepositoryTests;

public class GetByIdAsyncTests : IClassFixture<WorkTypeRepositoryFixture>
{
    #region Поля

    /// <summary>
    /// Репозиторий вида работ.
    /// </summary>
    private readonly IWorkTypeRepository _repository;

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="fixture"></param>
    public GetByIdAsyncTests(WorkTypeRepositoryFixture fixture)
    {
        _repository = fixture?.WorkTypeRepository ?? throw new ArgumentNullException(nameof(fixture));
    }

    #endregion

    #region Методы

    /// <summary>
    /// Тест получения вида работ по его ИД для существующего в источнике данных вида работ.
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncForExistedWorkTypeTest()
    {     
        var workType = (await _repository.GetAllAsync()).First();

        var expected = await _repository.GetByIdAsync(workType.Id);

        expected.Should().NotBeNull()
                         .And
                         .Match<WorkTypeModel>(p => p.Id == workType.Id && p.Name == workType.Name && p.WorkUnitId == workType.WorkUnitId);
    }

    /// <summary>
    /// Тест получения вида работ по его ИД для несуществующего в источнике данных вида работ.
    /// </summary>
    [Fact]
    public async Task GetByIdAsyncForNotExistedWorkTypeTest()
    {     

        var expected = await _repository.GetByIdAsync(Guid.NewGuid());

        expected.Should().BeNull();
    }

    #endregion
}
