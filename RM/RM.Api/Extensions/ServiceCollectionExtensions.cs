using System;
using Microsoft.Extensions.DependencyInjection;
using RM.Api.Services;
using RM.Common.Constants;

namespace RM.Api.Extensions;

/// <summary>
/// Расширение функционала объекта <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRmWebApi (
        this IServiceCollection services,
        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        services.AddHttpClient(ApiConstants.RmWebApiClientName, client =>
        {          
            client.Timeout = TimeSpan.FromSeconds(ApiConstants.DefaultHttpClientTimeoutSeconds);
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
