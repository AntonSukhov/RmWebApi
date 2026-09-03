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
using PerformerResponse = RM.Api.DTOs.Responses.PerformerResponse;

namespace RM.Api.Services
{
    /// <summary>
    /// Реализация сервиса исполнителей договоров для работы с внешним API.
    /// </summary>
    public class PerformerService : IPerformerService
    {
        private readonly RmWebApiClient _rmWebApiClient;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PerformerService"/>.
        /// </summary>
        /// <param name="httpClientFactory">Фабрика для создания <see cref="HttpClient"/>.</param>
        public PerformerService(IHttpClientFactory httpClientFactory)
        {
            ArgumentNullException.ThrowIfNull(httpClientFactory);

            _rmWebApiClient = new RmWebApiClient(httpClientFactory.CreateClient(
                ApiConstants.RmWebApiClientName));
        }
        
        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<PerformerResponse>> GetAllAsync(
            PageOptionsRequest pageOptions, 
            CancellationToken? cancellationToken = null)
        {
            var cancellationTokenLocal = cancellationToken ?? CancellationToken.None;
            var performers = await _rmWebApiClient.GetPerformersAsync(
                pageOptions.PageNumber,
                pageOptions.PageSize,
                cancellationTokenLocal) ?? Enumerable.Empty<GeneratedApiClients.PerformerResponse>();

            return performers.Select(wt => wt.ToPerformerResponse()).ToArray();
        }

       
        /// <inheritdoc/>
        public async Task<PerformerResponse?> GetByIdAsync(
            Guid performerId, 
            CancellationToken? cancellationToken = null)
        {
            var cancellationTokenLocal = cancellationToken ?? CancellationToken.None;
            var performer = await _rmWebApiClient.GetPerformerAsync(performerId, cancellationTokenLocal);

            return performer?.ToPerformerResponse();
        }
    }
}