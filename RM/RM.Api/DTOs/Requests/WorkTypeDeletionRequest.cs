using System;

namespace RM.Api.DTOs.Requests;

/// <summary>
/// Запрос на удаление вида работ.
/// </summary>
public class WorkTypeDeletionRequest
{
    /// <summary>
    /// Получает или задает ИД удаляемого вида работ.
    /// </summary>
    public Guid Id { get; set; }
}
