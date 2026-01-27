using AutoMapper;

namespace RM.DAL.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="Abstractions.Models.WorkTypeShortModel"/></item>
///   <item><see cref="Abstractions.Models.WorkTypeModel"/></item>
/// </list>
/// </summary>
public class WorkTypeShortMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeShortMappingProfile"/>.
    /// </summary>
    public WorkTypeShortMappingProfile()
    {
        CreateMap<Abstractions.Models.WorkTypeShortModel, Abstractions.Models.WorkTypeModel>()
           .ForMember(dest => dest.Id, expr => expr.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, expr => expr.MapFrom(src => src.Name))
           .ForMember(dest => dest.WorkUnitId, expr => expr.MapFrom(src => src.WorkUnitId))
           .ForMember(dest => dest.WorkUnit, expr => expr.Ignore());
    }
}
