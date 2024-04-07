using Moq;
using RM.BLL.Abstractions.Services;
using RM.BLL.Abstractions.Validators;
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
    /// Мок репозиторий единиц работ.
    /// </summary>
    public Mock<IWorkUnitRepository> WorkUnitRepositoryMock { get; }

    /// <summary>
    /// Валидатор названия вида работ.
    /// </summary>
    public IWorkTypeNameValidator WorkTypeNameValidator { get; }

    /// <summary>
    /// Валидатор модели обновления вида работ.
    /// </summary>
    public IWorkTypeUpdationModelValidator WorkTypeUpdationModelValidator { get; }

    /// <summary>
    /// Валидатор настроект страницы.
    /// </summary>
    public IPageOptionsValidator PageOptionsValidator { get; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    public WorkTypeServiceFixture()
    {
        WorkTypeRepositoryMock = new Mock<IWorkTypeRepository>();
        WorkUnitRepositoryMock = new Mock<IWorkUnitRepository>();
        WorkTypeNameValidator = new WorkTypeNameValidator();
        WorkTypeUpdationModelValidator = new WorkTypeUpdationModelValidator();
        PageOptionsValidator = new PageOptionsValidator();
    }

    #endregion
}
