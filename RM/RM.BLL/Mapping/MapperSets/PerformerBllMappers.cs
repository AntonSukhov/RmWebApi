using System;
using Infrastructure.Mapping.Interfaces;
using RM.BLL.Abstractions.Models;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Mapping.MapperSets
{
    /// <summary>
    /// Реализация контейнера мапперов для работы с исполнителями договоров на уровне BLL.
    /// </summary>
    /// <remarks>
    /// Объединяет все преобразования, необходимые сервису <see cref="PerformerService"/>.
    /// </remarks>
    public class PerformerBllMappers : IPerformerBllMappers
    {
        /// <inheritdoc/>
        public IMapper<PerformerEntity, PerformerModel> ToPerformerModel { get; }

        /// <summary>
        /// Инициализирует экземпляр <see cref="PerformerBllMappers"/>.
        /// </summary>
        /// <param name="entityToModelMapper">Маппер для преобразования <see cref="PerformerEntity"/> в <see cref="PerformerModel"/>.</param>
        public PerformerBllMappers(IMapper<PerformerEntity, PerformerModel> entityToModelMapper)
        {
            ArgumentNullException.ThrowIfNull(entityToModelMapper, nameof(entityToModelMapper));

            ToPerformerModel = entityToModelMapper;
        }
    }
}