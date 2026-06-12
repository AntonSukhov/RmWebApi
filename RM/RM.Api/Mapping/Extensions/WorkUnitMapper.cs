using System;
using RM.Api.DTOs.Responses;

namespace RM.Api.Mapping.Extensions;

/// <summary>
/// Маппер для преобразования сущностей единиц работ.
/// </summary>
internal static class WorkUnitMapper
{
    /// <summary>
    /// Преобразует <see cref="GeneratedApiClients.WorkUnitResponse"/> в <see cref="WorkUnitResponse"/>.
    /// </summary>
    /// <param name="workUnitResponse">Ответ единицы работ из внешнего API.</param>
    /// <returns>Преобразованный объект <see cref="WorkUnitResponse"/>.</returns>
    /// <exception cref="ArgumentNullException">Вызывается, если <paramref name="workUnitResponse"/> равен <see langword="null"/>.</exception>
    public static WorkUnitResponse ToWorkUnitResponse(
        this GeneratedApiClients.WorkUnitResponse workUnitResponse)
    {
        ArgumentNullException.ThrowIfNull(workUnitResponse, nameof(workUnitResponse));

        return new WorkUnitResponse
        {
            Id = (short)workUnitResponse.Id,
            Name = workUnitResponse.Name ?? string.Empty
        };
    }
}
