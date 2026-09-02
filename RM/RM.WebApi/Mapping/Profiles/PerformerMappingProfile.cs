using AutoMapper;
using RM.Api.DTOs.Responses;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="PerformerModel"/> (из BLL)</item>
///   <item><see cref="PerformerResponse"/> (из Api)</item>
/// </list>
/// </summary>
public class PerformerMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="PerformerMappingProfile"/>.
    /// </summary>
    public PerformerMappingProfile()
    {
        CreateMap<PerformerModel, PerformerResponse>();
    }
}
