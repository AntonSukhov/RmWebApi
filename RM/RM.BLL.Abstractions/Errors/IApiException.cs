namespace RM.BLL.Abstractions.Errors;

/// <summary>
/// Исключение API.
/// </summary>
public interface IApiException
{
    /// <summary>
    /// Получает код ошибки.
    /// </summary>
    /// <value>Код ошибки.</value>
    public string Code { get; }
}
