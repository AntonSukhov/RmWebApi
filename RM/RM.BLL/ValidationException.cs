using System;

namespace RM.BLL;

/// <summary>
/// Исключение валидации.
/// </summary>
public class ValidationException : Exception
{  
    /// <summary>
    /// Инициализирует экземпляр <see cref="ValidationException"/>.
    /// </summary>
    public ValidationException(): base() {}

    /// <summary>
    /// Инициализирует экземпляр <see cref="ValidationException"/>.
    /// </summary>
    /// <param name="message">Сообщение об ошибки.</param>
    public ValidationException(string message) : base(message) { }
}
