using RM.BLL.Abstractions.Services;

namespace RM.BLL.Tests.TestSupport.Constants;

/// <summary>
/// Набор константных строк с именами методов сервисов для использования в тестах.
/// </summary>
public static class ServiceMethodNames
{
    /// <summary>
    /// Константы для методов сервиса <see cref="IAuthenticationService"/>.
    /// </summary>
    public static class AuthenticationService
    {
        /// <summary>
        /// Имя метода <see cref="IAuthenticationService.GetTokenAsync"/>.
        /// </summary>
        public const string GetTokenAsync = nameof(IAuthenticationService.GetTokenAsync);
    }
}
