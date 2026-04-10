using Infrastructure.Mapping.Interfaces;
using RM.BLL.Abstractions.Models;
using RM.BLL.Services;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Mapping.MapperSets;

/// <summary>
/// Контейнер мапперов для работы с видами работ на уровне BLL.
/// </summary>
/// <remarks>
/// Объединяет все преобразования, необходимые сервису <see cref="WorkTypeService"/>.
/// </remarks>
public interface IWorkTypeBllMappers
{
    /// <summary>
    /// Получает маппер для преобразования <see cref="WorkTypeCreationModel"/> 
    /// в <see cref="WorkTypeShortEntity"/>.
    /// </summary>
    public IMapper<WorkTypeCreationModel, WorkTypeShortEntity> ToWorkTypeShortEntity { get; }

    /// <summary>
    /// Получает маппер для преобразования <see cref="WorkTypeUpdationModel"/> 
    /// в <see cref="WorkTypeShortEntity"/>.
    /// </summary>
    public IMapper<WorkTypeUpdationModel, WorkTypeShortEntity> ToWorkTypeShortEntityForUpdate { get; }

    /// <summary>
    /// Получает маппер для преобразования <see cref="WorkTypeEntity"/> 
    /// в <see cref="WorkTypeModel"/>.
    /// </summary>
    public IMapper<WorkTypeEntity, WorkTypeModel> ToWorkTypeModel { get; }

    /// <summary>
    /// Получает маппер для преобразования <see cref="WorkTypeCreationModel"/> 
    /// в <see cref="Infrastructure.Shared.Models.PageOptionsModel"/>.
    /// </summary>
    public IMapper<PageOptionsModel, Infrastructure.Shared.Models.PageOptionsModel> ToPageOptionsModel{ get; }
}
