using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;

namespace RM.DAL.Tests.WorkUnitRepositoryTests;

/// <summary>
/// Тесты для метода <see cref="IWorkUnitRepository.GetAllAsync"/>.
/// </summary>
public class GetAllTests : IClassFixture<WorkUnitRepositoryFixture>
{
    #region Поля

    /// <summary>
    /// Репозиторий единицы работ.
    /// </summary>
    private readonly IWorkUnitRepository _repository;

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="fixture"></param>
    public GetAllTests(WorkUnitRepositoryFixture fixture)
    {
        _repository = fixture?.WorkUnitRepository ?? throw new ArgumentNullException(nameof(fixture));
    }

    #endregion

    #region Методы

    /// <summary>
    /// Тест получения всех единиц работ из источника данных.
    /// </summary>
    [Fact]
    public async Task GetAllTest()
    {
        var expected = await _repository.GetAllAsync();

        expected.Should().BeEquivalentTo(new[]
        {
            new WorkUnitModel { Id = 1, Name = "машина" },
            new WorkUnitModel { Id = 2, Name = "шт." },
            new WorkUnitModel { Id = 3, Name = "Кв.м." }
        });
    }

    #endregion
}
