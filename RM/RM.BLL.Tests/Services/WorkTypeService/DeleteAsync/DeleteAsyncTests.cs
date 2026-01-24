using Infrastructure.Testing.TestCases;
using Infrastructure.Testing.XUnit;
using Moq;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Exceptions;
using RM.BLL.Tests.TestSupport.Constants;

namespace RM.BLL.Tests.Services.WorkTypeService.DeleteAsync;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeService.DeleteAsync"/>.
/// </summary>
public class DeleteAsyncTests: BaseTest<WorkTypeServiceFixture>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="DeleteAsyncTests"/>.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования сервиса вида работ.</param>
    public DeleteAsyncTests(WorkTypeServiceFixture fixture) : base(fixture){}

    /// <summary>
    /// Проверяет, что метод <see cref="IWorkTypeService.DeleteAsync"/> успешно удаляет вид работ по его ИД.
    /// </summary>
    [Theory]
    [MemberData(nameof(DeleteAsyncTestCases.SuccessTestCases), 
                MemberType = typeof(DeleteAsyncTestCases))]
    public async Task SucceedsForValidInput(TestCaseInputWithStubs<WorkTypeDeletionModel> testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[(RepositoryMethodNames.WorkTypeRepository.GetByIdAsync,
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<DAL.Abstractions.Models.WorkTypeModel>();

        _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
                                       .ReturnsAsync(stubOutputData);

        _fixture.WorkTypeRepositoryMock.Setup(p => p.DeleteAsync(It.IsAny<Guid>()))
                                       .Returns(Task.CompletedTask);

        // Act & Assert: 
        await _fixture.WorkTypeService.DeleteAsync(testCase.InputData);
    }

    /// <summary>
    /// Проверяет, что метод <see cref="IWorkTypeService.DeleteAsync"/> неуспешно 
    /// удаляет вид работ по его ИД, которого нет в БД.
    /// </summary>
    [Theory]
    [MemberData(nameof(DeleteAsyncTestCases.UnSuccessTestCases), 
                MemberType = typeof(DeleteAsyncTestCases))]
    public async Task FailsForNonExistingId(TestCaseInputWithStubs<WorkTypeDeletionModel> testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[(RepositoryMethodNames.WorkTypeRepository.GetByIdAsync,
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<DAL.Abstractions.Models.WorkTypeModel>();

        _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
                                       .ReturnsAsync(stubOutputData);

        // Act & Assert: 
        await Assert.ThrowsAsync<DataNotFoundException>(
            async () => await _fixture.WorkTypeService.DeleteAsync(testCase.InputData));
    }
}
