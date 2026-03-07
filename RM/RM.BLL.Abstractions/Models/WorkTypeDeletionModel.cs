using System;

namespace RM.BLL.Abstractions.Models;

/// <summary>
/// Модель удаления вида работ.
/// </summary>
public class WorkTypeDeletionModel
{
    /// <summary>
    /// Получает или задает ИД удаляемого вида работ.
    /// </summary>
    public Guid Id { get; set; }
}
