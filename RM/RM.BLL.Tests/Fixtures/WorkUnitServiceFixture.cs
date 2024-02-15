using Moq;
using RM.BLL.Abstractions.Services;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Tests;

/// <summary>
/// Фикстура для тестирования методов сервиса <see cref="IWorkTypeService"/>.
/// </summary>
public class WorkUnitServiceFixture
{
   #region Свойства

    /// <summary>
    /// Мок репозиторий единицы работ.
    /// </summary>
    public Mock<IWorkUnitRepository> WorkUnitRepositoryMock { get; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    public WorkUnitServiceFixture()
    {
        WorkUnitRepositoryMock = new Mock<IWorkUnitRepository>();
    }

    #endregion
}
