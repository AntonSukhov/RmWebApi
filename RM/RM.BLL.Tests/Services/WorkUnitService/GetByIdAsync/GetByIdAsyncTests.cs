using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using Infrastructure.Testing.XUnit;
using Moq;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Tests.Services.WorkUnitService.GetByIdAsync;

/// <summary>
/// Тесты для метода <see cref="IWorkUnitService.GetByIdAsync"/>
/// </summary>
public class GetByIdAsyncTests: BaseTest<WorkUnitServiceFixture>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="GetByIdAsyncTests"/>.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования сервиса единиц работ.</param>
    public GetByIdAsyncTests (WorkUnitServiceFixture fixture) : base(fixture){}

    /// <summary>
    /// Проверяет, что метод <see cref="IWorkUnitService.GetByIdAsync"/>
    /// успешно получает единицу работ по ИД.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetByIdAsyncTestCases.SuccessTestCases),
                MemberType = typeof(GetByIdAsyncTestCases))]
    public async Task SucceedsForValidInput(
        TestCaseWithStubs<short, WorkUnitModel?> testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[new StubOutputKey(
            RepositoryMethodNames.WorkUnitRepository.GetByIdAsync,
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<WorkUnitEntity>();

        _fixture.WorkUnitRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<short>()))
                                       .ReturnsAsync(stubOutputData);

        // Act:
        var result = await _fixture.WorkUnitService.GetByIdAsync(testCase.InputData);

        // Assert:
        Assert.Equal(result, testCase.OutputData, _fixture.WorkUnitModelEqualityComparer);
    }
}
