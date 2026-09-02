using Infrastructure.Mapping.Interfaces;
using RM.Api.DTOs.Requests;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.MapperSets
{
    /// <summary>
    /// Контейнер мапперов для работы с страницей свойств на уровне API.
    /// </summary>
    public interface IPageOptionsApiMappers
    {
        /// <summary>
        /// Получает маппер для преобразования <see cref="PageOptionsRequest"/> в 
        /// <see cref="PageOptionsModel"/>.
        /// </summary>
        /// <value>
        /// Маппер для преобразования <see cref="PageOptionsRequest"/> в <see cref="PageOptionsModel"/>.
        /// </value>
        public IMapper<PageOptionsRequest, PageOptionsModel> ToPageOptionsModel { get; }
    }
}