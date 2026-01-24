using System;

namespace RM.BLL.Exceptions;

/// <summary>
/// Исключение отсутствия данных.
/// </summary>
/// <remarks>Исключение возникает при отсутствии требуемых данных.</remarks>
public class DataNotFoundException : InvalidOperationException
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DataNotFoundException"/>.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public DataNotFoundException(string message) : base(message) { }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DataNotFoundException"/>.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="innerException">Исключение, которое вызвало текущее исключение.</param>
    public DataNotFoundException(string message, Exception innerException) 
        : base(message, innerException) { }

}
