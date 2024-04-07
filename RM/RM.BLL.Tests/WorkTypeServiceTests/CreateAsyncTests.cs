using FluentAssertions;
using Moq;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestData;

namespace RM.BLL.Tests.WorkTypeServiceTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeService.CreateAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования сервиса видов работ.</param>
public class CreateAsyncTests(WorkTypeServiceFixture fixture) : IClassFixture<WorkTypeServiceFixture>
{
    #region Поля

    private readonly WorkTypeServiceFixture _fixture = fixture;

    #endregion

    #region Методы

    /// <summary>
    /// Тест создания вида работ для корректных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeServiceTestData.CreateAsyncForCorrectDataTestData), 
                MemberType = typeof(WorkTypeServiceTestData))]
    public async Task ForCorrectDataTest(WorkTypeCreationModel workTypeCreationModel)
    {
     
        _fixture.WorkTypeRepositoryMock.Setup(p => p.CreateAsync(It.IsAny<DAL.Abstractions.Models.WorkTypeShortModel>()))
                                       .Returns(Task.CompletedTask);

        _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByNameAsync(It.IsAny<string>()))
                                       .Returns(Task.FromResult<DAL.Abstractions.Models.WorkTypeModel?>(null));

        var workUnitModel = workTypeCreationModel.WorkUnitId.HasValue? new DAL.Abstractions.Models.WorkUnitModel
        {
            Id = workTypeCreationModel.WorkUnitId.Value
        }: null;

        _fixture.WorkUnitRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<byte>()))
                                       .Returns(Task.FromResult(workUnitModel));                            
                       
        var workTypeService = new WorkTypeService(_fixture.WorkTypeRepositoryMock.Object,
                                                  _fixture.WorkUnitRepositoryMock.Object, 
                                                  _fixture.WorkTypeNameValidator, 
                                                  _fixture.WorkTypeUpdationModelValidator,
                                                  _fixture.PageOptionsValidator);

        var expected = await workTypeService.CreateAsync(workTypeCreationModel);

        expected.Should().NotBeEmpty();
    }

    /// <summary>
    /// Тест создания вида работ для некорректных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeServiceTestData.CreateAsyncForIncorrectDataTestData), 
                MemberType = typeof(WorkTypeServiceTestData))]
    public async Task ForIncorrectDataTest(WorkTypeCreationModel workTypeCreationModel)
    {
        _fixture.WorkTypeRepositoryMock.Setup(p => p.CreateAsync(It.IsAny<DAL.Abstractions.Models.WorkTypeShortModel>()))
                                       .Throws(() => new Exception("Repository Exception"));
        
        _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByNameAsync(It.IsAny<string>()))
                                       .Returns(Task.FromResult<DAL.Abstractions.Models.WorkTypeModel?>(null));

        _fixture.WorkUnitRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<byte>()))
                                       .Returns(Task.FromResult<DAL.Abstractions.Models.WorkUnitModel?>(null));                            
                       
        var workTypeService = new WorkTypeService(_fixture.WorkTypeRepositoryMock.Object,
                                                  _fixture.WorkUnitRepositoryMock.Object, 
                                                  _fixture.WorkTypeNameValidator, 
                                                  _fixture.WorkTypeUpdationModelValidator,
                                                  _fixture.PageOptionsValidator);

        var expected = async() => await workTypeService.CreateAsync(workTypeCreationModel);

        await expected.Should().ThrowAsync<Exception>();
    }

    #endregion
}
