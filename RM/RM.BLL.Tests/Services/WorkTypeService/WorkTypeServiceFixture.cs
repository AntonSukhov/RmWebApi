using AutoMapper;
using Infrastructure.Mapping.AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Mapping.MapperSets;
using RM.BLL.Mapping.Profiles;
using RM.BLL.Services;
using RM.BLL.Validators;
using RM.DAL.Abstractions.Entities;
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
    /// <value>
    /// Мок-объект репозитория вида работ.
    /// </value>
    public Mock<IWorkTypeRepository> WorkTypeRepositoryMock { get; }

    /// <summary>
    /// Получает мок-объект репозитория единицы работ.
    /// </summary>
    /// <value>
    /// Мок-объект репозитория единицы работ.
    /// </value>
    public Mock<IWorkUnitRepository> WorkUnitRepositoryMock { get; }

    /// <summary>
    /// Получает мок-объект репозитория исполнителей договоров.
    /// </summary>
    /// <value>
    /// Мок-объект репозитория исполнителей договоров.
    /// </value>
    public Mock<IPerformerRepository> PerformerRepositoryMock { get; }

    /// <summary>
    /// Получает сервис единицы работ.
    /// </summary>
    /// <value>
    /// Сервис единицы работ.
    /// </value>
    public IWorkUnitService WorkUnitService { get; }

    /// <summary>
    /// Получает сервис вида работ.
    /// </summary>
    /// <value>
    /// Сервис вида работ.
    /// </value>
    public IWorkTypeService WorkTypeService { get; }

    /// <summary>
    /// Получает сервис исполнителей договоров.
    /// </summary>
    /// <value>
    /// Сервис исполнителей договоров.
    /// </value>
    public IPerformerService PerformerService  { get; }

    /// <summary>
    /// Получает компаратор для сравнения объектов <see cref="WorkTypeModel"/>.
    /// </summary>
    /// <value>
    /// Компаратор для сравнения объектов <see cref="WorkTypeModel"/>.
    /// </value>
    public IEqualityComparer<WorkTypeModel?> WorkTypeModelEqualityComparer { get; }

    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeServiceFixture"/>.
    /// </summary>
    public WorkTypeServiceFixture()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WorkUnitMappingProfile>();
            cfg.AddProfile<WorkTypeMappingProfile>();
            cfg.AddProfile<PageOptionsMappingProfile>();
            cfg.AddProfile<WorkTypeCreationMappingProfile>();
            cfg.AddProfile<WorkTypeUpdationMappingProfile>();
            cfg.AddProfile<PerformerMappingProfile>();
        }, 
        NullLoggerFactory.Instance);

        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        var pageOptionsMapper = new Mapper<PageOptionsModel, Infrastructure.Shared.Models.PageOptionsModel>(mapper);
        var workUnitMapper = new Mapper<WorkUnitEntity, WorkUnitModel>(mapper);
        var performerMapper = new Mapper<PerformerEntity, PerformerModel>(mapper);
        
        var pageOptionsBllMappers = new PageOptionsBllMappers( pageOptionsMapper );
        var workTypeBllMappers = new WorkTypeBllMappers(
            new Mapper<WorkTypeCreationModel, WorkTypeShortEntity>(mapper),
            new Mapper<WorkTypeUpdationModel, WorkTypeShortEntity>(mapper),
            new Mapper<WorkTypeEntity, WorkTypeModel>(mapper)
        );
        var performerBllMappers = new PerformerBllMappers(performerMapper);
        
        WorkTypeRepositoryMock = new Mock<IWorkTypeRepository>();
        WorkUnitRepositoryMock = new Mock<IWorkUnitRepository>();
        PerformerRepositoryMock = new Mock<IPerformerRepository>();

        WorkTypeModelEqualityComparer = new WorkTypeModelEqualityComparer();

        var workTypeNamePropertyValidator = new WorkTypeNamePropertyValidator();
        var pageOptionsValidator = new PageOptionsValidator();

        WorkUnitService = new BLL.Services.WorkUnitService(WorkUnitRepositoryMock.Object, workUnitMapper);
        WorkTypeService = new BLL.Services.WorkTypeService(WorkTypeRepositoryMock.Object, 
            WorkUnitRepositoryMock.Object,
            new WorkTypeNameValidator( workTypeNamePropertyValidator),
            new WorkTypeUpdationModelValidator(workTypeNamePropertyValidator),
            pageOptionsValidator, 
            workTypeBllMappers,
            pageOptionsBllMappers);
        PerformerService = new PerformerService(PerformerRepositoryMock.Object,
            pageOptionsValidator,
            pageOptionsBllMappers,
            performerBllMappers);
        }     
}