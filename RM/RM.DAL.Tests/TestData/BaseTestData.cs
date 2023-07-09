using RM.DAL.Abstractions.Models.Pagination;

namespace RM.DAL.Tests.TestData
{
    /// <summary>
    /// Базовые тестовые данные
    /// </summary>
    public class BaseTestData
    {
        #region Свойства

        /// <summary>
        /// Предоставляет коллекцию настроек страницы
        /// </summary>
        /// <returns>Коллекция настроек страницы</returns>
        public static IEnumerable<object[]> GetPageOptions
        {
            get
            {
                yield return new[] { new PageOptionsModel() };
            }
        }

        #endregion
    }
}
