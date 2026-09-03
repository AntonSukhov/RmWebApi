using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Tests.Services.PerformerService.GetByIdAsync;

/// <summary>
/// Набор тестовых сценариев для проверки метода <see cref="IPerformerService.GetByIdAsync"/>.
/// </summary>
public static class GetByIdAsyncTestCases
{
    private static readonly Guid _performerId = Guid.NewGuid();
    private static readonly Guid _performerEntityId = Guid.NewGuid();

    /// <summary>
    /// Получает сценарии успешного выполнения метода <see cref="IPerformerService.GetByIdAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseWithStubs<Guid, PerformerModel?>> SuccessTestCases
    {
        get
        {
            var theoryData = new TheoryData<TestCaseWithStubs<Guid, PerformerModel?>>
            {
                new() {
                    ScenarioNumber = 1,
                    Description = "Проверка успешного получения исполнителя договоров по не пустому значению ИД.",
                    InputData = _performerId,
                    OutputData = new PerformerModel
                    {
                        Id = _performerId,
                        EntityId = _performerEntityId,
                        CreateDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        EditDate = null,
                        Creator = "Creator1",
                        Editor = null,
                        Snils = "112-233-445 95",
                        Inn = "1234567890",
                        Surname = "Ivanov",
                        Name = "Ivan",
                        Patronymic = "Ivanovich",
                        Gender = BLL.Abstractions.Enums.GenderEnum.Male,
                        PassportNumber = "123456",
                        PassportSeries = "1234",
                        PassportIssuePlace = "ОУФМС России",
                        PassportIssueDate = new DateTime(2010, 5, 10, 0, 0, 0, DateTimeKind.Utc),
                        PassportDepartmentCode = "123-456",
                        PassportBirthPlace = "г. Москва",
                        PassportBirthDate = new DateTime(1990, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                        PassportRegistrationPlace = "г. Москва, ул. Ленина, д. 1"
                    },
                    StubOutputs = new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.PerformerRepository.GetByIdAsync,
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = new PerformerEntity
                            {
                                Id = _performerId,
                                EntityId = _performerEntityId,
                                CreateDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                                EditDate = null,
                                Creator = "Creator1",
                                Editor = null,
                                Snils = "112-233-445 95",
                                Inn = "1234567890",
                                Surname = "Ivanov",
                                Name = "Ivan",
                                Patronymic = "Ivanovich",
                                Gender = 0,
                                PassportNumber = "123456",
                                PassportSeries = "1234",
                                PassportIssuePlace = "ОУФМС России",
                                PassportIssueDate = new DateTime(2010, 5, 10, 0, 0, 0, DateTimeKind.Utc),
                                PassportDepartmentCode = "123-456",
                                PassportBirthPlace = "г. Москва",
                                PassportBirthDate = new DateTime(1990, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                                PassportRegistrationPlace = "г. Москва, ул. Ленина, д. 1"
                            },
                            ExpectedType = typeof(PerformerEntity)
                        }
                    }
                },
                new() {
                    ScenarioNumber = 2,
                    Description = "Проверка получения Null по пустому значению ИД.",
                    InputData = Guid.Empty,
                    OutputData = default,
                    StubOutputs = new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.PerformerRepository.GetByIdAsync,
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = null,
                            ExpectedType = typeof(PerformerEntity)
                        }
                    }
                },
                new() {
                    ScenarioNumber = 3,
                    Description = "Проверка получения Null по не пустому значению ИД и которого нет в БД.",
                    InputData = _performerId,
                    OutputData = default,
                    StubOutputs = new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.PerformerRepository.GetByIdAsync,
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = null,
                            ExpectedType = typeof(PerformerEntity)
                        }
                    }
                }
            };

            return theoryData;
        }
    }
}
