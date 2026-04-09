using System;
using Infrastructure.Mapping.Interfaces;
using RM.Api.DTOs.Requests;
using RM.Api.DTOs.Responses;
using RM.BLL.Abstractions.Models;

namespace RM.WebApi.Mapping.MapperSets;

/// <summary>
/// Реализация контейнера мапперов для работы с видами работ на уровне API.
/// </summary>
public class WorkTypeApiMappers: IWorkTypeApiMappers
{
   /// <inheritdoc/>
   public IMapper<PageOptionsRequest, PageOptionsModel> ToPageOptionsModel { get; }

   /// <inheritdoc/>
   public IMapper<WorkTypeModel, WorkTypeResponse> ToWorkTypeResponse { get; }

   /// <inheritdoc/>
   public IMapper<WorkTypeCreationRequest, WorkTypeCreationModel> ToWorkTypeCreationModel { get; }

   /// <inheritdoc/>
   public IMapper<WorkTypeUpdationRequest, WorkTypeUpdationModel> ToWorkTypeUpdationModel { get; }

   /// <summary>
   /// Инициализация экземпляра <see cref="WorkTypeApiMappers"/>.
   /// </summary>
   public WorkTypeApiMappers(
        IMapper<PageOptionsRequest, PageOptionsModel> pageOptionsMapper,
        IMapper<WorkTypeModel, WorkTypeResponse> responseMapper,
        IMapper<WorkTypeCreationRequest, WorkTypeCreationModel> creationMapper,
        IMapper<WorkTypeUpdationRequest, WorkTypeUpdationModel> updationMapper
   )
    {
        ArgumentNullException.ThrowIfNull(pageOptionsMapper, nameof(pageOptionsMapper));
        ArgumentNullException.ThrowIfNull(responseMapper, nameof(responseMapper));
        ArgumentNullException.ThrowIfNull(creationMapper, nameof(creationMapper));
        ArgumentNullException.ThrowIfNull(updationMapper, nameof(updationMapper));

        ToPageOptionsModel = pageOptionsMapper;
        ToWorkTypeResponse = responseMapper;
        ToWorkTypeCreationModel = creationMapper;
        ToWorkTypeUpdationModel = updationMapper;
    }
}
