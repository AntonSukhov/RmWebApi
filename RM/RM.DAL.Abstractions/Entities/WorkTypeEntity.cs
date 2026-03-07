using System;

namespace RM.DAL.Abstractions.Entities;

/// <summary>
/// Сущность вида работ.
/// </summary>
public class WorkTypeEntity
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

    /// <summary>
    /// Получает или задает единицу работ для вида работ.
    /// </summary>
    public WorkUnitEntity? WorkUnit { get; set; }

}

