namespace RM.BLL.Abstractions.Models;

/// <summary>
/// Модель единицы работ.
/// </summary>
public class WorkUnitModel
{
    #region Свойства

    /// <summary>
    /// Идентификатор единицы работ.
    /// </summary>
    public byte Id { get; set; }

    /// <summary>
    /// Название единицы работ.
    /// </summary>
    public string Name { get; set; }

    #endregion
}
