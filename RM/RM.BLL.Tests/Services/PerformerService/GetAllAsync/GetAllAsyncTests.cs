using Moq;
using RM.BLL.Abstractions.Services;
using RM.BLL.Abstractions.Models;
using Infrastructure.Testing.XUnit;
using Infrastructure.Testing.TestCases;
using RM.BLL.Tests.TestSupport.Constants;
using RM.BLL.Exceptions;
using Infrastructure.Testing.Common;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Tests.Services.PerformerService.GetAllAsync;

/// <summary>
/// Тесты для метода <see cref="IPerformerService.GetAllAsync"/>.
/// </summary>
public class GetAllAsyncTests : BaseTest<PerformerServiceFixture>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="GetAllAsyncTests"/>.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования сервиса исполнителей договоров.</param>
    public GetAllAsyncTests (PerformerServiceFixture fixture) : base(fixture){}

    /// <summary>
    /// Проверяет, что метод <see cref="IPerformerService.GetAllAsync"/> успешно получает исполнителей договоров.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetAllAsyncTestCases.SuccessTestCases),
                MemberType = typeof(GetAllAsyncTestCases))]
    public async Task SucceedsForValidRequest(TestCaseWithStubs<PageOptionsModel?, IReadOnlyCollection<PerformerModel>>
        testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[new StubOutputKey(
            RepositoryMethodNames.PerformerRepository.GetAllAsync,
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<IReadOnlyCollection<PerformerEntity>>()?? [];

        _fixture.PerformerRepositoryMock.Setup(p => p.GetAllAsync(It.IsAny<Infrastructure.Shared.Models.PageOptionsModel>()))
                                       .ReturnsAsync(stubOutputData);

        // Act:
        var results = await _fixture.PerformerService.GetAllAsync(testCase.InputData);

        // Assert:
        Assert.Equal(results, testCase.OutputData, _fixture.PerformerModelEqualityComparer);
    }

    /// <summary>
    /// Проверяет, что метод <see cref="IPerformerService.GetAllAsync"/> неуспешно получает исполнителей договоров.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetAllAsyncTestCases.UnSuccessTestCases),
                MemberType = typeof(GetAllAsyncTestCases))]
    public async Task FailsForInvalidRequest(TestCaseInput<PageOptionsModel> testCase)
    {
        // Arrange & Act & Assert:
        var exception = await Assert.ThrowsAnyAsync<Exception>(
            async () => await _fixture.PerformerService.GetAllAsync(testCase.InputData)
        );

        // Проверяем, что исключение относится к разрешённым типам
        Assert.True(
            exception is ValidationException ||
            exception is ValidationAggregationException
        );
    }
}
