using System;
using Infrastructure.Mapping.Interfaces;
using RM.Api.DTOs.Requests;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.MapperSets
{
    /// <summary>
    /// Реализация контейнера мапперов для работы с страницей свойств на уровне API.
    /// </summary>
    public class PageOptionsApiMappers : IPageOptionsApiMappers
    {
        /// <inheritdoc/>
        public IMapper<PageOptionsRequest, PageOptionsModel> ToPageOptionsModel { get; }

        /// <summary>
        /// Инициализация экземпляра <see cref="PageOptionsApiMappers"/>.
        /// </summary>
        /// <param name="pageOptionsMapper">Маппер для преобразования <see cref="PageOptionsRequest"/> в <see cref="PageOptionsModel"/>.</param>
        public PageOptionsApiMappers(IMapper<PageOptionsRequest, PageOptionsModel> pageOptionsMapper)
        {
            ArgumentNullException.ThrowIfNull(pageOptionsMapper, nameof(pageOptionsMapper));

            ToPageOptionsModel = pageOptionsMapper;
        }
    }
}