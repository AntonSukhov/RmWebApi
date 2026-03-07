using AutoMapper;
using RM.BLL.Abstractions.Models;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="WorkUnitEntity"/> (из DAL)</item>
///   <item><see cref="WorkUnitModel"/> (из BLL)</item>
/// </list>
/// </summary>
public class WorkUnitMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkUnitMappingProfile"/>.
    /// </summary>
    public WorkUnitMappingProfile()
    {
        CreateMap<WorkUnitEntity, WorkUnitModel>()
           .ForMember(bll => bll.Id, expr => expr.MapFrom(dal => dal.Id))
           .ForMember(bll => bll.Name, expr => expr.MapFrom(dal => dal.Name));
    }
}
