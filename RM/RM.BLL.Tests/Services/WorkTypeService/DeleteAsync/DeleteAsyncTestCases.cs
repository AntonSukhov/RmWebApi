using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Tests.Services.WorkTypeService.DeleteAsync;

/// <summary>
/// Набор тестовых сценариев для проверки метода <see cref="IWorkTypeService.DeleteAsync"/>.
/// </summary>
public static class DeleteAsyncTestCases
{
    private static readonly Guid _workTypeId = Guid.NewGuid();

    /// <summary>
    /// Получает сценарии успешного выполнения метода <see cref="IWorkTypeService.DeleteAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseInputWithStubs<WorkTypeDeletionModel>> SuccessTestCases
    {
        get
        {
            var theoryData = new TheoryData<TestCaseInputWithStubs<WorkTypeDeletionModel>>
            {
                new() {
                    ScenarioNumber = 1,
                    Description = "Проверка успешного удаления вида работ по его значению ИД, который есть в БД.",
                    InputData = new WorkTypeDeletionModel { Id = _workTypeId },
                    StubOutputs =new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.WorkTypeRepository.GetByIdAsync, 
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData =  new WorkTypeEntity
                            { 
                                Id = _workTypeId , Name = "WorkType201"
                            },
                            ExpectedType = typeof(WorkTypeEntity)
                        }
                    }
                }
            };

            return theoryData;
        }
    }

    /// <summary>
    /// Получает сценарии неуспешного выполнения метода <see cref="IWorkTypeService.DeleteAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseInputWithStubs<WorkTypeDeletionModel>> UnSuccessTestCases
    {
        get
        {
            var theoryData = new TheoryData<TestCaseInputWithStubs<WorkTypeDeletionModel>>
            {
                new() {
                    ScenarioNumber = 1,
                    Description = "Проверка не успешного удаления вида работ по пустому значению его ИД, которого нет в БД.",
                    InputData = new WorkTypeDeletionModel { Id = Guid.Empty },
                    StubOutputs =new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.WorkTypeRepository.GetByIdAsync, 
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData =  default,
                            ExpectedType = typeof(WorkTypeEntity)
                        }
                    }
                },
                new() {
                    ScenarioNumber = 2,
                    Description = "Проверка не успешного удаления вида работ по не пустому значению его ИД, которого нет в БД.",
                    InputData = new WorkTypeDeletionModel { Id = Guid.NewGuid() },
                    StubOutputs =new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.WorkTypeRepository.GetByIdAsync, 
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData =  default,
                            ExpectedType = typeof(WorkTypeEntity)
                        }
                    }
                }
            };

            return theoryData;
        }
    }
}
