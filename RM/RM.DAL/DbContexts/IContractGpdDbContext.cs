using Microsoft.EntityFrameworkCore;
using RM.DAL.Entities;

namespace RM.DAL.DbContexts
{
    /// <summary>
    /// Контекст работы с базой данных договоров ГПД
    /// </summary>
    public interface IContractGpdDbContext
    {
        #region Свойства

        /// <summary>
        /// Единицы работ
        /// </summary>
        DbSet<WorkUnitEntity> WorkUnits { get; set; }

        /// <summary>
        /// Виды работ
        /// </summary>
        DbSet<WorkTypeEntity> WorkTypes { get; set; }

        #endregion
    }
}
