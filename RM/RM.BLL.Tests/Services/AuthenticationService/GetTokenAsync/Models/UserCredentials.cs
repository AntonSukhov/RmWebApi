namespace RM.BLL.Tests.Services.AuthenticationService.GetTokenAsync.Models;

/// <summary>
/// Учетные данные пользователя.
/// </summary>
/// <param name="Login">Логин.</param>
/// <param name="Password">Пароль.</param>
public record UserCredentials(string Login, string Password);
