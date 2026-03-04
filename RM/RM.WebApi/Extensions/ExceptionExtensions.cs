using System;
using System.Linq;

namespace RM.WebApi.Extensions;

/// <summary>
/// Расширение функционала объекта <see cref="Exception"/>.
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Определяет, является ли исключение ошибкой аутентификации (неверный логин/пароль).
    /// </summary>
    /// <param name="exception">Исключение для проверки.</param>
    /// <returns>true, если исключение указывает на ошибку аутентификации; иначе false.</returns>
    public static bool IsAuthenticationFailure(this Exception exception)
    {
        var baseException = exception.GetBaseException();

        if (baseException is not InvalidOperationException invalidException)
            return false;

        var errorMessages = new[]
        {
            "Неверный логин или пароль",
            "Invalid username or password",
            "Incorrect credentials",
            "Authentication failed",
            "User not found or wrong password"
        };

        return errorMessages.Any(msg =>
            invalidException.Message.Contains(msg, StringComparison.OrdinalIgnoreCase));
    }
}
