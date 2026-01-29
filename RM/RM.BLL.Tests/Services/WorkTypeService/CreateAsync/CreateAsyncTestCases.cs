using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;

namespace RM.BLL.Tests.Services.WorkTypeService.CreateAsync;

/// <summary>
/// Набор тестовых сценариев для проверки метода <see cref="IWorkTypeService.CreateAsync"/>.
/// </summary>
public static class CreateAsyncTestCases
{
    /// <summary>
    /// Получает сценарии успешного выполнения метода <see cref="IWorkTypeService.CreateAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseInputWithStubs<WorkTypeCreationModel>> SuccessTestCases
        =>
        [
            new TestCaseInputWithStubs<WorkTypeCreationModel>
            {
                ScenarioNumber = 1,
                Description = "Проверка успешного создания вида работ с указанием ИД единицы работ.",
                InputData = new WorkTypeCreationModel { Name = "WorkType1", WorkUnitId = 1},
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  new DAL.Abstractions.Models.WorkUnitModel { Id = 1, Name = "WorkUnit1"},
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkUnitModel)
                    }
                }
            },
            new TestCaseInputWithStubs<WorkTypeCreationModel>
            {
                ScenarioNumber = 2,
                Description = "Проверка успешного создания вида работ без указания ИД единицы работ.",
                InputData = new WorkTypeCreationModel { Name = "WorkType2" },
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  null,
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkUnitModel)
                    }
                }
            },
            new TestCaseInputWithStubs<WorkTypeCreationModel>
            {
                ScenarioNumber = 3,
                Description = "Проверка успешного создания вида работ с максимально допустимой длиной названия.",
                InputData = new WorkTypeCreationModel { Name = new string('n', 200) },
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(RepositoryMethodNames.WorkUnitRepository.GetByIdAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  null,
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkUnitModel)
                    }
                }
            },
        ];

    /// <summary>
    /// Получает сценарии неуспешного выполнения метода <see cref="IWorkTypeService.CreateAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseInputWithStubs<WorkTypeCreationModel>> UnSuccessTestCases
        =>
        [
            new TestCaseInputWithStubs<WorkTypeCreationModel>
            {
                ScenarioNumber = 1,
                Description = "Проверка неуспешного создания вида работ с указанием Названия вида работ равного Null.",
                InputData = new WorkTypeCreationModel { Name = null, WorkUnitId = 1},
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
                        OutputData =  new DAL.Abstractions.Models.WorkUnitModel { Id = 1, Name = "WorkUnit1"},
                        ExpectedType = typeof(DAL.Abstractions.Models.WorkUnitModel)
                    }
                }
            },
            new TestCaseInputWithStubs<WorkTypeCreationModel>
            {
                ScenarioNumber = 2,
                Description = "Проверка неуспешного создания вида работ с указанием Названия вида работ равного Empty.",
                InputData = new WorkTypeCreationModel { Name = string.Empty },
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
            new TestCaseInputWithStubs<WorkTypeCreationModel>
            {
                ScenarioNumber = 3,
                Description = "Проверка неуспешного создания вида работ с длиной названия, превышающую максимально допустимую длину.",
                InputData = new WorkTypeCreationModel { Name = new string('n', 201) },
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
            new TestCaseInputWithStubs<WorkTypeCreationModel>
            {
                ScenarioNumber = 4,
                Description = "Проверка неуспешного создания вида работ с несуществующим значением ИД единицы работ.",
                InputData = new WorkTypeCreationModel { Name = "WorkType1", WorkUnitId = byte.MaxValue },
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
            new TestCaseInputWithStubs<WorkTypeCreationModel>
            {
                ScenarioNumber = 5,
                Description = "Проверка неуспешного создания вида работ с уже существующим названием вида работ.",
                InputData = new WorkTypeCreationModel { Name = "WorkType101" },
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(RepositoryMethodNames.WorkTypeRepository.GetByNameAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  new DAL.Abstractions.Models.WorkTypeModel{ Id = Guid.NewGuid(), Name = "WorkType101"},
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
        ];
}
