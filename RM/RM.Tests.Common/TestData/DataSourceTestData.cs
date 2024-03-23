using RM.DAL.Abstractions.Models;

namespace RM.Tests.Common.TestData;

/// <summary>
/// Тестовые данные источника данных.
/// </summary>
public static class DataSourceTestData
{
     #region Поля

    private static readonly IEnumerable<WorkUnitModel> _workUnits = 
    [
        new WorkUnitModel { Id = 1, Name = "машина" },
        new WorkUnitModel { Id = 2, Name = "шт." },
        new WorkUnitModel { Id = 3, Name = "Кв.м." }
    ];

    private static readonly IEnumerable<WorkTypeModel> _workTypes = 
    [
        new WorkTypeModel { Id = Guid.Parse("29deec3b-b376-4ad5-a9aa-b214bb6da266"), 
                            Name = "Вид работ 1" },
        new WorkTypeModel { Id = Guid.Parse("cefd2ef1-a7d6-42c0-8ca9-e9df2e562ab8"), 
                            Name = "Вид работ 2", 
                            WorkUnitId = _workUnits.FirstOrDefault()?.Id, 
                            WorkUnit = _workUnits.FirstOrDefault() },
        new WorkTypeModel { Id = Guid.Parse("69e5c02a-46c2-4034-94d4-8bd22b8a22f2"),
                            Name = "Вид работ 3", 
                            WorkUnitId = _workUnits.Skip(1).FirstOrDefault()?.Id, 
                            WorkUnit = _workUnits.Skip(1).FirstOrDefault() },
        new WorkTypeModel { Id = Guid.Parse("49857726-892a-4980-9e2b-9fb68a68683a"),
                            Name = "Вид работ 4", 
                            WorkUnitId = _workUnits.LastOrDefault()?.Id, 
                            WorkUnit = _workUnits.LastOrDefault() },
        new WorkTypeModel { Id = Guid.Parse("257a6515-86f9-4900-a30d-aaef667a9b92"), 
                            Name = "Вид работ 5" },
        new WorkTypeModel { Id = Guid.Parse("6152fb83-63ac-4120-a0fc-bf3865cab398"), 
                            Name = "Вид работ 6" },
        new WorkTypeModel { Id = Guid.Parse("15b047c3-579a-45b2-afe3-e33d47e9b0cd"), 
                            Name = "Вид работ 7" },
        new WorkTypeModel { Id = Guid.Parse("faaedb71-1ce4-4069-b266-36375088c816"),
                            Name = "Вид работ 8" },
        new WorkTypeModel { Id = Guid.Parse("f98ff7ef-33e6-41b3-ad0d-e8a48a5d2d84"),
                            Name = "Вид работ 9" },
        new WorkTypeModel { Id = Guid.Parse("7b670ba5-938c-456b-8424-1590a940056a"),
                            Name = "Вид работ 10", 
                            WorkUnitId = _workUnits.FirstOrDefault()?.Id, 
                            WorkUnit = _workUnits.FirstOrDefault() }
    ];

    #endregion

    #region Свойства

    /// <summary>
    /// Тестовые данные единиц работ.
    /// </summary>
    public static IEnumerable<WorkUnitModel> WorkUnits => _workUnits;

    /// <summary>
    /// Тестовые данные видов работ.
    /// </summary>
    public static IEnumerable<WorkTypeModel> WorkTypes => _workTypes;

    /// <summary>
    /// Тестовые данные видов работ, отсортированные по возрастанию по свойству ИД вида работ.
    /// </summary>
    public static IEnumerable<WorkTypeModel> WorkTypesSortedById => _workTypes.OrderBy(p => p.Id);

    #endregion
}
