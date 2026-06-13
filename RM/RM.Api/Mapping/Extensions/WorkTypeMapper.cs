using System;
using RM.Api.DTOs.Responses;
using RM.Api.DTOs.Requests;

namespace RM.Api.Mapping.Extensions;

/// <summary>
/// Маппер для преобразования сущностей видов работ.
/// </summary>
internal static class WorkTypeMapper
{
    /// <summary>
    /// Преобразует <see cref="GeneratedApiClients.WorkTypeResponse"/> в <see cref="WorkTypeResponse"/>.
    /// </summary>
    /// <param name="workTypeResponse">Ответ вида работ из внешнего API.</param>
    /// <returns>Преобразованный объект <see cref="WorkTypeResponse"/>.</returns>
    /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="workTypeResponse"/> равен <see langword="null"/>.</exception>
    public static WorkTypeResponse ToWorkTypeResponse(
        this GeneratedApiClients.WorkTypeResponse workTypeResponse)
    {
        ArgumentNullException.ThrowIfNull(workTypeResponse, nameof(workTypeResponse));

        return new WorkTypeResponse
        {
            Id = workTypeResponse.Id,
            Name = workTypeResponse.Name ?? string.Empty,
            WorkUnit = workTypeResponse.WorkUnit?.ToWorkUnitResponse()
        };
    }

    /// <summary>
    /// Преобразует <see cref="WorkTypeCreationRequest"/> в <see cref="GeneratedApiClients.WorkTypeCreationRequest"/>.
    /// </summary>
    /// <param name="request">Запрос на создание вида работ.</param>
    /// <returns>Запрос на создание вида работ для внешнего API.</returns>
    public static GeneratedApiClients.WorkTypeCreationRequest ToWorkTypeCreationRequest(
        this WorkTypeCreationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        return new GeneratedApiClients.WorkTypeCreationRequest
        {
            Name = request.Name ?? string.Empty,
            WorkUnitId = request.WorkUnitId
        };
    }

    /// <summary>
    /// Преобразует <see cref="WorkTypeUpdationRequest"/> в 
    /// <see cref="GeneratedApiClients.WorkTypeUpdationRequest"/>.
    /// </summary>
    /// <param name="request">Запрос на обновление вида работ.</param>
    /// <returns>Запрос на обновление вида работ для внешнего API.</returns>
    public static GeneratedApiClients.WorkTypeUpdationRequest ToWorkTypeUpdationRequest(
        this WorkTypeUpdationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        return new GeneratedApiClients.WorkTypeUpdationRequest
        {
            Id = request.Id,
            Name = request.Name ?? string.Empty,
            WorkUnitId = request.WorkUnitId
        };
    }
}
