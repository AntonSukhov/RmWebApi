using System;

namespace RM.BLL.Abstractions.Models;

/// <summary>
/// Модель вида работ.
/// </summary>
public class WorkTypeModel
{
    /// <summary>
    /// Получает или задает ИД вида работ.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Получает или задает название вида работ.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Получает или задает единицу работ для вида работ.
    /// </summary>
    public WorkUnitModel? WorkUnit { get; set; }

}
