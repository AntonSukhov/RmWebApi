using Infrastructure.Mapping.Interfaces;
using RM.Api.DTOs.Requests;
using RM.Api.DTOs.Responses;
using RM.BLL.Abstractions.Models;
using RM.WebApi.Controllers;

namespace RM.WebApi.Mapping.MapperSets;

/// <summary>
/// Контейнер мапперов для работы с видами работ на уровне API.
/// </summary>
/// <remarks>
/// Объединяет все преобразования, необходимые контроллеру <see cref="WorkTypeApiController"/>.
/// </remarks>
public interface IWorkTypeApiMappers
{
   /// <summary>
   /// Получает маппер для преобразования <see cref="PageOptionsRequest"/> в <see cref="PageOptionsModel"/>.
   /// </summary>
   public IMapper<PageOptionsRequest, PageOptionsModel> ToPageOptionsModel { get; }

   /// <summary>
   /// Получает маппер для преобразования <see cref="WorkTypeModel"/> (из BLL) 
   /// в <see cref="WorkTypeResponse"/> (для API).
   /// </summary>
   public IMapper<WorkTypeModel, WorkTypeResponse> ToWorkTypeResponse { get; }

   /// <summary>
   /// Получает маппер для преобразования <see cref="WorkTypeCreationRequest"/> (из API) 
   /// в <see cref="WorkTypeCreationModel"/> (для BLL).
   /// </summary>
   public IMapper<WorkTypeCreationRequest, WorkTypeCreationModel> ToWorkTypeCreationModel { get; }

   /// <summary>
   /// Получает маппер для преобразования <see cref="WorkTypeUpdationRequest"/> (из API) 
   /// в <see cref="WorkTypeUpdationModel"/> (для BLL).
   /// </summary>
   public IMapper<WorkTypeUpdationRequest, WorkTypeUpdationModel> ToWorkTypeUpdationModel { get; }
}

