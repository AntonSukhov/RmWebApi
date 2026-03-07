using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Tests.Services.WorkTypeService.GetByIdAsync;

/// <summary>
/// Набор тестовых сценариев для проверки метода <see cref="IWorkTypeService.GetByIdAsync"/>.
/// </summary>
public static class GetByIdAsyncTestCases
{
    private static readonly Guid _workTypeId = Guid.NewGuid();

    /// <summary>
    /// Получает сценарии успешного выполнения метода <see cref="IWorkTypeService.GetByIdAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseWithStubs<WorkTypeGettingByIdModel, WorkTypeModel?>> SuccessTestCases
    {
        get
        {
            var theoryData = new TheoryData<TestCaseWithStubs<WorkTypeGettingByIdModel, WorkTypeModel?>>
            {
                new() {
                    ScenarioNumber = 1,
                    Description = "Проверка успешного получения вида работ по не пустому значению ИД.",
                    InputData = new WorkTypeGettingByIdModel { Id = _workTypeId},
                    OutputData = new WorkTypeModel 
                    { 
                        Id = _workTypeId, Name = "WorkType1", 
                        WorkUnit = new WorkUnitModel { Id = 1, Name = "WorkUnit1"} 
                    },
                    StubOutputs =new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.WorkTypeRepository.GetByIdAsync, 
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData =  new WorkTypeEntity 
                            { 
                                    Id = _workTypeId, Name = "WorkType1", WorkUnitId = 1, 
                                    WorkUnit = new WorkUnitEntity 
                                    { 
                                        Id = 1, Name = "WorkUnit1"
                                    }
                            },
                            ExpectedType = typeof(WorkTypeEntity)
                        }
                    }
                },
                new() {
                    ScenarioNumber = 2,
                    Description = "Проверка получения Null по пустому значению ИД.",
                    InputData = new WorkTypeGettingByIdModel { Id = Guid.Empty},
                    OutputData = default,
                    StubOutputs =new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.WorkTypeRepository.GetByIdAsync, 
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = null,
                            ExpectedType = typeof(WorkTypeEntity)
                        }
                    }
                },
                new() {
                    ScenarioNumber = 3,
                    Description = "Проверка получения Null по не пустому значению ИД и которого нет в БД.",
                    InputData = new WorkTypeGettingByIdModel { Id = Guid.NewGuid()},
                    OutputData = default,
                    StubOutputs =new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.WorkTypeRepository.GetByIdAsync, 
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = null,
                            ExpectedType = typeof(WorkTypeEntity)
                        }
                    }
                }
            };

            return theoryData;
        }
    }        
}
