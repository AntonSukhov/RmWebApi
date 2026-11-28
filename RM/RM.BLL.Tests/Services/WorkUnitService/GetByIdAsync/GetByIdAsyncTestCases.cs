using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Tests.Services.WorkUnitService.GetByIdAsync;

/// <summary>
/// Набор тестовых сценариев для проверки метода <see cref="IWorkUnitService.GetByIdAsync"/>.
/// </summary>
public static class GetByIdAsyncTestCases
{
    private const short WorkUnitId = 1;

    /// <summary>
    /// Получает сценарии успешного выполнения метода <see cref="IWorkUnitService.GetByIdAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseWithStubs<short, WorkUnitModel?>> SuccessTestCases
    {
        get
        {
            var theoryData = new TheoryData<TestCaseWithStubs<short, WorkUnitModel?>>
            {
                new() {
                    ScenarioNumber = 1,
                    Description = "Проверка успешного получения единицы работ по не пустому значению ИД.",
                    InputData = WorkUnitId,
                    OutputData = new WorkUnitModel
                    {
                        Id = WorkUnitId, Name = "WorkUnit1"
                    },
                    StubOutputs = new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync,
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = new WorkUnitEntity
                            {
                                Id = WorkUnitId, Name = "WorkUnit1"
                            },
                            ExpectedType = typeof(WorkUnitEntity)
                        }
                    }
                },
                new() {
                    ScenarioNumber = 2,
                    Description = "Проверка получения Null по пустому значению ИД.",
                    InputData = 0,
                    OutputData = default,
                    StubOutputs = new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync,
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = null,
                            ExpectedType = typeof(WorkUnitEntity)
                        }
                    }
                },
                new() {
                    ScenarioNumber = 3,
                    Description = "Проверка получения Null по не пустому значению ИД и которого нет в БД.",
                    InputData = WorkUnitId,
                    OutputData = default,
                    StubOutputs = new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync,
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = null,
                            ExpectedType = typeof(WorkUnitEntity)
                        }
                    }
                }
            };

            return theoryData;
        }
    }
}
