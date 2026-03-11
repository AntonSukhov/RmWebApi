namespace RM.Api.DTOs.Requests;

/// <summary>
/// Запрос на создание вида работ.
/// </summary>
public class WorkTypeCreationRequest
{
    /// <summary>
    /// Получает или задает название вида работ.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Получает или задает ИД единицы работ для вида работ.
    /// </summary>
    public byte? WorkUnitId { get; set; }
}
