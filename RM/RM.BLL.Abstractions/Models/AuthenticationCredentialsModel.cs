namespace RM.BLL.Abstractions.Models;

/// <summary>
/// Модель учётных данных для аутентификации пользователя.
/// </summary>
public class AuthenticationCredentialsModel
{
    /// <summary>
    /// Получает или задает имя пользователя.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Получает или задает пароль пользователя.
    /// </summary>
    public string UserPassword { get; set; } = string.Empty;

    /// <summary>
    /// Получает или задает сервер аутентификации.
    /// </summary>
    public string ServerName { get; set; } = string.Empty;
}
