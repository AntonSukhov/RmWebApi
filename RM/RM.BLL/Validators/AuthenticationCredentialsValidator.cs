using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Extensions;
using RM.BLL.Validators.Constants;

namespace RM.BLL.Validators;

/// <summary>
/// Реализация валидатора учётных данных для аутентификации пользователя.
/// </summary>
public class AuthenticationCredentialsValidator : AbstractValidator<AuthenticationCredentialsModel>,
                                                  IAuthenticationCredentialsValidator
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="AuthenticationCredentialsValidator"/>.
    /// </summary>
    public AuthenticationCredentialsValidator()
    {
        RuleFor(p => p.UserName).NotEmpty()
                                .WithMessage(ValidationMessages.NotEmpty)
                                .WithName(FieldNames.UserName);

        RuleFor(p => p.UserName).Length(1, 256)
                                .When(p => !string.IsNullOrWhiteSpace(p.UserName))
                                .WithMessage(ValidationMessages.LengthRange)
                                .WithName(FieldNames.UserName);

        RuleFor(p => p.UserPassword).NotEmpty()
                                    .WithMessage(ValidationMessages.NotEmpty)
                                    .WithName(FieldNames.UserPassword);

        RuleFor(p => p.ServerName).NotEmpty()
                                    .WithMessage(ValidationMessages.NotEmpty)
                                    .WithName(FieldNames.ServerName);
    }


    /// <inheritdoc/>
    public async Task ValidateAndThrowAsync(
        AuthenticationCredentialsModel value, 
        CancellationToken cancellationToken = default)
    {
        await this.ValidateAndThrowCustomAsync(value, cancellationToken);
    }
}
