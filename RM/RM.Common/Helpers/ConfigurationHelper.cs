using Microsoft.Extensions.Configuration;

namespace RM.Common.Helpers;


/// <summary>
/// Помощник в работе с конфигурациями.
/// </summary>
public static class ConfigurationHelper
{
    #region Методы

    /// <summary>
    /// Получает строку подключения к БД.
    /// </summary>   
    /// <param name="connectionSectionName">Название секции конфигурационного файла для подключения к базе данных.</param>
    /// <param name="configFilePath">Путь к конфигурационному файлу.</param>
    /// <returns>Строка подключения к БД.</returns>
    public static string? GetConnectionString(string connectionSectionName, string configFilePath = "appconfig.json") 
    {      

        return new ConfigurationBuilder().AddJsonFile(configFilePath)
                                         .Build()
                                         .GetConnectionString(connectionSectionName);
    } 

    #endregion
}
