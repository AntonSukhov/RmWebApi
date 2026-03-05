using Microsoft.Extensions.Options;
using Moq;
using RM.BLL.Abstractions.Configuration;
using RM.BLL.Abstractions.Services;
using RM.BLL.Validators;

namespace RM.BLL.Tests.Services.AuthenticationService;

/// <summary>
/// Фикстура для тестирования методов сервиса <see cref="IAuthenticationService/>.
/// </summary>
public class AuthenticationServiceFixture
{
    /// <summary>
    /// Получает мок-объект сервиса аутентификации пользователей.
    /// </summary>
    public Mock<IdentityWebApp.Api.Services.IAuthenticationService> AuthenticationServiceMock { get; }

    /// <summary>
    /// Получает сервис аутентификации пользователей.
    /// </summary>
    public IAuthenticationService AuthenticationService { get; }

    /// <summary>
    /// Инициализирует экземпляр <see cref="AuthenticationServiceFixture"/>.
    /// </summary>
    public AuthenticationServiceFixture()
    {
        var authenticationCredentialsValidator = new AuthenticationCredentialsValidator();

        var authenticationSettings = new AuthenticationSettings
        { 
            ServerName = "localhost",
            Port = 7121
        };

        var authenticationSettingsOptions = Options.Create(authenticationSettings);

        AuthenticationServiceMock = new Mock<IdentityWebApp.Api.Services.IAuthenticationService>();  

        AuthenticationService = new BLL.Services.AuthenticationService(
            AuthenticationServiceMock.Object,
            authenticationSettingsOptions,
            authenticationCredentialsValidator);
    }
}
