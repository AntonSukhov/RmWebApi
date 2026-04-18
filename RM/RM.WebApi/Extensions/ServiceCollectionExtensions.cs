using System;
using Infrastructure.Mapping.AutoMapper;
using Infrastructure.Mapping.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RM.BLL.Abstractions.Configuration;
using RM.BLL.Abstractions.Services;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Mapping.MapperSets;
using RM.BLL.Mapping.Profiles;
using RM.BLL.Services;
using RM.BLL.Validators;
using RM.Common.Services;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.DbContexts;
using RM.DAL.Mapping.Profiles;
using RM.DAL.Repositories;
using RM.WebApi.Mapping.MapperSets;

namespace RM.WebApi.Extensions;

/// <summary>
/// Расширение функционала объекта <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    private const string ConnectionStringsSection = "ConnectionStrings";

    /// <summary>
    /// Регистрация контекстов баз данных.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация свойств API.</param>
    public static void RegisterDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var dataStorageType = configuration.GetValue<string>(Constants.DataStorageTypeString);

        if (string.IsNullOrWhiteSpace(dataStorageType))
        {
            throw new InvalidOperationException(
                $"Конфигурация {nameof(Constants.DataStorageTypeString)} не задана.");
        }

        if (dataStorageType.Equals(Constants.MsSqlServer, StringComparison.OrdinalIgnoreCase))
        {
            var connectionString = GetConnectionStringOrException(configuration, 
                Constants.MsSqlDbContractConnectionString);
            
            services.AddDbContext<ContractGpdDbContextBase, DAL.MsSql.DbContexts.ContractGpdDbContext>(
                options => options.UseSqlServer(connectionString));
        }
        else if (dataStorageType.Equals(Constants.PostgreSql, StringComparison.OrdinalIgnoreCase))
        {
            var connectionString = GetConnectionStringOrException(configuration, 
                Constants.PostgreDbContractConnectionString);

            services.AddDbContext<ContractGpdDbContextBase, DAL.PostgreSql.DbContexts.ContractGpdDbContext>(
                options => options.UseNpgsql(connectionString));
        }
        else
        {
            throw new InvalidOperationException($"Неподдерживаемый тип хранилища данных: {dataStorageType}");
        }
    }

    /// <summary>
    /// Регистрация репозиториев.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWorkUnitRepository, WorkUnitRepository>();
        services.AddScoped<IWorkTypeRepository, WorkTypeRepository>();
    }

    /// <summary>
    /// Регистрация сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IWorkUnitService, WorkUnitService>();
        services.AddScoped<IWorkTypeService, WorkTypeService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }

    /// <summary>
    /// Регистрация валидаторов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddSingleton<IPageOptionsValidator, PageOptionsValidator>();
        services.AddSingleton<IWorkTypeNameValidator, WorkTypeNameValidator>();
        services.AddSingleton<IWorkTypeUpdationModelValidator, WorkTypeUpdationModelValidator>();
        services.AddSingleton<IAuthenticationCredentialsValidator, AuthenticationCredentialsValidator>();
        services.AddSingleton<WorkTypeNamePropertyValidator>();
    }

    /// <summary>
    /// Регистрация профилей настроек преобразований объектов через AutoMapper.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void RegisterAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(config => {}, 
            typeof(Mapping.Profiles.WorkUnitMappingProfile), 
            typeof(WorkUnitMappingProfile), 
            typeof(WorkTypeShortMappingProfile));
    }

    /// <summary>
    /// Регистрация кастомных мапперов для явных преобразований.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void RegisterCustomMappers(this IServiceCollection services)
    {
         services.AddSingleton(typeof(IMapper<,>), typeof(Mapper<,>));
         services.AddSingleton<IWorkTypeApiMappers, WorkTypeApiMappers>();
         services.AddSingleton<IWorkTypeBllMappers, WorkTypeBllMappers>();
    }

    /// <summary>
    /// Регистрация настроек API.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация свойств API.</param>
    public static void RegisterSettings(
        this IServiceCollection services, 
             IConfiguration configuration)
    {
        services.Configure<AuthenticationSettings>(
            configuration.GetSection(Constants.Authentication));

        services.AddSingleton(provider => 
            provider.GetRequiredService<IOptions<AuthenticationSettings>>().Value);
    }

    private static string GetConnectionStringOrException(
        IConfiguration configuration, 
        string connectionStringName)
    {
        var connectionStringsSection = configuration.GetSection(ConnectionStringsSection);
    
        var connectionString = connectionStringsSection[connectionStringName];

        if(string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"Строка подключения '{connectionStringName}' не найдена в секции '{nameof(ConnectionStringsSection)}' конфигурации.");
        }

        return connectionString;
    }
}
