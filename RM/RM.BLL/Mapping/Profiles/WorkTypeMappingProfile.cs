using AutoMapper;

namespace RM.BLL.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="DAL.Abstractions.Models.WorkTypeModel"/> (из DAL)</item>
///   <item><see cref="Abstractions.Models.WorkTypeModel"/> (из BLL)</item>
/// </list>
/// </summary>
public class WorkTypeMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeMappingProfile"/>.
    /// </summary>
    public WorkTypeMappingProfile()
    {
        CreateMap<DAL.Abstractions.Models.WorkTypeModel, Abstractions.Models.WorkTypeModel>()
           .ForMember(bll => bll.Id, expr => expr.MapFrom(dal => dal.Id))
           .ForMember(bll => bll.Name, expr => expr.MapFrom(dal => dal.Name))
           .ForMember(bll => bll.WorkUnit, expr => expr.MapFrom(dal => dal.WorkUnit));
    }
}
