namespace RM.DAL.Abstractions.Models;

/// <summary>
/// Модель единицы работ.
/// </summary>
public class WorkUnitModel
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

