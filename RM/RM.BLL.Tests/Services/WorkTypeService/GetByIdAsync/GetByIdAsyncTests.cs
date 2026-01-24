using Infrastructure.Testing.TestCases;
using Infrastructure.Testing.XUnit;
using Moq;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;

namespace RM.BLL.Tests.Services.WorkTypeService.GetByIdAsync;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeService.GetByIdAsync"/>
/// </summary>
public class GetByIdAsyncTests: BaseTest<WorkTypeServiceFixture>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="GetByIdAsyncTests"/>.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования сервиса вида работ.</param>
    public GetByIdAsyncTests (WorkTypeServiceFixture fixture) : base(fixture){}

    /// <summary>
    /// Проверяет, что метод <see cref="IWorkTypeService.GetByIdAsync"/> 
    /// успешно получает вид работ по ИД.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetByIdAsyncTestCases.SuccessTestCases), 
                MemberType = typeof(GetByIdAsyncTestCases))]
    public async Task SucceedsForValidInput(
        TestCaseWithStubs<WorkTypeGettingByIdModel, WorkTypeModel?> testCase)
    {       
        // Arrange:
        var stubOutput = testCase.StubOutputs[(RepositoryMethodNames.WorkTypeRepository.GetByIdAsync, 
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<DAL.Abstractions.Models.WorkTypeModel>();

        _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
                                       .ReturnsAsync(stubOutputData);
                                       
        // Act:         
        var result = await _fixture.WorkTypeService.GetByIdAsync(testCase.InputData);

        // Assert: 
        Assert.Equal(result, testCase.OutputData, _fixture.WorkTypeModelEqualityComparer);
    }
}
