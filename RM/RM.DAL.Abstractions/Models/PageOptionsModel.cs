namespace RM.DAL.Abstractions.Models;

/// <summary>
/// Модель настроек страницы.
/// </summary>
public class PageOptionsModel
{
    /// <summary>
    /// Получает или задает порядковый номер страницы.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Получает или задает кол-во элементов страницы.
    /// </summary>
    public int PageSize { get; set; }

}
