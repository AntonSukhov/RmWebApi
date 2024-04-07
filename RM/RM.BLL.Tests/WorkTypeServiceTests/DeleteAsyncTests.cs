using FluentAssertions;
using Moq;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestData;

namespace RM.BLL.Tests.WorkTypeServiceTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeService.DeleteAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования сервиса видов работ.</param>
public class DeleteAsyncTests(WorkTypeServiceFixture fixture) : IClassFixture<WorkTypeServiceFixture>
{
    #region Поля

    private readonly WorkTypeServiceFixture _fixture = fixture;

    #endregion

    #region Методы

    /// <summary>
    /// Тест удаления вида работ для корректных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeServiceTestData.DeleteAsyncForCorrectDataTestData), 
                MemberType = typeof(WorkTypeServiceTestData))]
    public async Task ForCorrectDataTest(WorkTypeDeletionModel workTypeDeletionModel)
    {
     
        _fixture.WorkTypeRepositoryMock.Setup(p => p.DeleteAsync(It.IsAny<Guid>()))
                                       .Returns(Task.CompletedTask);
                   
        var workTypeService = new WorkTypeService(_fixture.WorkTypeRepositoryMock.Object, 
                                                  _fixture.WorkUnitRepositoryMock.Object,
                                                  _fixture.WorkTypeNameValidator,
                                                  _fixture.WorkTypeUpdationModelValidator, 
                                                  _fixture.PageOptionsValidator);

        var expected = async () => await workTypeService.DeleteAsync(workTypeDeletionModel);

        await expected.Should().NotThrowAsync<Exception>();
    }

    /// <summary>
    /// Тест удаления вида работ для некорректных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeServiceTestData.DeleteAsyncForIncorrectDataTestData), 
                MemberType = typeof(WorkTypeServiceTestData))]
    public async Task ForIncorrectDataTest(WorkTypeDeletionModel workTypeDeletionModel)
    {
        _fixture.WorkTypeRepositoryMock.Setup(p => p.DeleteAsync(It.IsAny<Guid>()))
                                       .Throws(() => new Exception("Repository Exception"));
                       
        var workTypeService = new WorkTypeService(_fixture.WorkTypeRepositoryMock.Object, 
                                                  _fixture.WorkUnitRepositoryMock.Object,
                                                  _fixture.WorkTypeNameValidator,
                                                  _fixture.WorkTypeUpdationModelValidator, 
                                                  _fixture.PageOptionsValidator);

        var expected = async() => await workTypeService.DeleteAsync(workTypeDeletionModel);

        await expected.Should().ThrowAsync<Exception>();
    }

    #endregion
}
