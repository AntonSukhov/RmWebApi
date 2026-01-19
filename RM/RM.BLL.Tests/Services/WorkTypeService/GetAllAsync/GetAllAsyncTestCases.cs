using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;

namespace RM.BLL.Tests.Services.WorkTypeService.GetAllAsync;

/// <summary>
/// Набор тестовых сценариев для проверки метода <see cref="IWorkTypeService.GetAllAsync"/>.
/// </summary>
public static class GetAllAsyncTestCases
{
    private static readonly Guid _workTypeId1 = Guid.NewGuid();
    private static readonly Guid _workTypeId2 = Guid.NewGuid();
    private static readonly Guid _workTypeId3 = Guid.NewGuid();

    /// <summary>
    /// Получает сценарии успешного выполнения метода <see cref="IWorkTypeService.GetAllAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseWithStubs<PageOptionsModel?, IReadOnlyCollection<WorkTypeModel>>> 
        SuccessTestCases =>
        [
            new TestCaseWithStubs<PageOptionsModel?, IReadOnlyCollection<WorkTypeModel>>
            {
                ScenarioNumber = 1,
                Description = "Проверка успешного постраничного получения видов работ.",
                InputData = new PageOptionsModel { PageNumber = 1, PageSize = 100},
                OutputData =
                [
                    new WorkTypeModel 
                    { 
                        Id = _workTypeId1, Name = "WorkType1", 
                        WorkUnit = new WorkUnitModel { Id = 1, Name = "WorkUnit1"} 
                    },
                    new WorkTypeModel 
                    {
                        Id = _workTypeId2, Name = "WorkType2"
                    },
                    new WorkTypeModel 
                    {
                        Id = _workTypeId3, Name = "WorkType3"
                    }
                ],
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(RepositoryMethodNames.WorkTypeRepository.GetAllAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  new []
                        {
                            new DAL.Abstractions.Models.WorkTypeModel 
                            { 
                                Id = _workTypeId1, Name = "WorkType1", WorkUnitId = 1, 
                                WorkUnit = new DAL.Abstractions.Models.WorkUnitModel 
                                { 
                                    Id = 1, Name = "WorkUnit1"
                                }
                            },
                            new DAL.Abstractions.Models.WorkTypeModel 
                            { 
                                Id = _workTypeId2, Name = "WorkType2"
                            },
                            new DAL.Abstractions.Models.WorkTypeModel 
                            { 
                                Id = _workTypeId3, Name = "WorkType3"
                            }
                        },
                        ExpectedType = typeof(IReadOnlyCollection<DAL.Abstractions.Models.WorkTypeModel>)
                    }
                }
            },
            new TestCaseWithStubs<PageOptionsModel?, IReadOnlyCollection<WorkTypeModel>>
            {
                ScenarioNumber = 2,
                Description = "Проверка успешного получения всех видов работ (без постраничного разбиения).",
                InputData = default,
                OutputData =
                [
                    new WorkTypeModel 
                    { 
                        Id = _workTypeId1, Name = "WorkType1", 
                        WorkUnit = new WorkUnitModel { Id = 1, Name = "WorkUnit1"} 
                    },
                    new WorkTypeModel 
                    {
                        Id = _workTypeId2, Name = "WorkType2"
                    },
                    new WorkTypeModel 
                    {
                        Id = _workTypeId3, Name = "WorkType3"
                    }
                ],
                StubOutputs =new Dictionary<(string MethodName, int SequenceNumber), StubOutput>
                {
                    [(RepositoryMethodNames.WorkTypeRepository.GetAllAsync, 
                        StubSequenceConstants.First)] = new StubOutput
                    {
                        OutputData =  new []
                        {
                            new DAL.Abstractions.Models.WorkTypeModel 
                            { 
                                Id = _workTypeId1, Name = "WorkType1", WorkUnitId = 1, 
                                WorkUnit = new DAL.Abstractions.Models.WorkUnitModel 
                                { 
                                    Id = 1, Name = "WorkUnit1"
                                }
                            },
                            new DAL.Abstractions.Models.WorkTypeModel 
                            { 
                                Id = _workTypeId2, Name = "WorkType2"
                            },
                            new DAL.Abstractions.Models.WorkTypeModel 
                            { 
                                Id = _workTypeId3, Name = "WorkType3"
                            }
                        },
                        ExpectedType = typeof(IReadOnlyCollection<DAL.Abstractions.Models.WorkTypeModel>)
                    }
                }
            }
        ];

    /// <summary>
    /// Получает сценарии неуспешного выполнения метода <see cref="IWorkTypeService.GetAllAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseInput<PageOptionsModel>> 
        UnSuccessTestCases =>
        [
            new TestCaseInput<PageOptionsModel>
            {
                ScenarioNumber = 1,
                Description = "Проверка неуспешного постраничного получения видов работ. PageNumber равен 0.",
                InputData = new PageOptionsModel { PageNumber = 0, PageSize = 100},            
            },
            new TestCaseInput<PageOptionsModel>
            {
                ScenarioNumber = 2,
                Description = "Проверка неуспешного постраничного получения видов работ. PageNumber меньше 0.",
                InputData = new PageOptionsModel { PageNumber = -1, PageSize = 100},           
                
            },
            new TestCaseInput<PageOptionsModel>
            {
                ScenarioNumber = 3,
                Description = "Проверка неуспешного постраничного получения видов работ. PageSize равен 0.",
                InputData = new PageOptionsModel { PageNumber = 1, PageSize = 0},           
                
            },
            new TestCaseInput<PageOptionsModel>
            {
                ScenarioNumber = 4,
                Description = "Проверка неуспешного постраничного получения видов работ. PageSize меньше 0.",
                InputData = new PageOptionsModel { PageNumber = 1, PageSize = -1},                       
            },
            new TestCaseInput<PageOptionsModel>
            {
                ScenarioNumber = 5,
                Description = "Проверка неуспешного постраничного получения видов работ. PageNumber и PageSize равны 0.",
                InputData = new PageOptionsModel { PageNumber = 0, PageSize = 0},            
            },
            new TestCaseInput<PageOptionsModel>
            {
                ScenarioNumber = 6,
                Description = "Проверка неуспешного постраничного получения видов работ. PageNumber и PageSize равны -1.",
                InputData = new PageOptionsModel { PageNumber = -1, PageSize = -1},            
            },
        ];
}
