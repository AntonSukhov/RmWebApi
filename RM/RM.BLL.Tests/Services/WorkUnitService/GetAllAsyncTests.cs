using Infrastructure.Testing.TestCases;
using Infrastructure.Testing.XUnit;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Tests.Services.WorkUnitService;

/// <summary>
/// Тесты для метода <see cref="IWorkUnitService.GetAllAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования сервиса единиц работ.</param>
public class GetAllAsyncTests : BaseTest<WorkUnitServiceFixture>
{
    public GetAllAsyncTests(WorkUnitServiceFixture fixture): base(fixture){ }

    /// <summary>
    /// Тест получения всех единиц работ для корректных данных из источника данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetAllAsyncTestData.CorrectTestData), 
                MemberType = typeof(GetAllAsyncTestData))]
    public async Task ForCorrectTestData(TestCaseResultWithStubs<IEnumerable<WorkUnitModel>> testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[(nameof(IWorkUnitRepository.GetAllAsync), 1)];
        var stubOutputData = stubOutput.GetOutputData<List<DAL.Abstractions.Models.WorkUnitModel>>();

        _fixture.WorkUnitRepositoryMock.Setup(p => p.GetAllAsync())
                                       .Returns(Task.FromResult<
                                            IReadOnlyCollection<DAL.Abstractions.Models.WorkUnitModel>>(stubOutputData));

        // Act:
        var result = await _fixture.WorkUnitService.GetAllAsync();

        // Assert:   
        Assert.Equal(result, testCase.OutputData, _fixture.WorkUnitModelEqualityComparer);
    }

}
