using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RM.BLL.Abstractions.Services;
using RM.WebApi.Models.Requests;

namespace RM.WebApi.Controllers;

/// <summary>
/// Контроллер аутентификации пользователей.
/// </summary>
[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    /// <summary>
    /// Инициализирует экземпляр <see cref="AuthenticationController"/>.
    /// </summary>
    /// <param name="authenticationService">Сервис аутентификации пользователей.</param>
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        ArgumentNullException.ThrowIfNull(authenticationService, nameof(authenticationService));

        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Выполняет аутентификацию пользователя.
    /// </summary>
    /// <param name="loginRequest">Запрос для аутентификации пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Токен аутентифицированного пользователя.</returns>
    [HttpPost("login")]
    public async Task<string> LoginAsync([FromBody] LoginRequest loginRequest, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(loginRequest, nameof(loginRequest));

        var token = await _authenticationService.GetTokenAsync(
            loginRequest.UserName,
            loginRequest.UserPassword,
            cancellationToken
        );

        return token;
    }
}
