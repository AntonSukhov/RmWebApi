using Moq;
using RM.BLL.Abstractions.Services;
using RM.BLL.Validators;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Tests;

/// <summary>
/// Фикстура для тестирования методов сервиса <see cref="IWorkTypeService"/>.
/// </summary>
public class WorkTypeServiceFixture
{
   #region Свойства

    /// <summary>
    /// Мок репозиторий видов работ.
    /// </summary>
    public Mock<IWorkTypeRepository> WorkTypeRepositoryMock { get; }

    /// <summary>
    /// Валидатор вида работ.
    /// </summary>
    public WorkTypeValidator WorkTypeValidator { get; }

    /// <summary>
    /// Валидатор настроект страницы.
    /// </summary>
    public PageOptionsValidator PageOptionsValidator { get; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    public WorkTypeServiceFixture()
    {
        WorkTypeRepositoryMock = new Mock<IWorkTypeRepository>();
        WorkTypeValidator = new WorkTypeValidator();
        PageOptionsValidator = new PageOptionsValidator();
    }

    #endregion
}
