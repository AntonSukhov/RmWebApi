using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Models.Pagination;
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
        /// Предоставляет все виды работ
        /// </summary>
        /// <param name="pageOptions">Настройки страницы</param>
        /// <returns>Виды работ</returns>
        Task<IEnumerable<WorkTypeModel>> GetAll(PageOptionsModel pageOptions = null);

        /// <summary>
        /// Создаёт вид работ
        /// </summary>
        ///<param name="workTypeName">Название вида работ</param>
        ///<param name="workUnitId">ИД единицы работ</param>
        /// <returns>Вид работ</returns>
        Task<WorkTypeModel> Create(string workTypeName, byte? workUnitId);

        #endregion
    }
}
