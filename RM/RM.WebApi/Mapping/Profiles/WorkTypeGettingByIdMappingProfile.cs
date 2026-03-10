using AutoMapper;
using RM.Api.DTOs.Requests;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="WorkTypeGettingByIdRequest"/> (из Api)</item>
///   <item><see cref="WorkTypeGettingByIdModel"/> (из BLL)</item>
/// </list>
/// </summary>
public class WorkTypeGettingByIdMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeGettingByIdMappingProfile"/>.
    /// </summary>
    public WorkTypeGettingByIdMappingProfile()
    {
        CreateMap<WorkTypeGettingByIdRequest, WorkTypeGettingByIdModel>();
    }
}
