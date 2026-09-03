using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Tests.Services.PerformerService.GetAllAsync;

/// <summary>
/// Набор тестовых сценариев для проверки метода <see cref="IPerformerService.GetAllAsync"/>.
/// </summary>
public static class GetAllAsyncTestCases
{
    private static readonly Guid _performerId1 = Guid.NewGuid();
    private static readonly Guid _performerEntityId1 = Guid.NewGuid();
    private static readonly Guid _performerId2 = Guid.NewGuid();
    private static readonly Guid _performerEntityId2 = Guid.NewGuid();

    /// <summary>
    /// Создаёт сущность исполнителя договоров для тестовых сценариев.
    /// </summary>
    /// <param name="id">Идентификатор исполнителя.</param>
    /// <param name="entityId">Идентификатор истории исполнителя.</param>
    /// <param name="surname">Фамилия.</param>
    /// <param name="name">Имя.</param>
    /// <returns>Сущность исполнителя договоров.</returns>
    private static PerformerEntity CreatePerformerEntity(Guid id, Guid entityId, string surname, string name) =>
        new()
        {
            Id = id,
            EntityId = entityId,
            CreateDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            EditDate = null,
            Creator = "Creator1",
            Editor = null,
            Snils = "112-233-445 95",
            Inn = "1234567890",
            Surname = surname,
            Name = name,
            Patronymic = "Patronymic1",
            Gender = 0,
            PassportNumber = "123456",
            PassportSeries = "1234",
            PassportIssuePlace = "ОУФМС России",
            PassportIssueDate = new DateTime(2010, 5, 10, 0, 0, 0, DateTimeKind.Utc),
            PassportDepartmentCode = "123-456",
            PassportBirthPlace = "г. Москва",
            PassportBirthDate = new DateTime(1990, 3, 15, 0, 0, 0, DateTimeKind.Utc),
            PassportRegistrationPlace = "г. Москва, ул. Ленина, д. 1"
        };

    /// <summary>
    /// Создаёт модель исполнителя договоров для тестовых сценариев.
    /// </summary>
    /// <param name="id">Идентификатор исполнителя.</param>
    /// <param name="entityId">Идентификатор истории исполнителя.</param>
    /// <param name="surname">Фамилия.</param>
    /// <param name="name">Имя.</param>
    /// <returns>Модель исполнителя договоров.</returns>
    private static PerformerModel CreatePerformerModel(Guid id, Guid entityId, string surname, string name) =>
        new()
        {
            Id = id,
            EntityId = entityId,
            CreateDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            EditDate = null,
            Creator = "Creator1",
            Editor = null,
            Snils = "112-233-445 95",
            Inn = "1234567890",
            Surname = surname,
            Name = name,
            Patronymic = "Patronymic1",
            Gender = BLL.Abstractions.Enums.GenderEnum.Male,
            PassportNumber = "123456",
            PassportSeries = "1234",
            PassportIssuePlace = "ОУФМС России",
            PassportIssueDate = new DateTime(2010, 5, 10, 0, 0, 0, DateTimeKind.Utc),
            PassportDepartmentCode = "123-456",
            PassportBirthPlace = "г. Москва",
            PassportBirthDate = new DateTime(1990, 3, 15, 0, 0, 0, DateTimeKind.Utc),
            PassportRegistrationPlace = "г. Москва, ул. Ленина, д. 1"
        };

    /// <summary>
    /// Получает сценарии успешного выполнения метода <see cref="IPerformerService.GetAllAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseWithStubs<PageOptionsModel?, IReadOnlyCollection<PerformerModel>>>
        SuccessTestCases
    {
        get
        {
            var theoryData = new TheoryData<
                TestCaseWithStubs<PageOptionsModel?, IReadOnlyCollection<PerformerModel>>>
            {
                new() {
                    ScenarioNumber = 1,
                    Description = "Проверка успешного постраничного получения исполнителей договоров.",
                    InputData = new PageOptionsModel { PageNumber = 1, PageSize = 100},
                    OutputData =
                    [
                        CreatePerformerModel(_performerId1, _performerEntityId1, "Ivanov", "Ivan"),
                        CreatePerformerModel(_performerId2, _performerEntityId2, "Petrov", "Petr")
                    ],
                    StubOutputs = new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.PerformerRepository.GetAllAsync,
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = new []
                            {
                                CreatePerformerEntity(_performerId1, _performerEntityId1, "Ivanov", "Ivan"),
                                CreatePerformerEntity(_performerId2, _performerEntityId2, "Petrov", "Petr")
                            },
                            ExpectedType = typeof(IReadOnlyCollection<PerformerEntity>)
                        }
                    }
                },
                new() {
                    ScenarioNumber = 2,
                    Description = "Проверка успешного получения всех исполнителей договоров (без постраничного разбиения).",
                    InputData = default,
                    OutputData =
                    [
                        CreatePerformerModel(_performerId1, _performerEntityId1, "Ivanov", "Ivan"),
                        CreatePerformerModel(_performerId2, _performerEntityId2, "Petrov", "Petr")
                    ],
                    StubOutputs = new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(RepositoryMethodNames.PerformerRepository.GetAllAsync,
                            StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = new []
                            {
                                CreatePerformerEntity(_performerId1, _performerEntityId1, "Ivanov", "Ivan"),
                                CreatePerformerEntity(_performerId2, _performerEntityId2, "Petrov", "Petr")
                            },
                            ExpectedType = typeof(IReadOnlyCollection<PerformerEntity>)
                        }
                    }
                }
            };

            return theoryData;
        }
    }

    /// <summary>
    /// Получает сценарии неуспешного выполнения метода <see cref="IPerformerService.GetAllAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseInput<PageOptionsModel>> UnSuccessTestCases
    {
        get
        {
            var theoryData = new TheoryData<TestCaseInput<PageOptionsModel>>
            {
                new() {
                    ScenarioNumber = 1,
                    Description = "Проверка неуспешного постраничного получения исполнителей договоров. PageNumber равен 0.",
                    InputData = new PageOptionsModel { PageNumber = 0, PageSize = 100},
                },
                new() {
                    ScenarioNumber = 2,
                    Description = "Проверка неуспешного постраничного получения исполнителей договоров. PageNumber меньше 0.",
                    InputData = new PageOptionsModel { PageNumber = -1, PageSize = 100},

                },
                new() {
                    ScenarioNumber = 3,
                    Description = "Проверка неуспешного постраничного получения исполнителей договоров. PageSize равен 0.",
                    InputData = new PageOptionsModel { PageNumber = 1, PageSize = 0},

                },
                new() {
                    ScenarioNumber = 4,
                    Description = "Проверка неуспешного постраничного получения исполнителей договоров. PageSize меньше 0.",
                    InputData = new PageOptionsModel { PageNumber = 1, PageSize = -1},
                },
                new() {
                    ScenarioNumber = 5,
                    Description = "Проверка неуспешного постраничного получения исполнителей договоров. PageNumber и PageSize равны 0.",
                    InputData = new PageOptionsModel { PageNumber = 0, PageSize = 0},
                },
                new() {
                    ScenarioNumber = 6,
                    Description = "Проверка неуспешного постраничного получения исполнителей договоров. PageNumber и PageSize равны -1.",
                    InputData = new PageOptionsModel { PageNumber = -1, PageSize = -1},
                }
            };

            return theoryData;
        }
    }
}
