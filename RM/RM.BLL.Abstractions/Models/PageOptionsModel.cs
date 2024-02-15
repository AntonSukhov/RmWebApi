namespace RM.BLL.Abstractions.Models;

/// <summary>
/// Модель настроек страницы.
/// </summary>
public class PageOptionsModel
{
    #region Свойства

    /// <summary>
    /// Порядковый номер страницы.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Кол-во элементов страницы.
    /// </summary>
    public int PageSize { get; set; }

    #endregion
}