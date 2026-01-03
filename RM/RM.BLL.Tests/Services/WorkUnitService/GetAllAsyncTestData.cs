using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using RM.BLL.Abstractions.Services;
using RM.BLL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Tests.Services.WorkUnitService;

/// <summary>
/// Тестовые данные для проверки метода <see cref="IWorkUnitService.GetAllAsync"/>.
/// </summary>
public static class GetAllAsyncTestData
{
    /// <summary>
    /// Набор тестовых данных для позитивных сценариев вызова метода.
    /// </summary>
    public static TheoryData<TestCaseResultWithStubs<IEnumerable<WorkUnitModel>>> CorrectTestData 
        => 
        [
            new TestCaseResultWithStubs<IEnumerable<WorkUnitModel>>
            {
                ScenarioNumber = 1,
                Description = "Проверка успешного получения списка всех рабочих единиц из репозитория.",
                OutputData = 
                [
                    new WorkUnitModel { Id = 1, Name = "WorkUnit1"},
                    new WorkUnitModel { Id = 2, Name = "WorkUnit2"} ,
                    new WorkUnitModel { Id = 3, Name = "WorkUnit3"} 
                ],
                StubOutputs = new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(nameof(IWorkUnitRepository.GetAllAsync), 1)] = new StubOutput
                    {
                        OutputData =  new List<DAL.Abstractions.Models.WorkUnitModel>
                        {
                            new() { Id = 1, Name = "WorkUnit1"},
                            new() { Id = 2, Name = "WorkUnit2"} ,
                            new() { Id = 3, Name = "WorkUnit3"} 
                        }
                
                    }
                }
            } 
        ];

}
