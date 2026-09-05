using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;

namespace RM.BLL.Tests.Services.PerformerService.DeleteAsync;

/// <summary>
/// Набор тестовых сценариев для проверки метода <see cref="IPerformerService.DeleteAsync"/>.
/// </summary>
public static class DeleteAsyncTestCases
{
    private static readonly Guid _performerId = Guid.NewGuid();

    /// <summary>
    /// Получает сценарии успешного выполнения метода <see cref="IPerformerService.DeleteAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseInputWithStubs<Guid>> SuccessTestCases
    {
        get
        {
            var theoryData = new TheoryData<TestCaseInputWithStubs<Guid>>
            {
                new() {
                    ScenarioNumber = 1,
                    Description = "Проверка успешного удаления исполнителя договоров по его значению ИД, который есть в БД.",
                    InputData = _performerId,
                    StubOutputs = new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.PerformerRepository.DeleteAsync,
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = 1,
                            ExpectedType = typeof(int)
                        }
                    }
                }
            };

            return theoryData;
        }
    }

    /// <summary>
    /// Получает сценарии неуспешного выполнения метода <see cref="IPerformerService.DeleteAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseInputWithStubs<Guid>> UnSuccessTestCases
    {
        get
        {
            var theoryData = new TheoryData<TestCaseInputWithStubs<Guid>>
            {
                new() {
                    ScenarioNumber = 1,
                    Description = "Проверка не успешного удаления исполнителя договоров по пустому значению его ИД, которого нет в БД.",
                    InputData = Guid.Empty,
                    StubOutputs = new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.PerformerRepository.DeleteAsync,
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = 0,
                            ExpectedType = typeof(int)
                        }
                    }
                },
                new() {
                    ScenarioNumber = 2,
                    Description = "Проверка не успешного удаления исполнителя договоров по не пустому значению его ИД, которого нет в БД.",
                    InputData = Guid.NewGuid(),
                    StubOutputs = new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.PerformerRepository.DeleteAsync,
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = 0,
                            ExpectedType = typeof(int)
                        }
                    }
                }
            };

            return theoryData;
        }
    }
}
