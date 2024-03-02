using FluentValidation;
using RM.BLL.Abstractions.Models;

namespace RM.BLL.Validators;

/// <summary>
/// Проверяет настройки страницы.
/// </summary>
public class PageOptionsValidator: AbstractValidator<PageOptionsModel>
{
    
    #region Конструкторы

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public PageOptionsValidator()
    {
        #region Порядковый номер страницы.

        var propertyName = "Порядковый номер страницы";

        RuleFor(p => p.PageNumber).GreaterThan(0)
                                  .WithMessage("Значение поля '{PropertyName}' не должно быть меньше единицы.")
                                  .WithName(propertyName);

        #endregion

        #region Кол-во элементов страницы

        propertyName = "Кол-во элементов страницы";

        RuleFor(p => p.PageSize).GreaterThan(0)
                                .WithMessage("Значение поля '{PropertyName}' не должно быть меньше единицы.")
                                .WithName(propertyName);

        #endregion
    }

    #endregion
}
