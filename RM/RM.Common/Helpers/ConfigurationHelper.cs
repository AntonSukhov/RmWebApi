using Microsoft.Extensions.Configuration;

namespace RM.Common.Helpers;


/// <summary>
/// Помощник в работе с конфигурациями.
/// </summary>
public static class ConfigurationHelper
{
    #region Поля

    private static readonly ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

    #endregion

    #region Методы

    /// <summary>
    /// Получает строку подключения к БД.
    /// </summary>
    /// <param name="configFilePath">Путь к конфигурационному файлу.</param>
    /// <param name="connectionSectionName">Название секции конфигурационного файла для подключения к базе данных.</param>
    /// <returns>Строка подключения к БД.</returns>
    public static string? GetConnectionString(string configFilePath = "appconfig.json", string connectionSectionName = "DefaultConnection") 
    {
        return configurationBuilder.AddJsonFile(configFilePath).Build().GetConnectionString(connectionSectionName);
    } 

    #endregion
}
