using AutoMapper;
using RM.Api.DTOs.Requests;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="WorkTypeCreationRequest"/> (из Api)</item>
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
        CreateMap<WorkTypeCreationRequest, WorkTypeCreationModel>();
    }
}
