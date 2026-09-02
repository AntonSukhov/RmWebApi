using Infrastructure.Mapping.Interfaces;
using RM.Api.DTOs.Responses;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.MapperSets
{
    /// <summary>
    /// Контейнер мапперов для работы с исполнителями договоров на уровне API.
    /// </summary>
    public interface IPerformerApiMappers
    {
        /// <summary>
        /// Получает маппер для преобразования <see cref="PerformerModel"/> (из BLL) 
        /// в <see cref="PerformerResponse"/> (для API).
        /// </summary>
        /// <value>
        /// Маппер для преобразования <see cref="PerformerModel"/> в <see cref="PerformerResponse"/>.
        /// </value>
        public IMapper<PerformerModel, PerformerResponse> ToPerformerResponse { get; }
    }
}