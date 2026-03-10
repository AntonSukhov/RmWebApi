using System;

namespace RM.Api.DTOs.Responses;

/// <summary>
/// Ответ, представляющий вид работ.
/// </summary>
public class WorkTypeResponse
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
    public WorkUnitResponse? WorkUnit { get; set; }
}
