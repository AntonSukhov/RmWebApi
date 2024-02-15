using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RM.DAL.Extensions
{
    /// <summary>
    /// Расширения интерфейса <see cref="IQueryable{T}"/>.
    /// </summary>
    internal static class QueryableExtentions
    {
        #region Поля

        internal static readonly MethodInfo EnumerableDefaultIfEmpty = typeof(Enumerable).GetMethods()
                                                                                         .First(x => x.Name == "DefaultIfEmpty" && 
                                                                                                     x.GetParameters().Length == 1);
        internal static readonly MethodInfo QueryableSelectMany = typeof(Queryable).GetMethods()
                                                                                   .Where(x => x.Name == "SelectMany" && 
                                                                                               x.GetParameters().Length == 3)
                                                                                   .OrderBy(x => x.ToString().Length)
                                                                                   .First();
        internal static readonly MethodInfo QueryableWhere = typeof(Queryable).GetMethods()
                                                                              .First(x => x.Name == "Where" && 
                                                                                          x.GetParameters().Length == 2);
        internal static readonly MethodInfo QueryableGroupJoin = typeof(Queryable).GetMethods()
                                                                                  .First(x => x.Name == "GroupJoin" && 
                                                                                              x.GetParameters().Length == 5);

        #endregion

        #region Методы

       /// <summary>
        /// Соединяет две коллекции с помощью левого соединения
        /// </summary>
        /// <typeparam name="TOuter"></typeparam>
        /// <typeparam name="TInner"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="outer"></param>
        /// <param name="inner"></param>
        /// <param name="outerKeySelector"></param>
        /// <param name="innerKeySelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
            this IQueryable<TOuter> outer,
            IQueryable<TInner> inner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
            Expression<Func<TOuter, TInner, TResult>> resultSelector)
        {

            ArgumentNullException.ThrowIfNull(outer);
            ArgumentNullException.ThrowIfNull(inner);
            ArgumentNullException.ThrowIfNull(outerKeySelector);
            ArgumentNullException.ThrowIfNull(innerKeySelector);
            ArgumentNullException.ThrowIfNull(resultSelector);

            var keyValuePairHolderWithGroup = typeof(KeyValuePairHolder<,>).MakeGenericType(
                    typeof(TOuter),
                    typeof(IEnumerable<>).MakeGenericType(typeof(TInner))
                );
            var paramOuter = Expression.Parameter(typeof(TOuter));
            var paramInner = Expression.Parameter(typeof(IEnumerable<TInner>));

            var resultSel = Expression
                .Lambda(
                    Expression.MemberInit(
                        Expression.New(keyValuePairHolderWithGroup),
                        Expression.Bind(
                            keyValuePairHolderWithGroup.GetMember("Item1").Single(),
                            paramOuter
                        ),
                        Expression.Bind(
                            keyValuePairHolderWithGroup.GetMember("Item2").Single(),
                            paramInner
                        )
                    ),
                    paramOuter,
                    paramInner
                );

            var groupJoin = QueryableGroupJoin
                .MakeGenericMethod(
                    typeof(TOuter),
                    typeof(TInner),
                    typeof(TKey),
                    keyValuePairHolderWithGroup
                )
                .Invoke(
                    "ThisArgumentIsIgnoredForStaticMethods",
                    [
                        outer,
                        inner,
                        outerKeySelector,
                        innerKeySelector,
                        resultSel
                    ]
                );


            var paramGroup = Expression.Parameter(keyValuePairHolderWithGroup);
            var collectionSelector = Expression.Lambda(
                            Expression.Call(
                                    null,
                                    EnumerableDefaultIfEmpty.MakeGenericMethod(typeof(TInner)),
                                    Expression.MakeMemberAccess(paramGroup, keyValuePairHolderWithGroup.GetProperty("Item2")))
                            ,
                            paramGroup
                        );

            var newResultSelector = new ResultSelectorRewriter<TOuter, TInner, TResult>(resultSelector).CombinedExpression;


            var selectMany1Result = QueryableSelectMany
                .MakeGenericMethod(
                    keyValuePairHolderWithGroup,
                    typeof(TInner),
                    typeof(TResult)
                )
                .Invoke(
                    "ThisArgumentIsIgnoredForStaticMethods",
                    new object[]
                    {
                        groupJoin,
                        collectionSelector,
                        newResultSelector
                    }
                );

            return (IQueryable<TResult>)selectMany1Result;
        }

    
        /// <summary>
        /// Предоставляет запрос получения страницы элементов.
        /// </summary>
        /// <typeparam name="T">Тип данных элементов.</typeparam>
        /// <param name="query">Запрос элементов.</param>
        /// <param name="pageOptions">Настройки страницы.</param>
        /// <returns>Запрос получения страницы элементов.</returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> query, PageOptionsModel pageOptions)
        {
            if (pageOptions == null)
                return query;

            var offset = (pageOptions.PageNumber - 1) * pageOptions.PageSize;

            return query.Skip(offset)
                        .Take(pageOptions.PageSize);
        }

        /// <summary>
        /// Асинхронно предосталяет список элементов.
        /// </summary>
        /// <typeparam name="T">Тип данных элементов.</typeparam>
        /// <param name="query">Запрос элементов.</param>
        /// <param name="pageOptions">Настройки страницы.</param>
        /// <param name="cancellationToken">Объект, который необходимо наблюдать в ожидании завершения задачи.</param>
        /// <returns>Список элементов.</returns>
        public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> query, PageOptionsModel pageOptions, CancellationToken cancellationToken = default)
        {
            return await query.Page(pageOptions)
                              .ToListAsync(cancellationToken);
        }

        #endregion
    }


    internal class KeyValuePairHolder<T1, T2>
    {
        #region Свойства

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }

        #endregion
    }

    internal class ResultSelectorRewriter<TOuter, TInner, TResult> : ExpressionVisitor
    {
        #region Поля
        
        private readonly ParameterExpression _oldTOuterParamExpression;
        private readonly ParameterExpression _oldTInnerParamExpression;
        private readonly ParameterExpression _newTOuterParamExpression;
        private readonly ParameterExpression _newTInnerParamExpression;

        #endregion

        #region Свойства
        public Expression<Func<KeyValuePairHolder<TOuter, IEnumerable<TInner>>, TInner, TResult>> CombinedExpression { get; }

        #endregion

        #region Конструторы

        public ResultSelectorRewriter(Expression<Func<TOuter, TInner, TResult>> resultSelector)
        {
            var resultSelectorLocal = resultSelector;
            _oldTOuterParamExpression = resultSelectorLocal.Parameters[0];
            _oldTInnerParamExpression = resultSelectorLocal.Parameters[1];

            _newTOuterParamExpression = Expression.Parameter(typeof(KeyValuePairHolder<TOuter, IEnumerable<TInner>>));
            _newTInnerParamExpression = Expression.Parameter(typeof(TInner));

            var newBody = Visit(resultSelectorLocal.Body);
            var combinedExpression = Expression.Lambda(newBody, new ParameterExpression[] { _newTOuterParamExpression, _newTInnerParamExpression });
            CombinedExpression = (Expression<Func<KeyValuePairHolder<TOuter, IEnumerable<TInner>>, TInner, TResult>>)combinedExpression;
        }

        #endregion
        
        #region Методы

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node == _oldTInnerParamExpression)
                return _newTInnerParamExpression;
            else if (node == _oldTOuterParamExpression)
                return Expression.PropertyOrField(_newTOuterParamExpression, "Item1");
            else
                throw new InvalidOperationException($"Did not expect a parameter: {node}");
        }

        #endregion
    }
}