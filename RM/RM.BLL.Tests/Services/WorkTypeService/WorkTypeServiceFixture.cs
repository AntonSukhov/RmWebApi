using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Mapping.Profiles;
using RM.BLL.Validators;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Tests.Services.WorkTypeService;

/// <summary>
/// Фикстура для тестирования методов сервиса <see cref="IWorkTypeService"/>.
/// </summary>
public class WorkTypeServiceFixture
{
    /// <summary>
    /// Получает мок-объект репозитория вида работ.
    /// </summary>
    public Mock<IWorkTypeRepository> WorkTypeRepositoryMock { get; }

    /// <summary>
    /// Получает мок-объект репозитория единицы работ.
    /// </summary>
    public Mock<IWorkUnitRepository> WorkUnitRepositoryMock { get; }

    /// <summary>
    /// Получает сервис единицы работ.
    /// </summary>
    public IWorkUnitService WorkUnitService { get; }

    /// <summary>
    /// Получает сервис вида работ.
    /// </summary>
    public IWorkTypeService WorkTypeService { get; }

    /// <summary>
    /// Получает компаратор для сравнения объектов <see cref="WorkTypeModel"/>.
    /// </summary>
    public IEqualityComparer<WorkTypeModel?> WorkTypeModelEqualityComparer { get; }

    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeServiceFixture"/>.
    /// </summary>
    public WorkTypeServiceFixture()
    {
        var expr = new MapperConfigurationExpression();
        expr.AddProfile<WorkUnitMappingProfile>();
        expr.AddProfile<WorkTypeMappingProfile>();

        var config = new MapperConfiguration(expr);
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();

        WorkTypeRepositoryMock = new Mock<IWorkTypeRepository>();
        WorkUnitRepositoryMock = new Mock<IWorkUnitRepository>();

        WorkTypeModelEqualityComparer = new WorkTypeModelEqualityComparer();

        WorkUnitService = new BLL.Services.WorkUnitService(WorkUnitRepositoryMock.Object, mapper);
        WorkTypeService = new BLL.Services.WorkTypeService(WorkTypeRepositoryMock.Object, 
            WorkUnitRepositoryMock.Object,
            new WorkTypeNameValidator(),
            new WorkTypeUpdationModelValidator(),
            new PageOptionsValidator(), 
            mapper);
    }
}
