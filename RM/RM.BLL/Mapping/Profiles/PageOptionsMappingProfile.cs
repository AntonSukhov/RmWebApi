using AutoMapper;
using Infrastructure.Shared.Models;

namespace RM.BLL.Mapping.Profiles;

/// <summary>
/// Профиль AutoMapper для настроек преобразований между:
/// <list type="bullet">
///   <item><see cref="PageOptionsModel"/> (из DAL)</item>
///   <item><see cref="Abstractions.Models.PageOptionsModel"/> (из BLL)</item>
/// </list>
/// </summary>
public class PageOptionsMappingProfile: Profile
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="PageOptionsMappingProfile"/>.
    /// </summary>
    public PageOptionsMappingProfile()
    {
        CreateMap<PageOptionsModel, Abstractions.Models.PageOptionsModel>()
           .ReverseMap(); // Включаем обратный маппинг     
    }
}
