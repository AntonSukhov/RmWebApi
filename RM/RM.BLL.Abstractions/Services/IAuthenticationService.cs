using System.Threading;
using System.Threading.Tasks;

namespace RM.BLL.Abstractions.Services;

/// <summary>
/// Сервис аутентификации пользователей.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Получает токен аутентифицированного пользователя.
    /// </summary>
    /// <param name="userName">Имя пользователя.</param>
    /// <param name="userPassword">Пароль пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Токен аутентифицированного пользователя.</returns>
    public Task<string> GetTokenAsync(string userName, string userPassword, 
        CancellationToken cancellationToken = default);
}
