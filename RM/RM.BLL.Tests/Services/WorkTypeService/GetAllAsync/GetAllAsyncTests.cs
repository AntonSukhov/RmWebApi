using Moq;
using RM.BLL.Abstractions.Services;
using RM.BLL.Abstractions.Models;
using Infrastructure.Testing.XUnit;
using Infrastructure.Testing.TestCases;
using RM.BLL.Tests.TestSupport.Constants;
using RM.BLL.Exceptions;

namespace RM.BLL.Tests.Services.WorkTypeService.GetAllAsync;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeService.GetAllAsync"/>.
/// </summary>
public class GetAllAsyncTests : BaseTest<WorkTypeServiceFixture>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="GetAllAsyncTests"/>.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования сервиса вида работ.</param>
    public GetAllAsyncTests (WorkTypeServiceFixture fixture) : base(fixture){}

    /// <summary>
    /// Проверяет, что метод <see cref="IWorkTypeService.GetAllAsync"/> успешно получает виды работ.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetAllAsyncTestCases.SuccessTestCases), 
                MemberType = typeof(GetAllAsyncTestCases))]
    public async Task SucceedsForValidRequest(TestCaseWithStubs<PageOptionsModel?, IReadOnlyCollection<WorkTypeModel>> 
        testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[(RepositoryMethodNames.WorkTypeRepository.GetAllAsync, 
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<IReadOnlyCollection<DAL.Abstractions.Models.WorkTypeModel>>();

        _fixture.WorkTypeRepositoryMock.Setup(p => p.GetAllAsync(It.IsAny<DAL.Abstractions.Models.PageOptionsModel?>()))
                                       .ReturnsAsync(stubOutputData);
                                       
        // Act:         
        var results = await _fixture.WorkTypeService.GetAllAsync(testCase.InputData);

        // Assert: 
        Assert.Equal(results, testCase.OutputData, _fixture.WorkTypeModelEqualityComparer);
    }

    /// <summary>
    /// Проверяет, что метод <see cref="IWorkTypeService.GetAllAsync"/> неуспешно получает виды работ.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetAllAsyncTestCases.UnSuccessTestCases), 
                MemberType = typeof(GetAllAsyncTestCases))]
    public async Task FailsForInvalidRequest(TestCaseInput<PageOptionsModel> testCase)
    {
        // Arrange & Act & Assert:
        var exception = await Assert.ThrowsAnyAsync<Exception>(
            async () => await _fixture.WorkTypeService.GetAllAsync(testCase.InputData)
        );

        // Проверяем, что исключение относится к разрешённым типам
        Assert.True(
            exception is ValidationException || 
            exception is ValidationAggregationException
        );
    }

}
