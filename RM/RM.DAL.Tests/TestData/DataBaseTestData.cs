using RM.DAL.Abstractions.Models;

namespace RM.DAL.Tests.TestData;

/// <summary>
/// Тестовые данные для базы данных.
/// </summary>
public static class DataBaseTestData
{
    #region Методы

    /// <summary>
    /// Предоставляет тестовые данных единиц работ.
    /// </summary>
    /// <returns>Тестовые данных единиц работ.</returns>
    public static IEnumerable<WorkUnitModel> GetWorkUnits()
    {
        return
        [
            new WorkUnitModel { Id = 1, Name = "машина" },
            new WorkUnitModel { Id = 2, Name = "шт." },
            new WorkUnitModel { Id = 3, Name = "Кв.м." }
        ];
    }

    #endregion
}
