using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using Infrastructure.Testing.XUnit;
using Moq;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Tests.Services.PerformerService.GetByIdAsync;

/// <summary>
/// Тесты для метода <see cref="IPerformerService.GetByIdAsync"/>
/// </summary>
public class GetByIdAsyncTests: BaseTest<PerformerServiceFixture>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="GetByIdAsyncTests"/>.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования сервиса исполнителей договоров.</param>
    public GetByIdAsyncTests (PerformerServiceFixture fixture) : base(fixture){}

    /// <summary>
    /// Проверяет, что метод <see cref="IPerformerService.GetByIdAsync"/>
    /// успешно получает исполнителя договоров по ИД.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetByIdAsyncTestCases.SuccessTestCases),
                MemberType = typeof(GetByIdAsyncTestCases))]
    public async Task SucceedsForValidInput(
        TestCaseWithStubs<Guid, PerformerModel?> testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[new StubOutputKey(
            RepositoryMethodNames.PerformerRepository.GetByIdAsync,
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<PerformerEntity>();

        _fixture.PerformerRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
                                       .ReturnsAsync(stubOutputData);

        // Act:
        var result = await _fixture.PerformerService.GetByIdAsync(testCase.InputData);

        // Assert:
        Assert.Equal(result, testCase.OutputData, _fixture.PerformerModelEqualityComparer);
    }
}
