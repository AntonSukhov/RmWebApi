using System;

namespace RM.BLL.Abstractions.Models;

/// <summary>
/// Модель вида работ.
/// </summary>
public class WorkTypeModel
{
    #region Свойства

    /// <summary>
    /// ИД вида работ.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название вида работ.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Единица работ для вида работ.
    /// </summary>
    public WorkUnitModel WorkUnit { get; set; }

    #endregion
}
