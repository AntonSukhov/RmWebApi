using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RM.Api.DTOs.Requests;
using RM.Api.DTOs.Responses;

namespace RM.Api.Services;

/// <summary>
/// Сервис видов работ для работы с внешним API.
/// </summary>
public interface IWorkTypeApiService
{
    /// <summary>
    /// Предоставляет все виды работ с пагинацией.
    /// </summary>
    /// <param name="pageOptions">Настройки пагинации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекция видов работ.</returns>
    Task<IReadOnlyCollection<WorkTypeResponse>> GetAllAsync(
        PageOptionsRequest pageOptions, 
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Предоставляет вид работ по его ИД.
    /// </summary>
    /// <param name="workTypeId">ИД вида работ.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Вид работ или <see langword="null"/>, если не найден.</returns>
    Task<WorkTypeResponse?> GetByIdAsync(Guid workTypeId, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Создаёт вид работ.
    /// </summary>
    /// <param name="workTypeCreationRequest">Запрос на создание вида работ.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>ИД созданного вида работ.</returns>
    Task<Guid> CreateAsync(
        WorkTypeCreationRequest workTypeCreationRequest, 
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Удаляет вид работ.
    /// </summary>
    /// <param name="workTypeId">ИД удаляемого вида работ.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, завершаемая после удаления.</returns>
    Task DeleteAsync(Guid workTypeId, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Обновляет вид работ.
    /// </summary>
    /// <param name="workTypeUpdationRequest">Запрос на обновление вида работ.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, завершаемая после обновления.</returns>
    Task UpdateAsync(
        WorkTypeUpdationRequest workTypeUpdationRequest, 
        CancellationToken? cancellationToken = null);
}
