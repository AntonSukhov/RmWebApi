using RM.DAL.Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RM.DAL.Abstractions.Repositories
{
    /// <summary>
    /// Репозиторий видов работ
    /// </summary>
    public interface IWorkTypeRepository
    {
        #region Методы

        /// <summary>
        /// Предоставляет виды работ
        /// </summary>
        /// <returns>Виды работ</returns>
        Task<IEnumerable<WorkTypeModel>> GetWorkTypes();

        #endregion
    }
}
