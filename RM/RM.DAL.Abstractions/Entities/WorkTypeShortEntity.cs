using System;

namespace RM.DAL.Abstractions.Entities;

/// <summary>
/// Сущность сокращенного вида работ.
/// </summary>
public class WorkTypeShortEntity
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
