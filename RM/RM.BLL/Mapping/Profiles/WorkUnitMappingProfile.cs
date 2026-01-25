using AutoMapper;

namespace RM.BLL.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="DAL.Abstractions.Models.WorkUnitModel"/> (из DAL)</item>
///   <item><see cref="Abstractions.Models.WorkUnitModel"/> (из BLL)</item>
/// </list>
/// </summary>
public class WorkUnitMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkUnitMappingProfile"/>.
    /// </summary>
    public WorkUnitMappingProfile()
    {
        CreateMap<DAL.Abstractions.Models.WorkUnitModel, Abstractions.Models.WorkUnitModel>()
           .ForMember(bll => bll.Id, expr => expr.MapFrom(dal => dal.Id))
           .ForMember(bll => bll.Name, expr => expr.MapFrom(dal => dal.Name));
    }
}
