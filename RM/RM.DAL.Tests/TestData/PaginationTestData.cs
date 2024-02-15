using RM.DAL.Abstractions.Models;

namespace RM.DAL.Tests;

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
            new PageOptionsModel(){ PageNumber = 1, PageSize = 100}
        };
    }
    
    /// <summary>
    /// Предоставляет коллекцию некорректных настроек страницы.
    /// </summary>
    /// <returns>Коллекция некорректных настроек страницы.</returns>
    public static IEnumerable<PageOptionsModel> GetIncorrectPageOptions()
    {
        return
        [
            new PageOptionsModel(){ PageNumber = 0, PageSize = 0},
            new PageOptionsModel(){ PageNumber = 1, PageSize = 0},
            new PageOptionsModel(){ PageNumber = 0, PageSize = -100},
            new PageOptionsModel(){ PageNumber = 1, PageSize = -100},
            new PageOptionsModel(){ PageNumber = -1, PageSize = 100},
            new PageOptionsModel(){ PageNumber = -1, PageSize = -100},
        ];
    }
    #endregion
}