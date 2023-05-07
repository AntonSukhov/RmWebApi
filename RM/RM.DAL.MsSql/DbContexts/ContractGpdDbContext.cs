using Microsoft.EntityFrameworkCore;
using RM.DAL.DbContexts;
using RM.DAL.Entities;

namespace RM.DAL.MsSql.DbContexts
{
    /// <summary>
    /// Контекст работы с базой данных договоров ГПД
    /// </summary>
    public class ContractGpdDbContext : DbContext, IContractGpdDbContext
    {
        #region Свойства

        /// <inheritdoc/>
        public DbSet<WorkUnitEntity> WorkUnits { get; set; }

        /// <inheritdoc/>
        public DbSet<WorkTypeEntity> WorkTypes { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию 
        /// </summary>
        /// <param name="options">Опции контекста работы с базой данных договоров ГПД</param>
        public ContractGpdDbContext(DbContextOptions<ContractGpdDbContext> options) : base(options) { }

        #endregion

        #region Методы

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        #endregion
    }
}
