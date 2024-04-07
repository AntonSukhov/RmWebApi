using System;

namespace RM.BLL.Abstractions.Models;

/// <summary>
/// Модель обновления вида работ.
/// </summary>
public class WorkTypeUpdationModel
{
   #region Свойства

    /// <summary>
    /// Идентификатор обновляемого вида работ.
    /// </summary>
    public Guid Id { get; set; }

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
