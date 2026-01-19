using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using RM.BLL.Abstractions.Services;
using RM.BLL.Abstractions.Models;
using RM.BLL.Tests.TestSupport.Constants;

namespace RM.BLL.Tests.Services.WorkUnitService.GetAllAsync;

/// <summary>
/// Набор тестовых сценариев для проверки метода <see cref="IWorkUnitService.GetAllAsync"/>.
/// </summary>
public static class GetAllAsyncTestCases
{
    private static readonly Type WorkUnitModelListType = typeof(List<DAL.Abstractions.Models.WorkUnitModel>);

    /// <summary>
    /// Получает сценарии успешного выполнения метода <see cref="IWorkUnitService.GetAllAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseResultWithStubs<IEnumerable<WorkUnitModel>>> SuccessTestCases =>
    [
        new TestCaseResultWithStubs<IEnumerable<WorkUnitModel>>
        {
            ScenarioNumber = 1,
            Description = "Проверка успешного получения списка всех рабочих единиц из репозитория.",
            OutputData =
            [
                new() { Id = 1, Name = "WorkUnit1" },
                new() { Id = 2, Name = "WorkUnit2" },
                new() { Id = 3, Name = "WorkUnit3" }
            ],
            StubOutputs = new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
            {
                [(RepositoryMethodNames.WorkUnitRepository.GetAllAsync, 
                    StubSequenceConstants.First)] = new StubOutput
                {
                    OutputData = new List<DAL.Abstractions.Models.WorkUnitModel>
                    {
                        new() { Id = 1, Name = "WorkUnit1" },
                        new() { Id = 2, Name = "WorkUnit2" },
                        new() { Id = 3, Name = "WorkUnit3" }
                    },
                    ExpectedType = WorkUnitModelListType
                }
            }
        }
    ];

    /// <summary>
    /// Получает сценарии ошибок при выполнении метода <see cref="IWorkUnitService.GetAllAsync"/>,
    /// когда репозиторий выбрасывает исключение.
    /// </summary>
    public static TheoryData<Exception> ErrorTestCases => new()
    {
        { new Exception("Database connection failed") },
        { new TimeoutException("Repository timeout")},
        { new InvalidOperationException("Invalid state in repository") }
    };
}
