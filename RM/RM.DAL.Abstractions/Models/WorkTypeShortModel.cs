using System;

namespace RM.DAL.Abstractions.Models;

/// <summary>
/// Модель сокращенного вида работ.
/// </summary>
public class WorkTypeShortModel
{
   #region Свойства

    /// <summary>
    /// Идентификатор вида работ.
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
