namespace RM.BLL.Abstractions.Configuration;

/// <summary>
/// Настройки для подключения к сервису аутентификации.
/// </summary>
public class AuthenticationSettings
{
    /// <summary>
    /// Получает или задает имя сервера аутентификации.
    /// </summary>
    public required string ServerName { get; set; }

    /// <summary>
    /// Получает или задает порт сервера аутентификации.
    /// </summary>
    public int? Port { get; set; }

    /// <summary>
    /// Получает или задает признак использования протокола HTTPS.
    /// </summary>
    public bool UseHttps { get; set; }
}
