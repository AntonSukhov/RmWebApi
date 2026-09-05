using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RM.Api.DTOs.Requests;
using RM.Api.DTOs.Responses;

namespace RM.Api.Services
{
    /// <summary>
    /// Сервис исполнителей договоров для работы с внешним API.
    /// </summary>
    public interface IPerformerService
    {
        /// <summary>
        /// Предоставляет всех исполнителей договоров с пагинацией.
        /// </summary>
        /// <param name="pageOptions">Настройки пагинации.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Коллекция исполнителей договоров.</returns>
        Task<IReadOnlyCollection<PerformerResponse>> GetAllAsync(
            PageOptionsRequest pageOptions, 
            CancellationToken? cancellationToken = null);

        /// <summary>
        /// Предоставляет исполнителя договоров по его ИД.
        /// </summary>
        /// <param name="performerId">ИД исполнителя договоров.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Исполнитель договоров или <see langword="null"/>, если не найден.</returns>
        Task<PerformerResponse?> GetByIdAsync(
            Guid performerId, 
            CancellationToken? cancellationToken = null);

        /// <summary>
        /// Удаляет исполнителя договоров.
        /// </summary>
        /// <param name="performerId">ИД удаляемого исполнителя договоров.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Задача, завершаемая после удаления.</returns>
        Task DeleteAsync(
            Guid performerId, 
            CancellationToken? cancellationToken = null);
    }
}