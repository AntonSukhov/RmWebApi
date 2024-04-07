using FluentAssertions;
using Moq;
using RM.BLL.Extensions;
using RM.BLL.Tests.TestData;
using RM.BLL.Abstractions.Services;
using RM.Tests.Common.TestData;
using RM.BLL.Abstractions.Models;

namespace RM.BLL.Tests.WorkTypeServiceTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeService.GetAllAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования сервиса видов работ.</param>
public class GetAllAsyncTests(WorkTypeServiceFixture fixture) : IClassFixture<WorkTypeServiceFixture>
{
    #region Поля

    private readonly WorkTypeServiceFixture _fixture = fixture;

    #endregion

    #region Методы

    /// <summary>
    /// Тест получения всех единиц работ для корректных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(PaginationTestData.GetCorrectPageOptions), 
                MemberType = typeof(PaginationTestData))]
    public async Task ForCorrectDataTest(PageOptionsModel? pageOptionsModel)
    {
        _fixture.WorkTypeRepositoryMock.Setup(p => p.GetAllAsync(It.IsAny<DAL.Abstractions.Models.PageOptionsModel?>()))
                                       .ReturnsAsync(DataSourceTestData.WorkTypes);

        var workTypeService = new WorkTypeService(_fixture.WorkTypeRepositoryMock.Object, 
                                                  _fixture.WorkUnitRepositoryMock.Object,
                                                  _fixture.WorkTypeNameValidator,
                                                  _fixture.WorkTypeUpdationModelValidator, 
                                                  _fixture.PageOptionsValidator);

        var expected = await workTypeService.GetAllAsync(pageOptionsModel);

        expected.Should().BeEquivalentTo(DataSourceTestData.WorkTypes.Select(p => p.ToBll()));
    }

     /// <summary>
    /// Тест получения всех единиц работ для корректных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(PaginationTestData.GetIncorrectPageOptions),
                MemberType = typeof(PaginationTestData))]
    public async Task ForIncorrectDataTest(PageOptionsModel pageOptionsModel)
    {
        _fixture.WorkTypeRepositoryMock.Setup(p => p.GetAllAsync(It.IsAny<DAL.Abstractions.Models.PageOptionsModel?>()))
                                       .Throws(() => new Exception("Repository Exception"));

        var workTypeService = new WorkTypeService(_fixture.WorkTypeRepositoryMock.Object, 
                                                  _fixture.WorkUnitRepositoryMock.Object,
                                                  _fixture.WorkTypeNameValidator,
                                                  _fixture.WorkTypeUpdationModelValidator,
                                                  _fixture.PageOptionsValidator);

        var expected = async () => await workTypeService.GetAllAsync(pageOptionsModel);

        await expected.Should().ThrowAsync<Exception>();
    }

    #endregion
}
