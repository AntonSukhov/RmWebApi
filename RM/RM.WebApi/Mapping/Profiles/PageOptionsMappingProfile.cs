using AutoMapper;
using RM.Api.DTOs.Requests;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="PageOptionsRequest"/> (из Api)</item>
///   <item><see cref="PageOptionsModel"/> (из BLL)</item>
/// </list>
/// </summary>
public class PageOptionsMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="PageOptionsMappingProfile"/>.
    /// </summary>
    public PageOptionsMappingProfile()
    {
        CreateMap<PageOptionsRequest, PageOptionsModel>();
    }
}
