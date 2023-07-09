namespace RM.DAL.Abstractions.Models.Pagination
{
    /// <summary>
    /// Модель настроек страницы
    /// </summary>
    public class PageOptionsModel
    {
        #region Свойства

        /// <summary>
        /// Порядковый номер страницы
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Кол-во элементов страницы
        /// </summary>
        public int PageSize { get; set; } = 100;

        #endregion
    }
}
