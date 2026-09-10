namespace RM.Api.DTOs.Requests;

/// <summary>
/// Запрос настроек страницы для реализации пагинации.
/// </summary>
public class PageOptionsRequest
{
    /// <summary>
    /// Получает или задает порядковый номер страницы.
    /// </summary>
    /// <value>Порядковый номер страницы. По умолчанию значение 1.</value>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Получает или задает количество элементов страницы.
    /// </summary>
    /// <value>Количество элементов страницы. По умолчанию значение 100.</value>
    public int PageSize { get; set; } = 100;
}
