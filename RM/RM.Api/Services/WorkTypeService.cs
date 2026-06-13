using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RM.Api.DTOs.Requests;
using RM.Api.GeneratedApiClients;
using RM.Api.Mapping.Extensions;
using RM.Common.Constants;
using WorkTypeCreationRequest = RM.Api.DTOs.Requests.WorkTypeCreationRequest;
using WorkTypeResponse = RM.Api.DTOs.Responses.WorkTypeResponse;
using WorkTypeUpdationRequest = RM.Api.DTOs.Requests.WorkTypeUpdationRequest;

namespace RM.Api.Services;

/// <summary>
/// Реализация сервиса видов работ для работы с внешним API.
/// </summary>
public class WorkTypeApiService : IWorkTypeApiService
{
    private readonly RmWebApiClient _rmWebApiClient;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorkTypeApiService"/>.
    /// </summary>
    /// <param name="httpClientFactory">Фабрика для создания <see cref="HttpClient"/>.</param>
    public WorkTypeApiService(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);

        _rmWebApiClient = new RmWebApiClient(httpClientFactory.CreateClient(
            ApiConstants.RmWebApiClientName));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<WorkTypeResponse>> GetAllAsync(
        PageOptionsRequest pageOptions,
        CancellationToken? cancellationToken = null)
    {
        var cancellationTokenLocal = cancellationToken ?? CancellationToken.None;
        var workTypes = await _rmWebApiClient.GetWorkTypesAsync(
            pageOptions.PageNumber,
            pageOptions.PageSize,
            cancellationTokenLocal) ?? Enumerable.Empty<GeneratedApiClients.WorkTypeResponse>();

        return workTypes.Select(wt => wt.ToWorkTypeResponse()).ToArray();
    }

    /// <inheritdoc/>
    public async Task<WorkTypeResponse?> GetByIdAsync(Guid workTypeId, CancellationToken? cancellationToken = null)
    {
        var cancellationTokenLocal = cancellationToken ?? CancellationToken.None;
        var workType = await _rmWebApiClient.GetWorkTypeAsync(workTypeId, cancellationTokenLocal);

        return workType?.ToWorkTypeResponse();
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(
        WorkTypeCreationRequest workTypeCreationRequest,
        CancellationToken? cancellationToken = null)
    {
        var cancellationTokenLocal = cancellationToken ?? CancellationToken.None;
        var request = workTypeCreationRequest.ToWorkTypeCreationRequest();

        return await _rmWebApiClient.CreateWorkTypeAsync(request, cancellationTokenLocal);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid workTypeId, CancellationToken? cancellationToken = null)
    {
        var cancellationTokenLocal = cancellationToken ?? CancellationToken.None;
        await _rmWebApiClient.DeleteWorkTypeAsync(workTypeId, cancellationTokenLocal);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(
        WorkTypeUpdationRequest workTypeUpdationRequest,
        CancellationToken? cancellationToken = null)
    {
        var cancellationTokenLocal = cancellationToken ?? CancellationToken.None;
        var request = workTypeUpdationRequest.ToWorkTypeUpdationRequest();

        await _rmWebApiClient.UpdateWorkTypeAsync(request, cancellationTokenLocal);
    }
}
