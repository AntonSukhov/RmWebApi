using System;

namespace RM.BLL.Abstractions.Models;

/// <summary>
/// Модель получения вида работ по его идентификатору.
/// </summary>
public class WorkTypeGettingByIdModel
{
    #region Свойства

    /// <summary>
    /// Идентификатор вида работ.
    /// </summary>
    public Guid Id { get; set; }

    #endregion
}
