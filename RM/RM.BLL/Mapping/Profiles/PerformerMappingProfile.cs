using AutoMapper;
using RM.BLL.Abstractions.Models;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Mapping.Profiles
{
    /// <summary>
    /// Профиль AutoMapper для настроек преобразований между:
    /// <list type="bullet">
    ///   <item><see cref="PerformerEntity"/> (из DAL)</item>
    ///   <item><see cref="PerformerModel"/> (из BLL)</item>
    /// </list>
    /// </summary>
    public class PerformerMappingProfile: Profile
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="PerformerMappingProfile"/>.
        /// </summary>
        public PerformerMappingProfile()
        {
            CreateMap<PerformerEntity, PerformerModel>();
        }
    }
}