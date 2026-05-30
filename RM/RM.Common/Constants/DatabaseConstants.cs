namespace RM.Common.Constants;

/// <summary>
/// Константы, связанные с конфигурацией баз данных.
/// </summary>
public static class DatabaseConstants
{
    /// <summary>
    /// Имя строки подключения к базе данных MS SQL.
    /// </summary>
    public const string MsSqlDbContractConnectionString = "MsSqlDbContractConnection";

    /// <summary>
    /// Имя строки подключения к базе данных PostgreSQL.
    /// </summary>
    public const string PostgreDbContractConnectionString = "PostgreDbContractConnection";

    /// <summary>
    /// Ключ для указания типа хранилища данных.
    /// </summary>
    public const string DataStorageTypeString = "DataStorageType";

    /// <summary>
    /// Константы, определяющие типы систем управления базами данных (СУБД).
    /// </summary>
    public static class DbmsTypes
    {
        public const string MsSqlServer = "MSSQL";
        public const string PostgreSql = "PostgreSQL";
    }
}
