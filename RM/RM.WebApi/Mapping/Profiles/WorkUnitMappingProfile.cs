using AutoMapper;
using RM.Api.DTOs.Responses;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="WorkUnitModel"/> (из BLL)</item>
///   <item><see cref="WorkUnitResponse"/> (из Api)</item>
/// </list>
/// </summary>
public class WorkUnitMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkUnitMappingProfile"/>.
    /// </summary>
    public WorkUnitMappingProfile()
    {
        CreateMap<WorkUnitModel, WorkUnitResponse>();
    }
}
