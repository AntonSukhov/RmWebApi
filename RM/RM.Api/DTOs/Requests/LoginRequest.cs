namespace RM.Api.DTOs.Requests;

/// <summary>
/// Модель запроса для аутентификации пользователя.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Получает имя пользователя для аутентификации.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Получает пароль пользователя.
    /// </summary>
    public string UserPassword { get; set; } = string.Empty;
}
