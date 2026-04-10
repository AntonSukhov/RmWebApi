using System;
using Infrastructure.Mapping.Interfaces;
using RM.BLL.Abstractions.Models;
using RM.DAL.Abstractions.Entities;

namespace RM.BLL.Mapping.MapperSets;

public class WorkTypeBllMappers: IWorkTypeBllMappers
{
    /// <inheritdoc/>
    public IMapper<WorkTypeCreationModel, WorkTypeShortEntity> ToWorkTypeShortEntity { get; }

    /// <inheritdoc/>
    public IMapper<WorkTypeUpdationModel, WorkTypeShortEntity> ToWorkTypeShortEntityForUpdate { get; }

    /// <inheritdoc/>
    public IMapper<WorkTypeEntity, WorkTypeModel> ToWorkTypeModel { get; }

    /// <inheritdoc/>
    public IMapper<PageOptionsModel, Infrastructure.Shared.Models.PageOptionsModel> ToPageOptionsModel{ get; }

    /// <summary>
    /// Инициализация экземпляра <see cref="WorkTypeBllMappers"/>.
    /// </summary>
    public WorkTypeBllMappers(
        IMapper<WorkTypeCreationModel, WorkTypeShortEntity> creationMapper,
        IMapper<WorkTypeUpdationModel, WorkTypeShortEntity> updateMapper,
        IMapper<WorkTypeEntity, WorkTypeModel> entityToModelMapper,
        IMapper<PageOptionsModel, Infrastructure.Shared.Models.PageOptionsModel> pageOptionsMapper)
    {
        ArgumentNullException.ThrowIfNull(creationMapper, nameof(creationMapper));
        ArgumentNullException.ThrowIfNull(updateMapper, nameof(updateMapper));
        ArgumentNullException.ThrowIfNull(entityToModelMapper, nameof(entityToModelMapper));
        ArgumentNullException.ThrowIfNull(pageOptionsMapper, nameof(pageOptionsMapper));

        ToWorkTypeShortEntity = creationMapper;
        ToWorkTypeShortEntityForUpdate = updateMapper;
        ToWorkTypeModel = entityToModelMapper;
        ToPageOptionsModel = pageOptionsMapper;
    }
}
