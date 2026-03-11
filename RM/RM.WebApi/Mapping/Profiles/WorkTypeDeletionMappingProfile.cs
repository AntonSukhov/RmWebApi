using AutoMapper;
using RM.Api.DTOs.Requests;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="WorkTypeDeletionRequest"/> (из Api)</item>
///   <item><see cref="WorkTypeDeletionModel"/> (из BLL)</item>
/// </list>
/// </summary>
public class WorkTypeDeletionMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="WorkTypeDeletionMappingProfile"/>.
    /// </summary>
    public WorkTypeDeletionMappingProfile()
    {
        CreateMap<WorkTypeDeletionRequest, WorkTypeDeletionModel>();
    }
}
