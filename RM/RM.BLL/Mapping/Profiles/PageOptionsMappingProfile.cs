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
           .ForMember(bll => bll.PageNumber, expr => expr.MapFrom(dll => dll.PageNumber))
           .ForMember(bll => bll.PageSize, expr => expr.MapFrom(dll => dll.PageSize))
           .ReverseMap(); // Включаем обратный маппинг     
    }
}
