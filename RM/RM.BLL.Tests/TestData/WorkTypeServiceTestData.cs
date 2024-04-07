using RM.BLL.Abstractions.Models;
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
    /// Данные для теста получения вида работ по его идентификатору 
    /// для корректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeGettingByIdModel> GetByIdAsyncForCorrectDataTestData()
    {
        return new TheoryData<WorkTypeGettingByIdModel>
        {
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.Empty }
        };
    }

    /// <summary>
    /// Данные для теста получения вида работ по его идентификатору 
    /// для некорректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeGettingByIdModel?> GetByIdAsyncForIncorrectDataTestData()
    {
        return new TheoryData<WorkTypeGettingByIdModel?>
        {
           null
        };
    }

    /// <summary>
    /// Данные для теста создания вида работ для корректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeCreationModel> CreateAsyncForCorrectDataTestData()
    {
        return new TheoryData<WorkTypeCreationModel>
        {
            new() { Name = $"Вид работ {Guid.NewGuid()}"},
            new() { Name = new string('n', 200) },
            new() { Name = $"Вид работ {Guid.NewGuid()}", 
                    WorkUnitId = DataSourceTestData.WorkUnits.FirstOrDefault()?.Id },
            new() { Name = $"Вид работ {Guid.NewGuid()}", 
                    WorkUnitId = DataSourceTestData.WorkUnits.LastOrDefault()?.Id }
        };
    }

    /// <summary>
    /// Данные для теста создания вида работ для некорректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeCreationModel> CreateAsyncForIncorrectDataTestData()
    {
        return new TheoryData<WorkTypeCreationModel> 
        {
            new(),
            new() { Name = null, 
                    WorkUnitId = DataSourceTestData.WorkUnits.FirstOrDefault()?.Id},
            new() { Name = string.Empty, 
                    WorkUnitId = DataSourceTestData.WorkUnits.FirstOrDefault()?.Id},
            new() { Name = new string('n', 201), 
                    WorkUnitId = DataSourceTestData.WorkUnits.LastOrDefault()?.Id },
            new() { Name = $"Вид работ {Guid.NewGuid()}", 
                    WorkUnitId = byte.MinValue },
            new() { Name = $"Вид работ {Guid.NewGuid()}", 
                    WorkUnitId = byte.MaxValue }
        };
    }

    /// <summary>
    /// Данные для теста удаления вида работ для корректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeDeletionModel> DeleteAsyncForCorrectDataTestData()
    {
        return new TheoryData<WorkTypeDeletionModel>
        {
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.Empty }         
        };
    }

    /// <summary>
    /// Данные для теста удаления вида работ для некорректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeDeletionModel> DeleteAsyncForIncorrectDataTestData()
    {
        return new TheoryData<WorkTypeDeletionModel>
        {
            new()       
        };
    }

    /// <summary>
    /// Данные для теста обновления вида работ для корректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeUpdationModel> UpdateAsyncForCorrectDataTestData()
    {
        return new TheoryData<WorkTypeUpdationModel>
        {
            new() { Id = Guid.NewGuid(), Name = $"Вид работ {Guid.NewGuid()}"},
            new() { Id = Guid.NewGuid(), Name = new string('n', 200) },
            new() { Id = Guid.NewGuid(), Name = $"Вид работ {Guid.NewGuid()}", 
                    WorkUnitId = DataSourceTestData.WorkUnits.FirstOrDefault()?.Id },
            new() { Id = Guid.NewGuid(), Name = $"Вид работ {Guid.NewGuid()}", 
                    WorkUnitId = DataSourceTestData.WorkUnits.LastOrDefault()?.Id },
            new() { Id = Guid.NewGuid(),  
                    WorkUnitId = DataSourceTestData.WorkUnits.LastOrDefault()?.Id }
        };
    }

    /// <summary>
    /// Данные для теста обновления вида работ для некорректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeUpdationModel> UpdateAsyncForIncorrectDataTestData()
    {
        return new TheoryData<WorkTypeUpdationModel> 
        {
            new(),
            new() { Name = $"Вид работ {Guid.NewGuid()}",
                    WorkUnitId = DataSourceTestData.WorkUnits.FirstOrDefault()?.Id},
            new() { Id = Guid.Empty,
                    Name = $"Вид работ {Guid.NewGuid()}",
                    WorkUnitId = DataSourceTestData.WorkUnits.FirstOrDefault()?.Id},
            new() { Id = Guid.NewGuid(),
                    Name = string.Empty, 
                    WorkUnitId = DataSourceTestData.WorkUnits.FirstOrDefault()?.Id},
            new() { Id = Guid.NewGuid(),
                    Name = new string('n', 201), 
                    WorkUnitId = DataSourceTestData.WorkUnits.LastOrDefault()?.Id },
            new() { Id = Guid.NewGuid(),
                    Name = $"Вид работ {Guid.NewGuid()}", 
                    WorkUnitId = byte.MinValue },
            new() { Id = Guid.NewGuid(),
                    Name = $"Вид работ {Guid.NewGuid()}", 
                    WorkUnitId = byte.MaxValue },
            new() { Id = Guid.NewGuid()}
        };
    }
    
    #endregion
}
