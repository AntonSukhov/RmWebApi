using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.DbContexts;
using RM.DAL.MsSql.DbContexts;
using RM.DAL.Repositories;
using RM.WebApi.Services;

namespace RM.WebApi.Extensions
{
    /// <summary>
    /// Расширение функционала объекта <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region Методы

        /// <summary>
        /// Регистрация контекстов баз данных в коллекцию сервисов 
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <param name="configuration">Конфигурация свойств приложения</param>
        public static void RegisterDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<string>(Constants.DataStorageTypeString) == Constants.MsSqlServer)
            {
                //TODO: вынести UseSqlServer отсюда
                services.AddDbContext<IContractGpdDbContext, ContractGpdDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(Constants.DefaultConnectionString)));
            }
        }

        /// <summary>
        /// Регистрация репозиториев в коллекцию сервисов
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWorkUnitRepository, WorkUnitRepository>();
            services.AddScoped<IWorkTypeRepository, WorkTypeRepository>();
        }

        /// <summary>
        /// Регистрация сервисов в коллекцию сервисов
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        public static void RegisterServices(this IServiceCollection services)
        {
            //services.AddScoped<IWorkUnitService, WorkUnitService>();
        }

        #endregion
    }
}
