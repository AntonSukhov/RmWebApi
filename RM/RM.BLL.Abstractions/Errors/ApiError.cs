using System.Collections.Generic;

namespace RM.BLL.Abstractions.Errors;

/// <summary>
/// Ошибка API.
/// </summary>
public class ApiError
{
    /// <summary>
    /// Получает или задает код ошибки.
    /// </summary>
    /// <remarks>
    /// Обязательное поле. Должно быть задано при создании объекта.
    /// </remarks>
    public required string Code { get; set; }
    
    /// <summary>
    /// Получает или задает сообщение ошибки.
    /// </summary>
    /// <remarks>
    /// Обязательное поле. Должно быть задано при создании объекта.
    /// </remarks>
    public required string Message { get; set; }
    
    /// <summary>
    /// Получает или задает список пояснений или деталей ошибки.
    /// </summary>
    public IReadOnlyCollection<string> Details { get; set; } = [];
}
