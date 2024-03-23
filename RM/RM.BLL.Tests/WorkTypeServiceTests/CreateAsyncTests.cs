using Moq;
using RM.BLL.Abstractions.Services;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Tests.TestData;
using RM.BLL.Validators;
using RM.DAL.Abstractions.Repositories;
using RM.Tests.Common.TestData;

namespace RM.BLL.Tests.WorkTypeServiceTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeService.CreateAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования сервиса видов работ.</param>
public class CreateAsyncTests(WorkTypeServiceFixture fixture) : IClassFixture<WorkTypeServiceFixture>
{
     #region Поля

    private readonly Mock<IWorkTypeRepository> _repositoryMock = fixture.WorkTypeRepositoryMock;
    private readonly IWorkTypeValidator _workTypeValidator = fixture.WorkTypeValidator;

    #endregion

     #region Методы

    /// <summary>
    /// Тест создания вида работ для корректных данных.
    /// </summary>
    //[Theory]
    // [MemberData(nameof(WorkTypeServiceTestData.CreateAsyncForCorrectDataTestData), 
    //             MemberType = typeof(WorkTypeServiceTestData))]
    public async Task ForCorrectDataTest(string workTypeName, byte? workUnitId)
    {
        _repositoryMock.Setup(p => p.GetAllAsync(It.IsAny<DAL.Abstractions.Models.PageOptionsModel?>()))
                       .ReturnsAsync(DataSourceTestData.WorkTypes);

        //var workTypeService = new WorkTypeService(_repositoryMock.Object, _workTypeValidator, _pageOptionsValidator);

        // var expected = await workTypeService.GetAllAsync(pageOptionsModel);

        // expected.Should().BeEquivalentTo(DataSourceTestData.WorkTypes.Select(p => p.ToBll()));
    }

    /// <summary>
    /// Тест создания вида работ для некорректных данных.
    /// </summary>
    // [Theory]
    // [MemberData(nameof(WorkTypeServiceTestData.CreateAsyncForIncorrectDataTestData), 
    //             MemberType = typeof(WorkTypeServiceTestData))]
    public async Task ForIncorrectDataTest(string workTypeName, byte? workUnitId)
    {
        // _repositoryMock.Setup(p => p.GetAllAsync(It.IsAny<DAL.Abstractions.Models.PageOptionsModel?>()))
        //                .ReturnsAsync(DataSourceTestData.WorkTypes);

        // var workTypeService = new WorkTypeService(_repositoryMock.Object, _workTypeValidator, _pageOptionsValidator);

        // var expected = await workTypeService.GetAllAsync(pageOptionsModel);

        // expected.Should().BeEquivalentTo(DataSourceTestData.WorkTypes.Select(p => p.ToBll()));
    }

    #endregion
}
