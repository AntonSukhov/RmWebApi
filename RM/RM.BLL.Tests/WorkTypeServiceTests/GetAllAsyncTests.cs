using FluentAssertions;
using Moq;
using RM.BLL.Extensions;
using RM.BLL.Tests.TestData;
using RM.BLL.Abstractions.Services;
using RM.DAL.Abstractions.Repositories;
using RM.Tests.Common.TestData;
using PageOptionsModel = RM.BLL.Abstractions.Models.PageOptionsModel;
using RM.BLL.Abstractions.Validators;

namespace RM.BLL.Tests.WorkTypeServiceTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeService.GetAllAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования сервиса видов работ.</param>
public class GetAllAsyncTests(WorkTypeServiceFixture fixture) : IClassFixture<WorkTypeServiceFixture>
{
    #region Поля

    private readonly Mock<IWorkTypeRepository> _repositoryMock = fixture.WorkTypeRepositoryMock;
    private readonly IWorkTypeValidator _workTypeValidator = fixture.WorkTypeValidator;
    private readonly IPageOptionsValidator _pageOptionsValidator = fixture.PageOptionsValidator;

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
        _repositoryMock.Setup(p => p.GetAllAsync(It.IsAny<DAL.Abstractions.Models.PageOptionsModel?>()))
                       .ReturnsAsync(DataSourceTestData.WorkTypes);

        var workTypeService = new WorkTypeService(_repositoryMock.Object, _workTypeValidator, _pageOptionsValidator);

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
        _repositoryMock.Setup(p => p.GetAllAsync(It.IsAny<DAL.Abstractions.Models.PageOptionsModel?>()))
                       .Throws(() => new Exception("Repository Exception"));

        var workTypeService = new WorkTypeService(_repositoryMock.Object, _workTypeValidator, _pageOptionsValidator);

        var action = async () => await workTypeService.GetAllAsync(pageOptionsModel);

        await action.Should().ThrowAsync<Exception>();
    }

    #endregion
}
