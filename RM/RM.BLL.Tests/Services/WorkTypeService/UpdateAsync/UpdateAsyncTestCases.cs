using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;

namespace RM.BLL.Tests.Services.WorkTypeService.UpdateAsync;

/// <summary>
/// Набор тестовых сценариев для проверки метода <see cref="IWorkTypeService.UpdateAsync"/>.
/// </summary>
public static class UpdateAsyncTestCases
{
    private static readonly Guid _workTypeId = Guid.NewGuid();

    /// <summary>
    /// Получает сценарии успешного выполнения метода <see cref="IWorkTypeService.UpdateAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseInputWithStubs<WorkTypeUpdationModel>> SuccessTestCases
        =>
        [
            new TestCaseInputWithStubs<WorkTypeUpdationModel>
            {
                ScenarioNumber = 1,
                Description = "Проверка успешного обновления вида работ. Новое значения ИД единицы работ есть в БД.",
                InputData = new WorkTypeUpdationModel 
                {  
                    Id = _workTypeId, Name = "WorkType1", WorkUnitId = 1
                },
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {

                    [(RepositoryMethodNames.WorkTypeRepository.GetByNameAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  new DAL.Abstractions.Models.WorkTypeModel 
                        { 
                            Id = _workTypeId, Name = "WorkType1", WorkUnitId = 1,
                            WorkUnit = new DAL.Abstractions.Models.WorkUnitModel 
                            { 
                                Id = 1, Name = "WorkUnit1"
                            }
                        },
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkTypeModel)
                    },
                    [(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  new DAL.Abstractions.Models.WorkUnitModel { Id = 1, Name = "WorkUnit1"},
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkUnitModel)
                    }
                }
            },
            new TestCaseInputWithStubs<WorkTypeUpdationModel>
            {
                ScenarioNumber = 2,
                Description = "Проверка успешного обновления вида работ. Новое значения ИД единицы работ равно Null.",
                InputData = new WorkTypeUpdationModel 
                {  
                    Id = _workTypeId, Name = "WorkType1", WorkUnitId = null
                },
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {

                    [(RepositoryMethodNames.WorkTypeRepository.GetByNameAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  new DAL.Abstractions.Models.WorkTypeModel 
                        { 
                            Id = _workTypeId, Name = "WorkType1", WorkUnitId = 1,
                            WorkUnit = new DAL.Abstractions.Models.WorkUnitModel 
                            { 
                                Id = 1, Name = "WorkUnit1"
                            }
                        },
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkTypeModel)
                    },
                    [(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  new DAL.Abstractions.Models.WorkUnitModel { Id = 1, Name = "WorkUnit1"},
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkUnitModel)
                    }
                }
            }
        ];

    /// <summary>
    /// Получает сценарии неуспешного выполнения метода <see cref="IWorkTypeService.UpdateAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseInputWithStubs<WorkTypeUpdationModel>> UnSuccessTestCases
        =>
        [
            new TestCaseInputWithStubs<WorkTypeUpdationModel>
            {
                ScenarioNumber = 1,
                Description = "Проверка неуспешного обновления вида работ.",
                InputData = new WorkTypeUpdationModel(),
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(RepositoryMethodNames.WorkTypeRepository.GetByNameAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  null,
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkTypeModel)
                    },
                    [(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  null,
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkUnitModel)
                    }
                }
            },
            new TestCaseInputWithStubs<WorkTypeUpdationModel>
            {
                ScenarioNumber = 2,
                Description = "Проверка неуспешного обновления вида работ. ИД обновляемого вида работ равно пустому значению.",
                InputData = new WorkTypeUpdationModel { Id = Guid.Empty, Name = "WorkType1", WorkUnitId = null},
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(RepositoryMethodNames.WorkTypeRepository.GetByNameAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  null,
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkTypeModel)
                    },
                    [(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  null,
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkUnitModel)
                    }
                }
            },
            new TestCaseInputWithStubs<WorkTypeUpdationModel>
            {
                ScenarioNumber = 3,
                Description = "Проверка неуспешного обновления вида работ. Новое значение названия вида работ равно пустому значению.",
                InputData = new WorkTypeUpdationModel { Id = _workTypeId, Name = string.Empty, WorkUnitId = null},
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(RepositoryMethodNames.WorkTypeRepository.GetByNameAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  null,
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkTypeModel)
                    },
                    [(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  null,
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkUnitModel)
                    }
                }
            },
            new TestCaseInputWithStubs<WorkTypeUpdationModel>
            {
                ScenarioNumber = 4,
                Description = "Проверка неуспешного обновления вида работ. Длина нового значения названия вида работ больше допустимого.",
                InputData = new WorkTypeUpdationModel { Id = _workTypeId, Name = new string('n', 300), WorkUnitId = null},
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(RepositoryMethodNames.WorkTypeRepository.GetByNameAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  null,
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkTypeModel)
                    },
                    [(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  null,
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkUnitModel)
                    }
                }
            },
            new TestCaseInputWithStubs<WorkTypeUpdationModel>
            {
                ScenarioNumber = 5,
                Description = "Проверка неуспешного обновления вида работ. Новое значение ИД единицы работ отсутствует в БД.",
                InputData = new WorkTypeUpdationModel { Id = _workTypeId, Name = "WorkType1", WorkUnitId = byte.MaxValue},
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(RepositoryMethodNames.WorkTypeRepository.GetByNameAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  new DAL.Abstractions.Models.WorkTypeModel 
                        { 
                            Id = _workTypeId, Name = "WorkType1"
                        }, 
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkTypeModel)
                    },
                    [(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  null,
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkUnitModel)
                    }
                }
            },
            new TestCaseInputWithStubs<WorkTypeUpdationModel>
            {
                ScenarioNumber = 6,
                Description = "Проверка неуспешного обновления вида работ. Вид работ с указанным новым названием уже существует в БД.",
                InputData = new WorkTypeUpdationModel { Id = Guid.NewGuid(), Name = "WorkType1" },
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(RepositoryMethodNames.WorkTypeRepository.GetByNameAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  new DAL.Abstractions.Models.WorkTypeModel 
                        {
                             Id = Guid.NewGuid(), Name = "WorkType1"
                        }, 
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkTypeModel)
                    },
                    [(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  null,
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkUnitModel)
                    }
                }
            }
        ];
}
