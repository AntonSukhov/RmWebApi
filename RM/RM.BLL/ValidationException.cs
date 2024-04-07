using System;

namespace RM.BLL;

/// <summary>
/// Исключение валидации.
/// </summary>
public class ValidationException : Exception
{  
    #region Конструкторы

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    /// <param name="message">Сообщение об ошибки.</param>
    public ValidationException(string message) : base(message)
    {
    }

    #endregion
}
