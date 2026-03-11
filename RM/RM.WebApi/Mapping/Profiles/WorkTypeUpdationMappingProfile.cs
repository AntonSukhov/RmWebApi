using AutoMapper;
using RM.Api.DTOs.Requests;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="WorkTypeUpdationRequest"/> (из Api)</item>
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
        CreateMap<WorkTypeUpdationRequest, WorkTypeUpdationModel>();
    }
}
