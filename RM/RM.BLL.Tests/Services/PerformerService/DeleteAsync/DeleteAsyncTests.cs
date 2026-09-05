using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using Infrastructure.Testing.XUnit;
using Moq;
using RM.BLL.Abstractions.Services;
using RM.BLL.Exceptions;
using RM.BLL.Tests.TestSupport.Constants;

namespace RM.BLL.Tests.Services.PerformerService.DeleteAsync;

/// <summary>
/// Тесты для метода <see cref="IPerformerService.DeleteAsync"/>.
/// </summary>
public class DeleteAsyncTests: BaseTest<PerformerServiceFixture>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="DeleteAsyncTests"/>.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования сервиса исполнителей договоров.</param>
    public DeleteAsyncTests(PerformerServiceFixture fixture) : base(fixture){}

    /// <summary>
    /// Проверяет, что метод <see cref="IPerformerService.DeleteAsync"/> успешно удаляет исполнителя договоров по его ИД.
    /// </summary>
    [Theory]
    [MemberData(nameof(DeleteAsyncTestCases.SuccessTestCases), 
                MemberType = typeof(DeleteAsyncTestCases))]
    public async Task SucceedsForValidInput(TestCaseInputWithStubs<Guid> testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[new StubOutputKey(
            RepositoryMethodNames.PerformerRepository.DeleteAsync,
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<int>();

        _fixture.PerformerRepositoryMock.Setup(p => p.DeleteAsync(It.IsAny<Guid>()))
                                       .ReturnsAsync(stubOutputData);

        // Act & Assert: 
        await _fixture.PerformerService.DeleteAsync(testCase.InputData);
    }

    /// <summary>
    /// Проверяет, что метод <see cref="IPerformerService.DeleteAsync"/> неуспешно 
    /// удаляет исполнителя договоров по его ИД, которого нет в БД.
    /// </summary>
    [Theory]
    [MemberData(nameof(DeleteAsyncTestCases.UnSuccessTestCases), 
                MemberType = typeof(DeleteAsyncTestCases))]
    public async Task FailsForNonExistingId(TestCaseInputWithStubs<Guid> testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[new StubOutputKey(
            RepositoryMethodNames.PerformerRepository.DeleteAsync,
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<int>();

        _fixture.PerformerRepositoryMock.Setup(p => p.DeleteAsync(It.IsAny<Guid>()))
                                       .ReturnsAsync(stubOutputData);

        // Act & Assert: 
        await Assert.ThrowsAsync<DataNotFoundException>(
            async () => await _fixture.PerformerService.DeleteAsync(testCase.InputData));
    }
}
