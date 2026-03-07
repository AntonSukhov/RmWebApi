using AutoMapper;
using RM.DAL.Abstractions.Entities;

namespace RM.DAL.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="WorkTypeShortEntity"/></item>
///   <item><see cref="WorkTypeEntity"/></item>
/// </list>
/// </summary>
public class WorkTypeShortMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeShortMappingProfile"/>.
    /// </summary>
    public WorkTypeShortMappingProfile()
    {
        CreateMap<WorkTypeShortEntity, WorkTypeEntity>()
           .ForMember(dest => dest.Id, expr => expr.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, expr => expr.MapFrom(src => src.Name))
           .ForMember(dest => dest.WorkUnitId, expr => expr.MapFrom(src => src.WorkUnitId))
           .ForMember(dest => dest.WorkUnit, expr => expr.Ignore());
    }
}
