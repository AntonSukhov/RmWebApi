using System;
using System.IO;
using System.Reflection;
using Infrastructure.Mapping.AutoMapper;
using Infrastructure.Mapping.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
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
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection RegisterDbContexts(
        this IServiceCollection services, 
            IConfiguration configuration)
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

        return services;
    }

    /// <summary>
    /// Регистрация репозиториев.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWorkUnitRepository, WorkUnitRepository>();
        services.AddScoped<IWorkTypeRepository, WorkTypeRepository>();

        return services;
    }

    /// <summary>
    /// Регистрация сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IWorkUnitService, WorkUnitService>();
        services.AddScoped<IWorkTypeService, WorkTypeService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }

    /// <summary>
    /// Регистрация валидаторов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services.AddSingleton<IPageOptionsValidator, PageOptionsValidator>();
        services.AddSingleton<IWorkTypeNameValidator, WorkTypeNameValidator>();
        services.AddSingleton<IWorkTypeUpdationModelValidator, WorkTypeUpdationModelValidator>();
        services.AddSingleton<IAuthenticationCredentialsValidator, AuthenticationCredentialsValidator>();
        services.AddSingleton<WorkTypeNamePropertyValidator>();

        return services;
    }

    /// <summary>
    /// Регистрация профилей настроек преобразований объектов через AutoMapper.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection RegisterAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(config => {}, 
            typeof(Mapping.Profiles.WorkUnitMappingProfile), 
            typeof(WorkUnitMappingProfile), 
            typeof(WorkTypeShortMappingProfile));

        return services;
    }

    /// <summary>
    /// Регистрация кастомных мапперов для явных преобразований.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static IServiceCollection RegisterCustomMappers(this IServiceCollection services)
    {
         services.AddSingleton(typeof(IMapper<,>), typeof(Mapper<,>));
         services.AddSingleton<IWorkTypeApiMappers, WorkTypeApiMappers>();
         services.AddSingleton<IWorkTypeBllMappers, WorkTypeBllMappers>();

         return services;
    }

    /// <summary>
    /// Регистрация настроек API.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация свойств API.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection RegisterSettings(
        this IServiceCollection services, 
             IConfiguration configuration)
    {
        services.Configure<AuthenticationSettings>(
            configuration.GetSection(Constants.Authentication));

        services.AddSingleton(provider => 
            provider.GetRequiredService<IOptions<AuthenticationSettings>>().Value);

        return services;
    }

    /// <summary>
    /// Настраивает Swagger (OpenAPI) для API.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "RM.WebApi",
                Version = "v1"
            });

            var xmlFileCurrentProject = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileCurrentProject));

            options.EnableAnnotations();
        });

        return services;
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
