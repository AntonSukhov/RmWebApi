namespace RM.Api.DTOs.Responses;

/// <summary>
/// Ответ, представляющий единицу работ.
/// </summary>
public class WorkUnitResponse
{
    /// <summary>
    /// Получает или задает идентификатор единицы работ.
    /// </summary>
    public byte Id { get; set; }

    /// <summary>
    /// Получает или задает название единицы работ.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
