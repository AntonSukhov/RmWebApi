using Moq;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Tests.Services.WorkUnitService;

/// <summary>
/// Фикстура для тестирования методов сервиса <see cref="IWorkTypeService"/>.
/// </summary>
public class WorkUnitServiceFixture
{
    /// <summary>
    /// Получает мок-объект репозитория единицы работ.
    /// </summary>
    public Mock<IWorkUnitRepository> WorkUnitRepositoryMock { get; }

    /// <summary>
    /// Получает сервис единицы работ.
    /// </summary>
    public IWorkUnitService WorkUnitService { get; }

    /// <summary>
    /// Получает компаратор для сравнения объектов <see cref="WorkUnitModel"/>.
    /// </summary>
    public IEqualityComparer<WorkUnitModel?> WorkUnitModelEqualityComparer { get; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public WorkUnitServiceFixture()
    {
        WorkUnitRepositoryMock = new Mock<IWorkUnitRepository>();
        WorkUnitModelEqualityComparer = new WorkUnitModelEqualityComparer();
        WorkUnitService = new BLL.Services.WorkUnitService(WorkUnitRepositoryMock.Object);
    }

}
