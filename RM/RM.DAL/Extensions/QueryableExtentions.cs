using RM.DAL.Abstractions.Models.Pagination;
using System;
using System.Linq;

namespace RM.DAL.Extensions
{
    /// <summary>
    /// Расширения интерфейса <see cref="IQueryable{T}"/>
    /// </summary>
    internal static class QueryableExtentions
    {
        #region Методы

        /// <summary>
        /// Предоставляет страницу c элементами
        /// </summary>
        /// <typeparam name="T">Тип данных элемента</typeparam>
        /// <param name="query">Запрос</param>
        ///<param name="pageOptions">Настройки страницы</param>
        /// <returns>Страница c элементами</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IQueryable<T> Page<T>(this IQueryable<T> query, PageOptionsModel pageOptions)
        {
            if (pageOptions == null || pageOptions.PageNumber < 1 || pageOptions.PageSize < 1)
                return query;

            var offset = (pageOptions.PageNumber - 1) * pageOptions.PageSize;

            return query.Skip(offset)
                        .Take(pageOptions.PageSize);
        }

        #endregion
    }
}
