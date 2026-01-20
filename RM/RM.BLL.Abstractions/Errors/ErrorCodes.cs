namespace RM.BLL.Abstractions.Errors;

/// <summary>
/// Коды ошибок.
/// </summary>
public static class ErrorCodes
{
    /// <summary>
    /// Ошибка валидации данных.
    /// </summary>
    /// <remarks>
    /// Возникает при нарушении правил проверки входных параметров.
    /// </remarks>
    public const string Validation = "validation";
    
    /// <summary>
    /// Ошибка конкурентного доступа.
    /// </summary>
    /// <remarks>
    /// Возникает при конфликте одновременных изменений данных.
    /// </remarks>
    public const string Concurrency = "concurrency";
    
    /// <summary>
    /// Общая внутренняя ошибка.
    /// </summary>
    /// <remarks>
    /// Используется, когда конкретная причина ошибки неизвестна или не требует детализации.
    /// </remarks>
    public const string Generic = "internal_error";
}
