using AutoMapper;
using RM.BLL.Abstractions.Models;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="WorkTypeShortEntity"/> (из DAL)</item>
///   <item><see cref="WorkTypeCreationModel"/> (из BLL)</item>
/// </list>
/// </summary>
public class WorkTypeCreationMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeCreationMappingProfile"/>.
    /// </summary>
    public WorkTypeCreationMappingProfile()
    {
        CreateMap<WorkTypeCreationModel, WorkTypeShortEntity>()
            .ForMember(dll=> dll.Id, expr => expr.Ignore())
            .ForMember(dll => dll.Name , expr => expr.MapFrom(bll => bll.Name))
            .ForMember(dll => dll.WorkUnitId, expr => expr.MapFrom(bll => bll.WorkUnitId));
    }
}
