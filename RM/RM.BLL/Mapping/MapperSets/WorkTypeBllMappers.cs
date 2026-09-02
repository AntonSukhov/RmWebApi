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

    /// <summary>
    /// Инициализация экземпляра <see cref="WorkTypeBllMappers"/>.
    /// </summary>
    /// <param name="creationMapper"></param>
    /// <param name="updateMapper"></param>
    /// <param name="entityToModelMapper"></param>
    public WorkTypeBllMappers(
        IMapper<WorkTypeCreationModel, WorkTypeShortEntity> creationMapper,
        IMapper<WorkTypeUpdationModel, WorkTypeShortEntity> updateMapper,
        IMapper<WorkTypeEntity, WorkTypeModel> entityToModelMapper)
    {
        ArgumentNullException.ThrowIfNull(creationMapper, nameof(creationMapper));
        ArgumentNullException.ThrowIfNull(updateMapper, nameof(updateMapper));
        ArgumentNullException.ThrowIfNull(entityToModelMapper, nameof(entityToModelMapper));

        ToWorkTypeShortEntity = creationMapper;
        ToWorkTypeShortEntityForUpdate = updateMapper;
        ToWorkTypeModel = entityToModelMapper;
    }
}
