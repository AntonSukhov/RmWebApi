using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RM.BLL;
using RM.BLL.Abstractions.Services;
using RM.BLL.Services;
using RM.BLL.Validators;
using RM.Common.Services;
using RM.DAL;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Repositories;

namespace RM.WebApi.Extensions;

/// <summary>
/// Расширение функционала объекта <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    #region Методы

    /// <summary>
    /// Регистрация контекстов баз данных в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация свойств приложения.</param>
    public static void RegisterDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<string>(Constants.DataStorageTypeString) == Constants.MsSqlServer)
        {
            //TODO: вынести UseSqlServer отсюда
            services.AddDbContext<ContractGpdDbContextBase, DAL.MsSql.DbContexts.ContractGpdDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(Constants.MsSqlDbContractConnectionString)));
        }
        else if (configuration.GetValue<string>(Constants.DataStorageTypeString) == Constants.PostgreSql)
        {
            //TODO: вынести UseNpgsql отсюда
            services.AddDbContext<ContractGpdDbContextBase, DAL.PostgreSql.DbContexts.ContractGpdDbContext>(options => options.UseNpgsql(configuration.GetConnectionString(Constants.PostgreDbContractConnectionString)));
        }
    }

    /// <summary>
    /// Регистрация репозиториев в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWorkUnitRepository, WorkUnitRepository>();
        services.AddScoped<IWorkTypeRepository, WorkTypeRepository>();
    }

    /// <summary>
    /// Регистрация сервисов в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IWorkUnitService, WorkUnitService>();
        services.AddScoped<IWorkTypeService, WorkTypeService>();
    }

    /// <summary>
    /// Регистрация валидаторов в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddSingleton<PageOptionsValidator>();
        services.AddSingleton<WorkTypeValidator>();
    }

    #endregion
}
