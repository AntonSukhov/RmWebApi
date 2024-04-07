namespace RM.BLL.Abstractions.Models;

/// <summary>
/// Модель создания вида работ.
/// </summary>
public class WorkTypeCreationModel
{
    #region Свойства

    /// <summary>
    /// Название вида работ.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Идентификатор единицы работ для вида работ.
    /// </summary>
    public byte? WorkUnitId { get; set; }

    #endregion
}
