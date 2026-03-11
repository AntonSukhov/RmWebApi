using System;

namespace RM.Api.DTOs.Requests;

/// <summary>
/// Запрос на обновление вида работ.
/// </summary>
public class WorkTypeUpdationRequest
{
    /// <summary>
    /// Получает или задает ИД обновляемого вида работ.
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
