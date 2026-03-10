using System;

namespace RM.Api.DTOs.Requests;

/// <summary>
/// Запрос на получение вида работ по его ИД.
/// </summary>
public class WorkTypeGettingByIdRequest
{
    /// <summary>
    /// Получает или задает ИД вида работ.
    /// </summary>
    public Guid Id { get; set; }
}
