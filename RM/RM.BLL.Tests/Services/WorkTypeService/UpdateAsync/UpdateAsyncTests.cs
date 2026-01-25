using Infrastructure.Testing.TestCases;
using Infrastructure.Testing.XUnit;
using Moq;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Exceptions;
using RM.BLL.Tests.TestSupport.Constants;

namespace RM.BLL.Tests.Services.WorkTypeService.UpdateAsync;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeService.UpdateAsync"/>.
/// </summary>
public class UpdateAsyncTests: BaseTest<WorkTypeServiceFixture>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="UpdateAsyncTests"/>.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования сервиса вида работ.</param>
    public UpdateAsyncTests(WorkTypeServiceFixture fixture) : base(fixture) { }

    /// <summary>
    /// Проверяет, что метод <see cref="IWorkTypeService.UpdateAsync"/> успешно обновляет вид работ.
    /// </summary>
    [Theory]
    [MemberData(nameof(UpdateAsyncTestCases.SuccessTestCases), 
                MemberType = typeof(UpdateAsyncTestCases))]
    public async Task SucceedsForValidInput(TestCaseInputWithStubs<WorkTypeUpdationModel> testCase)
    {
        // Arrange:
        var getByIdAsyncStubOutput = testCase.StubOutputs[(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync,
            StubSequenceConstants.First)];
        var getByIdAsyncStubOutputData = getByIdAsyncStubOutput.GetOutputData<DAL.Abstractions.Models.WorkUnitModel>();
        var getByNameAsyncStubOutput = testCase.StubOutputs[(RepositoryMethodNames.WorkTypeRepository.GetByNameAsync,
            StubSequenceConstants.First)];
        var getByNameAsyncStubOutputData = getByNameAsyncStubOutput.GetOutputData<DAL.Abstractions.Models.WorkTypeModel>();

        _fixture.WorkUnitRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<byte>()))
                                       .ReturnsAsync(getByIdAsyncStubOutputData);
        
        _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByNameAsync(It.IsAny<string>()))
                                       .ReturnsAsync(getByNameAsyncStubOutputData);

        _fixture.WorkTypeRepositoryMock.Setup(p => p.UpdateAsync(It.IsAny<DAL.Abstractions.Models.WorkTypeShortModel>()))
                                       .Returns(Task.CompletedTask);

        // Act & Assert:        
        await _fixture.WorkTypeService.UpdateAsync(testCase.InputData);
    }

    /// <summary>
    ///  Проверяет, что метод <see cref="IWorkTypeService.UpdateAsync"/> неуспешно обновляет вид работ.
    /// </summary>
    [Theory]
    [MemberData(nameof(UpdateAsyncTestCases.UnSuccessTestCases), 
                MemberType = typeof(UpdateAsyncTestCases))]
    public async Task FailsForInvalidInput(TestCaseInputWithStubs<WorkTypeUpdationModel> testCase)
    {

          // Arrange:
        var getByIdAsyncStubOutput = testCase.StubOutputs[(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync,
            StubSequenceConstants.First)];
        var getByIdAsyncStubOutputData = getByIdAsyncStubOutput.GetOutputData<DAL.Abstractions.Models.WorkUnitModel>();
        var getByNameAsyncStubOutput = testCase.StubOutputs[(RepositoryMethodNames.WorkTypeRepository.GetByNameAsync,
            StubSequenceConstants.First)];
        var getByNameAsyncStubOutputData = getByNameAsyncStubOutput.GetOutputData<DAL.Abstractions.Models.WorkTypeModel>();

        _fixture.WorkUnitRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<byte>()))
                                       .ReturnsAsync(getByIdAsyncStubOutputData);
        
        _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByNameAsync(It.IsAny<string>()))
                                       .ReturnsAsync(getByNameAsyncStubOutputData);

        // Act & Assert: 
        var exception = await Assert.ThrowsAnyAsync<Exception>(
            async () => await _fixture.WorkTypeService.UpdateAsync(testCase.InputData)
        );

        // Проверяем, что исключение относится к разрешённым типам
        Assert.True(
            exception is ValidationException || 
            exception is ValidationAggregationException ||
            exception is DataNotFoundException
        );
    }
}
