namespace RM.BLL.Abstractions.Models;

/// <summary>
/// Модель создания вида работ.
/// </summary>
public class WorkTypeCreationModel
{
    /// <summary>
    /// Название вида работ.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Идентификатор единицы работ для вида работ.
    /// </summary>
    public byte? WorkUnitId { get; set; }

}
