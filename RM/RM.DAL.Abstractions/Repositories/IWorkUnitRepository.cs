using RM.DAL.Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RM.DAL.Abstractions.Repositories
{
    /// <summary>
    /// Репозиторий единицы работ
    /// </summary>
    public interface IWorkUnitRepository
    {
        #region Методы

        /// <summary>
        /// Предоставляет все единицы работ
        /// </summary>
        /// <returns>Единицы работ</returns>
        Task<IEnumerable<WorkUnitModel>> GetAll();

        #endregion
    }
}
