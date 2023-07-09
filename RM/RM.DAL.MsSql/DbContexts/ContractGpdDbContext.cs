using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RM.DAL.DbContexts;
using RM.DAL.Entities;
using System.Diagnostics;
using System.Threading.Tasks;

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
        /// Конструктор 
        /// </summary>
        /// <param name="options">Опции контекста работы с базой данных договоров ГПД</param>
        public ContractGpdDbContext(DbContextOptions<ContractGpdDbContext> options) : base(options) { }

        #endregion

        #region Методы

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //TODO: для отладки, потом заменить на нормальное протоколирование
            optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information)
                          .EnableSensitiveDataLogging();
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        /// <inheritdoc/>
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        #endregion
    }
}
