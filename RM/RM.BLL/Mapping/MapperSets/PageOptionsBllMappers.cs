using System;
using Infrastructure.Mapping.Interfaces;
using RM.BLL.Abstractions.Models;

namespace RM.BLL.Mapping.MapperSets
{
    /// <summary>
    /// Реализация контейнера мапперов для работы с настройками страницы на уровне BLL.
    /// </summary>
    public class PageOptionsBllMappers : IPageOptionsBllMappers
    {
        /// <inheritdoc/>
        public IMapper<PageOptionsModel, Infrastructure.Shared.Models.PageOptionsModel> 
            ToPageOptionsModel { get; } 

        /// <summary>
        /// Инициализация экземпляра <see cref="PageOptionsBllMappers"/>.
        /// </summary>
        ///<param name="pageOptionsMapper">Маппер для преобразования <see cref="PageOptionsModel"/> в <see cref="Infrastructure.Shared.Models.PageOptionsModel"/>.</param>
        public PageOptionsBllMappers(
             IMapper<PageOptionsModel, Infrastructure.Shared.Models.PageOptionsModel> pageOptionsMapper)
        {
            ArgumentNullException.ThrowIfNull(pageOptionsMapper, nameof(pageOptionsMapper));

            ToPageOptionsModel = pageOptionsMapper;
        }
    }
}