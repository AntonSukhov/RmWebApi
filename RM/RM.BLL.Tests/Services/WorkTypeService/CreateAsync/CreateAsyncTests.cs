using Infrastructure.Testing.TestCases;
using Infrastructure.Testing.XUnit;
using Moq;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Exceptions;
using RM.BLL.Tests.TestSupport.Constants;

namespace RM.BLL.Tests.Services.WorkTypeService.CreateAsync;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeService.CreateAsync"/>.
/// </summary>
public class CreateAsyncTests : BaseTest<WorkTypeServiceFixture>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="CreateAsyncTests"/>.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования сервиса вида работ.</param>
    public CreateAsyncTests(WorkTypeServiceFixture fixture) : base(fixture) { }

    /// <summary>
    /// Проверяет, что метод <see cref="IWorkTypeService.CreateAsync"/> успешно создает вид работ.
    /// </summary>
    [Theory]
    [MemberData(nameof(CreateAsyncTestCases.SuccessTestCases),
                MemberType = typeof(CreateAsyncTestCases))]
    public async Task SucceedsForValidInput(TestCaseInputWithStubs<WorkTypeCreationModel> testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync,
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<DAL.Abstractions.Models.WorkUnitModel?>();

        _fixture.WorkTypeRepositoryMock.Setup(p => p.CreateAsync(It.IsAny<DAL.Abstractions.Models.WorkTypeShortModel>()))
                                       .Returns(Task.CompletedTask);

        _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByNameAsync(It.IsAny<string>()))
                                       .ReturnsAsync((DAL.Abstractions.Models.WorkTypeModel?)null);

        _fixture.WorkUnitRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<byte>()))
                                       .ReturnsAsync(stubOutputData);

        // Act:         
        var result = await _fixture.WorkTypeService.CreateAsync(testCase.InputData);

        // Assert: 
        Assert.NotEqual(result, Guid.Empty);
    }

    /// <summary>
    /// Проверяет, что метод <see cref="IWorkTypeService.CreateAsync"/> не создает вид работ из-за некорретных входных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(CreateAsyncTestCases.UnSuccessTestCases),
                MemberType = typeof(CreateAsyncTestCases))]
    public async Task FailsForInvalidInput(
        TestCaseInputWithStubs<WorkTypeCreationModel> testCase)
    {
        // Arrange:
        var firstStubOutput = testCase.StubOutputs[(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync,
            StubSequenceConstants.First)];
        var firstStubOutputData = firstStubOutput.GetOutputData<DAL.Abstractions.Models.WorkUnitModel?>();
        var secondStubOutput = testCase.StubOutputs[(RepositoryMethodNames.WorkTypeRepository.GetByNameAsync,
            StubSequenceConstants.First)];
        var secondStubOutputData = secondStubOutput.GetOutputData<DAL.Abstractions.Models.WorkTypeModel?>();

        _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByNameAsync(It.IsAny<string>()))
                                       .ReturnsAsync(secondStubOutputData);

        _fixture.WorkUnitRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<byte>()))
                                       .ReturnsAsync(firstStubOutputData);

        // Act & Assert: 
        var exception = await Assert.ThrowsAnyAsync<Exception>(
            async () => await _fixture.WorkTypeService.CreateAsync(testCase.InputData)
        );

        // Проверяем, что исключение относится к разрешённым типам
        Assert.True(
            exception is ValidationException || 
            exception is ValidationAggregationException ||
            exception is DataNotFoundException
        );
    }

}
