using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RM.BLL.Abstractions.Models;

namespace RM.BLL.Abstractions.Services
{
    /// <summary>
    /// Сервис испольнителей договоров.
    /// </summary>
    public interface IPerformerService
    {
        /// <summary>
        /// Предоставляет всех исполнителей договоров.
        /// </summary>
        /// <param name="pageOptions">Настройки страницы.</param>
        /// <returns>Исполнители договоров.</returns>
        Task<IReadOnlyCollection<PerformerModel>> GetAllAsync(
            PageOptionsModel? pageOptions = null);

        /// <summary>
        /// Предоставляет исполнителя договоров по ИД.
        /// </summary>
        /// <param name="performerId">ИД исполнителя договоров.</param>
        /// <returns>Исполнитель договоров.</returns>
        Task<PerformerModel?> GetByIdAsync(Guid performerId);
    }
}