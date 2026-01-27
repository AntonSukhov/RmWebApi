namespace RM.BLL.Abstractions.Models;

/// <summary>
/// Модель создания вида работ.
/// </summary>
public class WorkTypeCreationModel
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
