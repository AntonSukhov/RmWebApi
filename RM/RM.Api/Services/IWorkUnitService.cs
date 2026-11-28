using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RM.Api.DTOs.Responses;

namespace RM.Api.Services;

/// <summary>
/// Сервис единицы работ.
/// </summary>
public interface IWorkUnitService
{
    /// <summary>
    /// Предоставляет все единицы работ.
    /// </summary>
    /// <returns>Единицы работ.</returns>
    Task<IReadOnlyCollection<WorkUnitResponse>> GetAllAsync(CancellationToken? cancellationToken = null);

    /// <summary>
    /// Предоставляет единицу работ по её ИД.
    /// </summary>
    /// <param name="workUnitId">ИД единицы работ.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Единица работ или <see langword="null"/>, если не найдена.</returns>
    Task<WorkUnitResponse?> GetByIdAsync(short workUnitId, CancellationToken? cancellationToken = null);
}
