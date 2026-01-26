using System;

namespace RM.DAL.Abstractions.Models;

/// <summary>
/// Модель сокращенного вида работ.
/// </summary>
public class WorkTypeShortModel
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
    /// Получает или задает ИД единицы работ для вида работ.
    /// </summary>
    public byte? WorkUnitId { get; set; }

}
