namespace RM.BLL.Validators.Constants;

/// <summary>
/// Централизованное хранилище сообщений для валидаторов.
/// </summary>
public class ValidationMessages
{
    /// <summary>
    /// Сообщение для проверки, что числовое значение строго больше нуля.
    /// </summary>
    public const string NotLessThanOne = "Значение поля '{PropertyName}' не должно быть меньше единицы.";

    /// <summary>
    /// Сообщение для проверки на непустое значение.
    /// </summary>
    public const string NotEmpty = "Значение поля '{PropertyName}' не может быть пустым.";

    /// <summary>
    /// Сообщение о нарушении допустимой длины строки.
    /// </summary>
    public const string LengthRange =
        "Значение поля '{PropertyName}' должно содержать от {MinLength} до {MaxLength} символов. " +
        "Введено: {TotalLength} символов.";
}

