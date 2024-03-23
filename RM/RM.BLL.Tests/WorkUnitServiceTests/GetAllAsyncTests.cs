using FluentAssertions;
using Moq;
using RM.BLL.Abstractions.Services;
using RM.BLL.Extensions;
using RM.BLL.Services;
using RM.DAL.Abstractions.Repositories;
using RM.Tests.Common.TestData;

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
        _repositoryMock.Setup(p => p.GetAllAsync())
                       .ReturnsAsync(DataSourceTestData.WorkUnits);

        var workUnitService = new WorkUnitService(_repositoryMock.Object);

        var expected = await workUnitService.GetAllAsync();

        expected.Should().BeEquivalentTo(DataSourceTestData.WorkUnits.Select(p => p.ToBll()));
    }

    #endregion
}
