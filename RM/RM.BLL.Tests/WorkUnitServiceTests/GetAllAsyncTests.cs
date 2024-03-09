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
/// <param name="fixture">Настройка контекста для тестирования сервиса единиц работ.</param>
public class GetAllAsyncTests(WorkUnitServiceFixture fixture) : IClassFixture<WorkUnitServiceFixture>
{
    #region Поля

    private readonly Mock<IWorkUnitRepository> _repositoryMock = fixture.WorkUnitRepositoryMock;

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
