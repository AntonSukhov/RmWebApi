using System;
using System.Threading;
using System.Threading.Tasks;
using RM.BLL.Abstractions.Services;

namespace RM.BLL.Services;

/// <summary>
/// Реализация сервиса аутентификации пользователей.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IdentityWebApp.Api.Services.IAuthenticationService _authenticationService;

    /// <summary>
    /// Инициализирует экземпляр <see cref="AuthenticationService"/>.
    /// </summary>
    /// <param name="authenticationService">Сервис аутентификации пользователей.</param>
    public AuthenticationService(
        IdentityWebApp.Api.Services.IAuthenticationService  authenticationService)
    {
        ArgumentNullException.ThrowIfNull(authenticationService);

        _authenticationService = authenticationService;
    }

    /// <inheritdoc/>
    public async Task<string> GetTokenAsync(
        string userName, 
        string userPassword, 
        CancellationToken cancellationToken = default)
    {
        var tokenModel =  await _authenticationService.LoginAsync(
           userName: userName,
           password: userPassword,
           cancellationToken: cancellationToken
        );

        return tokenModel.Value;
    }
}
