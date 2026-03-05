using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RM.BLL.Abstractions.Configuration;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Abstractions.Validators;

namespace RM.BLL.Services;

/// <summary>
/// Реализация сервиса аутентификации пользователей.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IdentityWebApp.Api.Services.IAuthenticationService _authenticationService;
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly IAuthenticationCredentialsValidator _authenticationCredentialsValidator;

    /// <summary>
    /// Инициализирует экземпляр <see cref="AuthenticationService"/>.
    /// </summary>
    /// <param name="authenticationService">Сервис аутентификации пользователей.</param>
    /// <param name="authenticationSettings">Настройки для подключения к сервису аутентификации.</param>
    /// <param name="authenticationCredentialsValidator">Валидатор учётных данных для аутентификации пользователя.</param>
    public AuthenticationService(
        IdentityWebApp.Api.Services.IAuthenticationService  authenticationService,
        IOptions<AuthenticationSettings> authenticationSettings,
        IAuthenticationCredentialsValidator authenticationCredentialsValidator)
    {
        ArgumentNullException.ThrowIfNull(authenticationService);
        ArgumentNullException.ThrowIfNull(authenticationSettings);
        ArgumentNullException.ThrowIfNull(authenticationSettings.Value);
        ArgumentNullException.ThrowIfNull(authenticationCredentialsValidator);

        _authenticationService = authenticationService;
        _authenticationSettings = authenticationSettings.Value;
        _authenticationCredentialsValidator = authenticationCredentialsValidator;
    }

    /// <inheritdoc/>
    public async Task<string> GetTokenAsync(
        string userName, 
        string userPassword, 
        CancellationToken cancellationToken = default)
    {
        var authenticationCredentials = new AuthenticationCredentialsModel 
        {   
            UserName = userName, 
            UserPassword = userPassword, 
            ServerName = _authenticationSettings.ServerName
        };

        await _authenticationCredentialsValidator.ValidateAndThrowAsync(
            authenticationCredentials, cancellationToken);

        var tokenModel =  await _authenticationService.LoginAsync(
           serverName: _authenticationSettings.ServerName,
           port: _authenticationSettings.Port,
           userName: userName,
           password: userPassword,
           useHttps: _authenticationSettings.UseHttps,
           cancellationToken: cancellationToken
        );

        return tokenModel.Value;
    }
}
