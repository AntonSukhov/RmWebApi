namespace RM.DAL.Abstractions.Models;

/// <summary>
/// Модель единицы работ.
/// </summary>
public class WorkUnitModel
{
    #region Свойства

    /// <summary>
    /// ИД единицы работ.
    /// </summary>
    public byte Id { get; set; }

    /// <summary>
    /// Название единицы работ.
    /// </summary>
    public string Name { get; set; }

    #endregion
}

