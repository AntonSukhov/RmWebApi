using AutoMapper;

namespace RM.BLL.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="DAL.Abstractions.Models.WorkTypeShortModel"/> (из DAL)</item>
///   <item><see cref="Abstractions.Models.WorkTypeCreationModel"/> (из BLL)</item>
/// </list>
/// </summary>
public class WorkTypeCreationMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeCreationMappingProfile"/>.
    /// </summary>
    public WorkTypeCreationMappingProfile()
    {
        CreateMap<Abstractions.Models.WorkTypeCreationModel, DAL.Abstractions.Models.WorkTypeShortModel>()
            .ForMember(dll=> dll.Id, expr => expr.Ignore())
            .ForMember(dll => dll.Name , expr => expr.MapFrom(bll => bll.Name))
            .ForMember(dll => dll.WorkUnitId, expr => expr.MapFrom(bll => bll.WorkUnitId));
    }
}
