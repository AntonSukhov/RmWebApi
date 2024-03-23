using RM.Tests.Common.TestData;

namespace RM.BLL.Tests.TestData;

/// <summary>
/// Тестовые данные для сервиса видов работ.
/// </summary>
public class WorkTypeServiceTestData
{
    #region Конструкторы

    protected  WorkTypeServiceTestData() { }

    #endregion

    #region Методы

    /// <summary>
    /// Данные для теста создания вида работ для корректных входных данных.
    /// </summary>
    public static TheoryData<string, byte?> CreateAsyncForCorrectDataTestData()
    {
        return new TheoryData<string, byte?>
        {
            { $"Вид работ {Guid.NewGuid()}" , null },
            { new string('n', 200) , null },
            { $"Вид работ {Guid.NewGuid()}", DataSourceTestData.WorkUnits.FirstOrDefault()?.Id },
            { $"Вид работ {Guid.NewGuid()}", DataSourceTestData.WorkUnits.LastOrDefault()?.Id }
        };
    }

    /// <summary>
    /// Данные для теста создания вида работ для некорректных входных данных.
    /// </summary>
    public static TheoryData<string?, byte?> CreateAsyncForIncorrectDataTestData()
    {
        return new TheoryData<string?, byte?>
        {
            { null , null },
            { string.Empty, DataSourceTestData.WorkUnits.FirstOrDefault()?.Id  },
            { new string('n', 201) , DataSourceTestData.WorkUnits.LastOrDefault()?.Id },
            { $"Вид работ {Guid.NewGuid()}", byte.MinValue },
            { $"Вид работ {Guid.NewGuid()}", byte.MaxValue },
            { DataSourceTestData.WorkTypes.FirstOrDefault()?.Name, DataSourceTestData.WorkUnits.FirstOrDefault()?.Id },
        };
    }

    #endregion
}
