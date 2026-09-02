using Infrastructure.Mapping.Interfaces;
using RM.BLL.Abstractions.Models;

namespace RM.BLL.Mapping.MapperSets
{
    /// <summary>
    /// Контейнер мапперов для работы с настройками страницы на уровне BLL.
    /// </summary>
    public interface IPageOptionsBllMappers
    {
        /// <summary>
        /// Получает маппер для преобразования <see cref="PageOptionsModel"/> 
        /// в <see cref="Infrastructure.Shared.Models.PageOptionsModel"/>.
        /// </summary>
        /// <value>
        /// Маппер для преобразования <see cref="PageOptionsModel"/> в 
        /// <see cref="Infrastructure.Shared.Models.PageOptionsModel"/>.
        /// </value>
        public IMapper<PageOptionsModel, Infrastructure.Shared.Models.PageOptionsModel> ToPageOptionsModel{ get; }
    }
}