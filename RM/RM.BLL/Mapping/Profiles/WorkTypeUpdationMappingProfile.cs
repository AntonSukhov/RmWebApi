using AutoMapper;
using RM.BLL.Abstractions.Models;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="WorkTypeShortEntity"/> (из DAL)</item>
///   <item><see cref="WorkTypeUpdationModel"/> (из BLL)</item>
/// </list>
/// </summary>
public class WorkTypeUpdationMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeUpdationMappingProfile"/>.
    /// </summary>
    public WorkTypeUpdationMappingProfile()
    {
        CreateMap<WorkTypeUpdationModel, WorkTypeShortEntity>();
    }
}
