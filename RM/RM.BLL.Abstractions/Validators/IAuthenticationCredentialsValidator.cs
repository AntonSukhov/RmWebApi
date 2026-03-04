using RM.BLL.Abstractions.Models;

namespace RM.BLL.Abstractions.Validators;

/// <summary>
/// Валидатор учётных данных для аутентификации пользователя.
/// </summary>
public interface IAuthenticationCredentialsValidator: IAbstractValidator<AuthenticationCredentialsModel>
{
}
