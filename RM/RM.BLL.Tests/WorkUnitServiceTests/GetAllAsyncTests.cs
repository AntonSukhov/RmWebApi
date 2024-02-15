using FluentAssertions;
using Moq;
using RM.BLL.Abstractions.Services;
using RM.BLL.Converters;
using RM.BLL.Services;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Tests.WorkUnitServiceTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeService.GetAllAsync"/>.
/// </summary>
public class GetAllAsyncTests : IClassFixture<WorkUnitServiceFixture>
{
    #region Поля

    private readonly Mock<IWorkUnitRepository> _repositoryMock;

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="fixture"></param>
    public GetAllAsyncTests(WorkUnitServiceFixture fixture)
    {
        _repositoryMock = fixture?.WorkUnitRepositoryMock ?? throw new ArgumentNullException(nameof(fixture));
    }

    #endregion

    #region Методы

    /// <summary>
    /// Тест получения всех единиц работ для корректных данных из источника данных.
    /// </summary>
    [Fact]
    public async Task GetAllAsyncForCorrectDataTest()
    {
        var workUnits = new []{ new WorkUnitModel { Id = 1, Name = "машина" },
                                new WorkUnitModel { Id = 2, Name = "шт." },
                                new WorkUnitModel { Id = 3, Name = "Кв.м." }};

        _repositoryMock.Setup(p => p.GetAllAsync())
                       .ReturnsAsync(workUnits);

        var workUnitService = new WorkUnitService(_repositoryMock.Object);

        var expected = await workUnitService.GetAllAsync();

        expected.Should().BeEquivalentTo(workUnits.Select(WorkUnitConverter.ConvertDalToBllModel));
    }

    #endregion
}
