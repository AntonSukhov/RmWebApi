using RM.BLL.Abstractions.Models;

namespace RM.BLL.Tests.TestData;

/// <summary>
/// Тестовые данные для пагинации.
/// </summary>
public class PaginationTestData
{

    #region Конструкторы

    protected PaginationTestData() { }

    #endregion

    #region Методы

    /// <summary>
    /// Предоставляет коллекцию корректных настроек страницы.
    /// </summary>
    /// <returns>Коллекция корректных настроек страницы.</returns>
    public static TheoryData<PageOptionsModel?> GetCorrectPageOptions()
    {
        return new TheoryData<PageOptionsModel?>
        {
            null,
            new() { PageNumber = 1, PageSize = 100}
        };
    }
    
    /// <summary>
    /// Предоставляет коллекцию некорректных настроек страницы.
    /// </summary>
    /// <returns>Коллекция некорректных настроек страницы.</returns>
    public static TheoryData<PageOptionsModel> GetIncorrectPageOptions()
    {
        return new TheoryData<PageOptionsModel>
        {
            new(){ PageNumber = 0, PageSize = 0},
            new(){ PageNumber = 1, PageSize = 0},
            new(){ PageNumber = 0, PageSize = -100},
            new(){ PageNumber = 1, PageSize = -100},
            new(){ PageNumber = -1, PageSize = 100},
            new(){ PageNumber = -1, PageSize = -100},
        };
    }
    #endregion
}
