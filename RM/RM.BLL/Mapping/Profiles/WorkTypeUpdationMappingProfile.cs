using AutoMapper;

namespace RM.BLL.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="DAL.Abstractions.Models.WorkTypeShortModel"/> (из DAL)</item>
///   <item><see cref="Abstractions.Models.WorkTypeUpdationModel"/> (из BLL)</item>
/// </list>
/// </summary>
public class WorkTypeUpdationMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeUpdationMappingProfile"/>.
    /// </summary>
    public WorkTypeUpdationMappingProfile()
    {
        CreateMap<Abstractions.Models.WorkTypeUpdationModel, DAL.Abstractions.Models.WorkTypeShortModel>()
            .ForMember(dll => dll.Id, expr => expr.MapFrom(bll => bll.Id))
            .ForMember(dll => dll.Name , expr => expr.MapFrom(bll => bll.Name))
            .ForMember(dll => dll.WorkUnitId, expr => expr.MapFrom(bll => bll.WorkUnitId));
    }
}
