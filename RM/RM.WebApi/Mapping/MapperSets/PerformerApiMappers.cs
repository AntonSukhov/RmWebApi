using System;
using Infrastructure.Mapping.Interfaces;
using RM.Api.DTOs.Responses;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.MapperSets
{
    /// <summary>
    /// Реализация контейнера мапперов для работы с исполнителями договоров на уровне API.
    /// </summary>
    public class PerformerApiMappers : IPerformerApiMappers
    {
        /// <inheritdoc/>
        public IMapper<PerformerModel, PerformerResponse> ToPerformerResponse { get; }

        /// <summary>
        /// Инициализация экземпляра <see cref="PerformerApiMappers"/>.
        /// </summary>
        /// <param name="performerResponseMapper">Маппер для преобразования <see cref="PerformerModel"/> в <see cref="PerformerResponse"/>.</param>
        public PerformerApiMappers(IMapper<PerformerModel, PerformerResponse> performerResponseMapper)
        {
            ArgumentNullException.ThrowIfNull(performerResponseMapper, nameof(performerResponseMapper));

            ToPerformerResponse = performerResponseMapper;
        }
    }
}