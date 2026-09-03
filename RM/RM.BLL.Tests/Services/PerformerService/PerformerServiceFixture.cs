using AutoMapper;
using Infrastructure.Mapping.AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Mapping.MapperSets;
using RM.BLL.Mapping.Profiles;
using RM.BLL.Validators;
using RM.DAL.Abstractions.Entities;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Tests.Services.PerformerService;

/// <summary>
/// Фикстура для тестирования методов сервиса <see cref="IPerformerService"/>.
/// </summary>
public class PerformerServiceFixture
{
    /// <summary>
    /// Получает мок-объект репозитория исполнителей договоров.
    /// </summary>
    /// <value>
    /// Мок-объект репозитория исполнителей договоров.
    /// </value>
    public Mock<IPerformerRepository> PerformerRepositoryMock { get; }

    /// <summary>
    /// Получает сервис исполнителей договоров.
    /// </summary>
    /// <value>
    /// Сервис исполнителей договоров.
    /// </value>
    public IPerformerService PerformerService { get; }

    /// <summary>
    /// Получает компаратор для сравнения объектов <see cref="PerformerModel"/>.
    /// </summary>
    /// <value>
    /// Компаратор для сравнения объектов <see cref="PerformerModel"/>.
    /// </value>
    public IEqualityComparer<PerformerModel?> PerformerModelEqualityComparer { get; }

    /// <summary>
    /// Инициализирует экземпляр <see cref="PerformerServiceFixture"/>.
    /// </summary>
    public PerformerServiceFixture()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PageOptionsMappingProfile>();
            cfg.AddProfile<PerformerMappingProfile>();
        },
        NullLoggerFactory.Instance);

        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        var pageOptionsMapper = new Mapper<PageOptionsModel, Infrastructure.Shared.Models.PageOptionsModel>(mapper);
        var performerMapper = new Mapper<PerformerEntity, PerformerModel>(mapper);

        var pageOptionsBllMappers = new PageOptionsBllMappers(pageOptionsMapper);
        var performerBllMappers = new PerformerBllMappers(performerMapper);

        PerformerRepositoryMock = new Mock<IPerformerRepository>();
        PerformerModelEqualityComparer = new PerformerModelEqualityComparer();

        var pageOptionsValidator = new PageOptionsValidator();

        PerformerService = new BLL.Services.PerformerService(PerformerRepositoryMock.Object,
            pageOptionsValidator,
            pageOptionsBllMappers,
            performerBllMappers);
    }
}
