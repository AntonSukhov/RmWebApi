namespace RM.DAL.Abstractions.Entities;

/// <summary>
/// Сущность единицы работ.
/// </summary>
public class WorkUnitEntity
{
    /// <summary>
    /// Получает или задает ИД единицы работ.
    /// </summary>
    public byte Id { get; set; }

    /// <summary>
    /// Получает или задает название единицы работ.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

