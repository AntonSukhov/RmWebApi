namespace RM.Api.Constants;

/// <summary>
/// Константы для настройки HTTP-клиента RM Web API.
/// </summary>
internal static class ApiClientConstants
{
    /// <summary>
    /// Имя клиента для взаимодействия с веб-API RM.
    /// </summary>
    internal const string RmWebApiClientName = "RmWebApi";

    /// <summary>
    /// Таймаут HTTP-клиента по умолчанию (в секундах).
    /// </summary>
    internal const int DefaultHttpClientTimeoutSeconds = 30;
}