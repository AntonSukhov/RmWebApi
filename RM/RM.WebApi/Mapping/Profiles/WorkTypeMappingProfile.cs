using AutoMapper;
using RM.Api.DTOs.Responses;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="WorkTypeModel"/> (из BLL)</item>
///   <item><see cref="WorkTypeResponse"/> (из Api)</item>
/// </list>
/// </summary>
public class WorkTypeMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeMappingProfile"/>.
    /// </summary>
    public WorkTypeMappingProfile()
    {
        CreateMap<WorkTypeModel, WorkTypeResponse>();
    }
}
