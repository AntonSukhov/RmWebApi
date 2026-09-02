using Infrastructure.Mapping.Interfaces;
using RM.BLL.Abstractions.Models;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Mapping.MapperSets
{
    /// <summary>
    /// Контейнер мапперов для работы с исполнителями договоров на уровне BLL.
    /// </summary>
    /// <remarks>
    /// Объединяет все преобразования, необходимые сервису <see cref="PerformerService"/>.
    /// </remarks>
    public interface IPerformerBllMappers
    {
        /// <summary>
        /// Получает маппер для преобразования <see cref="PerformerEntity"/> в <see cref="PerformerModel"/>.
        /// </summary>
        /// <value>
        /// Маппер для преобразования <see cref="PerformerEntity"/> в <see cref="PerformerModel"/>.
        /// </value>
        public IMapper<PerformerEntity, PerformerModel> ToPerformerModel { get; }
    }
}