namespace RM.WebApi.Models.Requests;

/// <summary>
/// Модель запроса для аутентификации пользователя.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Получает имя пользователя для аутентификации.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Получает пароль пользователя.
    /// </summary>
    public string UserPassword { get; set; }
}
