namespace RM.Api.DTOs.Requests;

/// <summary>
/// Запрос настроек страницы для реализации пагинации.
/// </summary>
public class PageOptionsRequest
{
    /// <summary>
    /// Получает или задает порядковый номер страницы.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Получает или задает количество элементов страницы.
    /// </summary>
    public int PageSize { get; set; }
}
