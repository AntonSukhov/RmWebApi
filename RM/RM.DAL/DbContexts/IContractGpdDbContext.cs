using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;

namespace RM.DAL.DbContexts
{
    /// <summary>
    /// Контекст работы с базой данных договоров ГПД
    /// </summary>
    public interface IContractGpdDbContext : IBaseDbContext
    {
        #region Свойства

        /// <summary>
        /// Единицы работ
        /// </summary>
        DbSet<WorkUnitModel> WorkUnits { get; set; }

        /// <summary>
        /// Виды работ
        /// </summary>
        DbSet<WorkTypeModel> WorkTypes { get; set; }

        #endregion
    }
}
