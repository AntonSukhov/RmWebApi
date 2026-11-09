using System;
using Microsoft.Extensions.DependencyInjection;
using RM.Api.Services;
using RM.Api.Constants;

namespace RM.Api.Extensions;

/// <summary>
/// Расширение функционала объекта <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует сервисы RM Web API в контейнере зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="serviceLifetime">Время жизни регистрируемых сервисов.</param>
    /// <returns>Коллекция сервисов для дальнейшей конфигурации.</returns>
    public static IServiceCollection AddRmWebApi (
        this IServiceCollection services,
        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        services.AddHttpClient(ApiClientConstants.RmWebApiClientName, client =>
        {          
            client.Timeout = TimeSpan.FromSeconds(ApiClientConstants.DefaultHttpClientTimeoutSeconds);
        });

        var serviceDescriptor = new ServiceDescriptor(
            typeof(IWorkUnitService),
            typeof(WorkUnitService),
            serviceLifetime
        );

        services.Add(serviceDescriptor);

        serviceDescriptor = new ServiceDescriptor(
            typeof(IWorkTypeService),
            typeof(WorkTypeService),
            serviceLifetime
        );

        services.Add(serviceDescriptor);

        return services;
    }
}
