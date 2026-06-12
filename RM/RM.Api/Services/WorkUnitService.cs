using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RM.Api.GeneratedApiClients;
using RM.Api.Mapping.Extensions;
using RM.Common.Constants;
using WorkUnitResponse = RM.Api.DTOs.Responses.WorkUnitResponse;

namespace RM.Api.Services;

/// <summary>
/// Реализация сервиса единицы работ.
/// </summary>
public class WorkUnitService : IWorkUnitService
{
    private readonly RmWebApiClient _rmWebApiClient;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorkUnitService"/>.
    /// </summary>
    /// <param name="httpClientFactory">Фабрика для создания <see cref="HttpClient"/>.</param>
    public WorkUnitService(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);

        _rmWebApiClient = new RmWebApiClient(httpClientFactory.CreateClient(
            ApiConstants.RmWebApiClientName));
    }


    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<WorkUnitResponse>> GetAllAsync(
        CancellationToken? cancellationToken = null)
    {
        var cancellationTokenLocal = cancellationToken ?? CancellationToken.None;
        var workUnits = await _rmWebApiClient.GetWorkUnitsAsync(cancellationTokenLocal) 
            ?? Enumerable.Empty<GeneratedApiClients.WorkUnitResponse>();
        
        var results = workUnits.Select(wu => wu.ToWorkUnitResponse()).ToArray();

        return results;
    }
}
