using FluentAssertions;
using Moq;
using RM.BLL.Converters;
using RM.BLL.Tests.TestData;
using RM.BLL.Validators;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using PageOptionsModel = RM.BLL.Abstractions.Models.PageOptionsModel;

namespace RM.BLL.Tests.WorkTypeServiceTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeService.GetAllAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования сервиса видов работ.</param>
public class GetAllAsyncTests(WorkTypeServiceFixture fixture) : IClassFixture<WorkTypeServiceFixture>
{
    #region Поля

    private readonly Mock<IWorkTypeRepository> _repositoryMock = fixture.WorkTypeRepositoryMock;
    private readonly WorkTypeValidator _workTypeValidator = fixture.WorkTypeValidator;
    private readonly PageOptionsValidator _pageOptionsValidator = fixture.PageOptionsValidator;

    #endregion

    #region Методы

    /// <summary>
    /// Тест получения всех единиц работ для корректных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(PaginationTestData.GetCorrectPageOptions), 
                MemberType = typeof(PaginationTestData))]
    public async Task GetAllAsyncForCorrectDataTest(PageOptionsModel? pageOptionsModel)
    {
        var workTypes = new []{ new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 1" },
                                new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 2", WorkUnitId = 1, WorkUnit = new WorkUnitModel { Id = 1, Name = "машина"} },
                                new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 3", WorkUnitId = 2, WorkUnit = new WorkUnitModel { Id = 2, Name = "шт."} },
                                new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 4", WorkUnitId = 3, WorkUnit = new WorkUnitModel { Id = 3, Name = "Кв.м."} }
                            };

        var repositoryMock = new Mock<IWorkTypeRepository>();
        var workTypeValidator = new WorkTypeValidator();
        var pageOptionsValidator = new PageOptionsValidator();

        repositoryMock.Setup(p => p.GetAllAsync(It.IsAny<DAL.Abstractions.Models.PageOptionsModel?>()))
                      .ReturnsAsync(workTypes);

        var workTypeService = new WorkTypeService(repositoryMock.Object, workTypeValidator, pageOptionsValidator);

        var expected = await workTypeService.GetAllAsync(pageOptionsModel);

        expected.Should().BeEquivalentTo(workTypes.Select(WorkTypeConverter.ConvertDalToBllModel));
    }

     /// <summary>
    /// Тест получения всех единиц работ для корректных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(PaginationTestData.GetIncorrectPageOptions),
                MemberType = typeof(PaginationTestData))]
    public async Task GetAllAsyncForIncorrectDataTest(PageOptionsModel pageOptionsModel)
    {
        _repositoryMock.Setup(p => p.GetAllAsync(It.IsAny<DAL.Abstractions.Models.PageOptionsModel?>()))
                       .Throws(() => new Exception("Repository Exception"));

        var workTypeService = new WorkTypeService(_repositoryMock.Object, _workTypeValidator, _pageOptionsValidator);

        var action = async () => await workTypeService.GetAllAsync(pageOptionsModel);

        await action.Should().ThrowAsync<Exception>();
    }

    #endregion
}
