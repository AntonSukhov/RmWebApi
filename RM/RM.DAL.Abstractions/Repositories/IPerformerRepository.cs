using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Shared.Models;
using RM.DAL.Abstractions.Entities;

namespace RM.DAL.Abstractions.Repositories;

/// <summary>
/// Репозиторий исполнителей договоров.
/// </summary>
public interface IPerformerRepository: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Предоставляет всех исполнителей договоров.
    /// </summary>
    /// <param name="pageOptions">Настройки страницы.</param>
    /// <returns>Исполнители договоров.</returns>
    Task<IReadOnlyCollection<PerformerEntity>> GetAllAsync(
        PageOptionsModel? pageOptions = null);

    /// <summary>
    /// Предоставляет исполнителя договоров по ИД.
    /// </summary>
    /// <param name="performerId">ИД исполнителя договоров.</param>
    /// <returns>Исполнитель договоров.</returns>
    Task<PerformerEntity?> GetByIdAsync(Guid performerId);
}