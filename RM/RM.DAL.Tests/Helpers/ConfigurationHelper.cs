using Microsoft.Extensions.Configuration;

namespace ContractGpdApiTests.Helpers
{
    /// <summary>
    /// Класс помощи в работе с конфигурациями
    /// </summary>
    internal static class ConfigurationHelper
    {
        #region Методы

        /// <summary>
        /// Метод получения строки подключения к БД
        /// </summary>
        /// <param name="configFilePath">Путь к конфигурационному файлу</param>
        /// <param name="connectionSectionName">Название секции конфигурационного файла для подключения к базе данных</param>
        /// <returns>Строка подключения к БД</returns>
        public static string GetConnectionString(string configFilePath = "appconfig.json", string connectionSectionName = "DefaultConnection") => new ConfigurationBuilder().AddJsonFile(configFilePath).Build().GetConnectionString(connectionSectionName);

        #endregion
    }
}
