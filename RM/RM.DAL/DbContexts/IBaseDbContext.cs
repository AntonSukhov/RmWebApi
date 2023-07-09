using System.Threading.Tasks;

namespace RM.DAL.DbContexts
{
    /// <summary>
    /// Базовый контекст работы с базой данных
    /// </summary>
    public interface IBaseDbContext
    {
        #region Методы

        /// <summary>
        /// Сохранение всех изменений в базу данных
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Асинхронное сохранение всех изменений в базу данных
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        #endregion
    }
}
