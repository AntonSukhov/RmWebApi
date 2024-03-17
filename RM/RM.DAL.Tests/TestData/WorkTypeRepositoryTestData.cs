using RM.DAL.Abstractions.Models;

namespace RM.DAL.Tests.TestData;

/// <summary>
/// Тестовые данные для репозитория работы с видами работ.
/// </summary>
public class WorkTypeRepositoryTestData : PaginationTestData
{
    #region Свойства

    /// <summary>
    /// Данные для теста создания вида работ для корректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeModel> CreateAsyncForCorrectDataTestData()
    {
        return new TheoryData<WorkTypeModel>
        {
            new WorkTypeModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}"
            },
            new WorkTypeModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}",
                WorkUnitId = 1
            },
            new WorkTypeModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}",
                WorkUnitId = 2
            }
        };
    }

    /// <summary>
    /// Данные для теста создания вида работ для некорректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeModel?> CreateAsyncForIncorrectDataTestData()
    {
        return new TheoryData<WorkTypeModel?>
        {
            null,
            new WorkTypeModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}",
                WorkUnitId = byte.MaxValue
            },
            new WorkTypeModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}",
                WorkUnitId = byte.MinValue
            },
            new WorkTypeModel()
            {
                Id = Guid.NewGuid(), 
                Name = null,
                WorkUnitId = 2
            },
            new WorkTypeModel()
            {
                Id = Guid.NewGuid(), 
                Name = new string('n', 201),
                WorkUnitId = 2
            }
        };
    }

    /// <summary>
    /// Данные для теста удаления вида работ для корректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeModel> DeleteAsyncForCorrectDataTestData()
    {
        return new TheoryData<WorkTypeModel>
        {
            new WorkTypeModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}"
            },
            new WorkTypeModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}",
                WorkUnitId = 1
            },
            new WorkTypeModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}",
                WorkUnitId = 2
            }
        };
    }

    /// <summary>
    /// Данные для теста удаления несуществующего вида работ.
    /// </summary>
    public static TheoryData<Guid> DeleteAsyncNotExistedWorkTypeTestData()
    {
        return new TheoryData<Guid>
        {
            Guid.Empty,
            Guid.NewGuid()
        };
    }

    /// <summary>
    /// Данные для теста обновления вида работ для корректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeModel, string, byte?> UpdateAsyncForCorrectInputDataTestData()
    {
        return new TheoryData<WorkTypeModel, string, byte?>
        {
            {   
                new WorkTypeModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}"
                }, 
                $"Вид работ {Guid.NewGuid()}", 
                1 
            },
            {   
                new WorkTypeModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}",
                    WorkUnitId = 1
                }, 
                $"Вид работ {Guid.NewGuid()}", 
                2 
            },
            {   
                new WorkTypeModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}",
                    WorkUnitId = 2
                }, 
                $"Вид работ {Guid.NewGuid()}",
                null
            },
            {   
                new WorkTypeModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}",
                    WorkUnitId = 2
                }, 
                $"Вид работ {Guid.NewGuid()}",
                1
            }
        };
    }


    /// <summary>
    /// Данные для теста обновления вида работ для корректных входных данных в 
    /// источник данных Sqlite в памяти.
    /// </summary>
    public static TheoryData<WorkTypeModel, string, byte?> UpdateAsyncForCorrectInputDataInSqliteInMemoryTestData()
    {
        return new TheoryData<WorkTypeModel, string, byte?>
        {
            {   
                DataBaseTestData.WorkTypes.First(), 
                $"Вид работ {Guid.NewGuid()}", 
                DataBaseTestData.WorkUnits.First().Id
            },
            {   
                DataBaseTestData.WorkTypes.First(),
                $"Вид работ {Guid.NewGuid()}", 
                DataBaseTestData.WorkUnits.Last().Id 
            },
            {   
                DataBaseTestData.WorkTypes.Last(), 
                $"Вид работ {Guid.NewGuid()}",
                null
            }
        };
    }

    /// <summary>
    /// Данные для теста обновления вида работ для некорректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeModel, string?, byte?> UpdateAsyncForIncorrectWorkTypeNameOrWorkUnitIdTestData()
    {
        return new TheoryData<WorkTypeModel, string?, byte?>
        {
            {   
                new WorkTypeModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}"
                }, 
                $"Вид работ {Guid.NewGuid()}", 
                byte.MinValue 
            },
            {   
                new WorkTypeModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}",
                    WorkUnitId = 1
                }, 
                $"Вид работ {Guid.NewGuid()}", 
                byte.MaxValue
            },
            {   
                new WorkTypeModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}",
                    WorkUnitId = 2
                }, 
                null,
                1
            },
            {   
                new WorkTypeModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}",
                    WorkUnitId = 2
                }, 
                new string('n', 201),
                1
            }
        };
    }

    #endregion
}
