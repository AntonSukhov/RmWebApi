using RM.DAL.Abstractions.Models;

namespace RM.DAL.Tests.TestData;

/// <summary>
/// Тестовые данные для базы данных.
/// </summary>
public static class DataBaseTestData
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
        new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 1" },
        new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 2", 
                            WorkUnitId = _workUnits.FirstOrDefault()?.Id, 
                            WorkUnit = _workUnits.FirstOrDefault() },
        new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 3", 
                            WorkUnitId = _workUnits.Skip(1).FirstOrDefault()?.Id, 
                            WorkUnit = _workUnits.Skip(1).FirstOrDefault() },
        new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 4", 
                            WorkUnitId = _workUnits.LastOrDefault()?.Id, 
                            WorkUnit = _workUnits.LastOrDefault() },
        new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 5" },
        new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 6" },
        new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 7" },
        new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 8" },
        new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 9" },
        new WorkTypeModel { Id = Guid.NewGuid(), Name = "Вид работ 10", 
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

    #endregion
}
