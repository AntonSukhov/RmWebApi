using RM.DAL.Abstractions.Models;
using RM.Tests.Common.TestData;

namespace RM.DAL.Tests.TestData;

/// <summary>
/// Тестовые данные для репозитория работы с видами работ.
/// </summary>
public class WorkTypeRepositoryTestData : PaginationTestData
{
    #region Методы

    /// <summary>
    /// Данные для теста создания вида работ для корректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeShortModel> CreateAsyncForCorrectDataTestData()
    {
        return new TheoryData<WorkTypeShortModel>
        {
            new WorkTypeShortModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}"
            },
            new WorkTypeShortModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}",
                WorkUnitId = 1
            },
            new WorkTypeShortModel()
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
    public static TheoryData<WorkTypeShortModel?> CreateAsyncForIncorrectDataTestData()
    {
        return new TheoryData<WorkTypeShortModel?>
        {
            null,
            new WorkTypeShortModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}",
                WorkUnitId = byte.MaxValue
            },
            new WorkTypeShortModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}",
                WorkUnitId = byte.MinValue
            },
            new WorkTypeShortModel()
            {
                Id = Guid.NewGuid(), 
                Name = null,
                WorkUnitId = 2
            },
            new WorkTypeShortModel()
            {
                Id = Guid.NewGuid(), 
                Name = new string('n', 201),
                WorkUnitId = 2
            }
        };
    }

    /// <summary>
    /// Данные для теста создания вида работ для некорректных входных данных. Второй набор данных.
    /// </summary>
    public static TheoryData<WorkTypeShortModel?> CreateAsyncForIncorrectDataTestDataSecond()
    {
        return new TheoryData<WorkTypeShortModel?>
        {
            null,
            new WorkTypeShortModel()
            {
                Id = Guid.NewGuid(),
                Name = $"Вид работ {Guid.NewGuid()}",
                WorkUnitId = byte.MaxValue
            },
            new WorkTypeShortModel()
            {
                Id = Guid.NewGuid(),
                Name = $"Вид работ {Guid.NewGuid()}",
                WorkUnitId = byte.MinValue
            },
            new WorkTypeShortModel()
            {
                Id = Guid.NewGuid(),
                Name = null,
                WorkUnitId = 2
            }
        };
    }

    /// <summary>
    /// Данные для теста удаления вида работ для корректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeShortModel> DeleteAsyncForCorrectDataTestData()
    {
        return new TheoryData<WorkTypeShortModel>
        {
            new WorkTypeShortModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}"
            },
            new WorkTypeShortModel()
            {
                Id = Guid.NewGuid(), 
                Name = $"Вид работ {Guid.NewGuid()}",
                WorkUnitId = 1
            },
            new WorkTypeShortModel()
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
    public static TheoryData<WorkTypeShortModel, string, byte?> UpdateAsyncForCorrectInputDataTestData()
    {
        return new TheoryData<WorkTypeShortModel, string, byte?>
        {
            {   
                new WorkTypeShortModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}"
                }, 
                $"Вид работ {Guid.NewGuid()}", 
                1 
            },
            {   
                new WorkTypeShortModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}",
                    WorkUnitId = 1
                }, 
                $"Вид работ {Guid.NewGuid()}", 
                2 
            },
            {   
                new WorkTypeShortModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}",
                    WorkUnitId = 2
                }, 
                $"Вид работ {Guid.NewGuid()}",
                null
            },
            {   
                new WorkTypeShortModel()
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
                DataSourceTestData.WorkTypes.First(), 
                $"Вид работ {Guid.NewGuid()}", 
                DataSourceTestData.WorkUnits.First().Id
            },
            {   
                DataSourceTestData.WorkTypes.First(),
                $"Вид работ {Guid.NewGuid()}", 
                DataSourceTestData.WorkUnits.Last().Id 
            },
            {   
                DataSourceTestData.WorkTypes.Last(), 
                $"Вид работ {Guid.NewGuid()}",
                null
            },
            {   
                DataSourceTestData.WorkTypes.Last(), 
                null,
                DataSourceTestData.WorkUnits.First().Id
            }
            
        };
    }

    /// <summary>
    /// Данные для теста обновления вида работ для некорректных входных данных.
    /// </summary>
    public static TheoryData<WorkTypeShortModel, string?, byte?> UpdateAsyncForIncorrectWorkTypeNameOrWorkUnitIdTestData()
    {
        return new TheoryData<WorkTypeShortModel, string?, byte?>
        {
            {   
                new WorkTypeShortModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}"
                }, 
                $"Вид работ {Guid.NewGuid()}", 
                byte.MinValue 
            },
            {   
                new WorkTypeShortModel()
                {
                    Id = Guid.NewGuid(), 
                    Name = $"Вид работ {Guid.NewGuid()}",
                    WorkUnitId = 1
                }, 
                $"Вид работ {Guid.NewGuid()}", 
                byte.MaxValue
            },
            {   
                new WorkTypeShortModel()
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
