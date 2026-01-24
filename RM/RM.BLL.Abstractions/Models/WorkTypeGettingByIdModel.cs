using System;

namespace RM.BLL.Abstractions.Models;

/// <summary>
/// Модель получения вида работ по ИД.
/// </summary>
public class WorkTypeGettingByIdModel
{
    /// <summary>
    /// Получает или задает ИД вида работ.
    /// </summary>
    public Guid Id { get; set; }

}
