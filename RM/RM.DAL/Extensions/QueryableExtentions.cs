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

        /// <summary>
        /// Метаданные метода <see cref="Enumerable.DefaultIfEmpty{TSource}(IEnumerable{TSource})"/>.
        /// </summary>
        private static readonly MethodInfo EnumerableDefaultIfEmptyMetadata = typeof(Enumerable).GetMethods()
                                                                                                .First(m => m.Name == nameof(Enumerable.DefaultIfEmpty) &&   
                                                                                                            m.GetParameters().Length == 1);
        /// <summary>
        /// Метаданные метода <see cref="Queryable.SelectMany{TSource, TCollection, TResult}(IQueryable{TSource}, Expression{Func{TSource, IEnumerable{TCollection}}}, Expression{Func{TSource, TCollection, TResult}})"/>.
        /// </summary>
        private static readonly MethodInfo QueryableSelectManyMetadata = typeof(Queryable).GetMethods()
                                                                                          .Last(m => m.Name == nameof(Queryable.SelectMany) && 
                                                                                                     m.GetParameters().Length == 3);
        /// <summary>
        /// Метаданные метода <see cref="Queryable.GroupJoin{TOuter, TInner, TKey, TResult}(IQueryable{TOuter}, IEnumerable{TInner}, Expression{Func{TOuter, TKey}}, Expression{Func{TInner, TKey}}, Expression{Func{TOuter, IEnumerable{TInner}, TResult}})"/>.
        /// </summary>
        private static readonly MethodInfo QueryableGroupJoinMetadata = typeof(Queryable).GetMethods()
                                                                                         .First(m => m.Name == nameof(Queryable.GroupJoin) && 
                                                                                                     m.GetParameters().Length == 5);

        #endregion

        #region Методы

        /// <summary>
        /// Соединяет две последовательности с помощью левого соединения.
        /// </summary>
        /// <typeparam name="TOuter">Тип данных элементов первой последовательности.</typeparam>
        /// <typeparam name="TInner">Тип данных элементов второй последовательности.</typeparam>
        /// <typeparam name="TKey">Тип данных ключей, возвращаемых функциями селектора ключа.</typeparam>
        /// <typeparam name="TResult">Тип данных результирующих элементов.</typeparam>
        /// <param name="outer">Первая последовательность элементов для соединения.</param>
        /// <param name="inner">Последовательность элементов, соединяемая с первой последовательностью элементов.</param>
        /// <param name="outerKeySelector">Функция, извлекающая ключ соединения из каждого элемента первой последовательности.</param>
        /// <param name="innerKeySelector">Функция, извлекающая ключ соединения из каждого элемента второй последовательности.</param>
        /// <param name="resultSelector">Функция для создания результирующего элемента для пары соответствующих элементов.</param>
        /// <returns>Последовательность элементов, полученных в результате левого соединения двух последовательностей элементов.</returns>
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

            var keyValuePairHolderMetadata = typeof(KeyValuePairHolder<,>).MakeGenericType(
                    typeof(TOuter),
                    typeof(IEnumerable<>).MakeGenericType(typeof(TInner))
                );

            var paramOuter = Expression.Parameter(typeof(TOuter));
            var paramInner = Expression.Parameter(typeof(IEnumerable<TInner>));
            var paramGroup = Expression.Parameter(keyValuePairHolderMetadata);

            var itemFirstName = nameof(KeyValuePairHolder<TKey, IEnumerable<TInner>>.ItemFirst);
            var itemSecondName = nameof(KeyValuePairHolder<TKey, IEnumerable<TInner>>.ItemSecond);

            //Создаём ламбда выражение для отображения результата выполнения метода Queryable.GroupJoin
            var resultSelect = Expression
                .Lambda(
                    Expression.MemberInit(
                        Expression.New(keyValuePairHolderMetadata),
                        Expression.Bind(
                            keyValuePairHolderMetadata.GetMember(itemFirstName).Single(),
                            paramOuter
                        ),
                        Expression.Bind(
                            keyValuePairHolderMetadata.GetMember(itemSecondName).Single(),
                            paramInner
                        )
                    ),
                    paramOuter,
                    paramInner
                );

            //Выполняем метод Queryable.GroupJoin
            var groupJoinResult = QueryableGroupJoinMetadata
                .MakeGenericMethod(
                    typeof(TOuter),
                    typeof(TInner),
                    typeof(TKey),
                    keyValuePairHolderMetadata
                )
                .Invoke(
                    null,
                    new object[]
                    {
                        outer,
                        inner,
                        outerKeySelector,
                        innerKeySelector,
                        resultSelect
                    }
                );

            //Создаём ламбда выражение для дальнейшего выполнения метода Enumerable.DefaultIfEmpty
            var collectionSelector = Expression.Lambda(
                            Expression.Call(
                                    null,
                                    EnumerableDefaultIfEmptyMetadata.MakeGenericMethod(typeof(TInner)),
                                    Expression.MakeMemberAccess(paramGroup, keyValuePairHolderMetadata.GetProperty(itemSecondName)))
                            ,
                            paramGroup
                        );

            var newResultSelector = new ResultSelectorRewriter<TOuter, TInner, TResult>(resultSelector).CombinedExpression;

            //Выполняем метод Queryable.SelectMany
            var selectManyResult = QueryableSelectManyMetadata
                .MakeGenericMethod(
                    keyValuePairHolderMetadata,
                    typeof(TInner),
                    typeof(TResult)
                )
                .Invoke(
                    null,
                    new object[]
                    {
                        groupJoinResult,
                        collectionSelector,
                        newResultSelector
                    }
                );

            return (IQueryable<TResult>)selectManyResult;
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


    /// <summary>
    /// Держатель пары ключевых объектов.
    /// </summary>
    /// <typeparam name="TFirst">Тип данных первого объекта.</typeparam>
    /// <typeparam name="TSecond">Тип данных второго объекта.</typeparam>
    internal class KeyValuePairHolder<TFirst, TSecond>
    {
        #region Свойства

        /// <summary>
        /// Первый ключевой объект.
        /// </summary>
        public TFirst ItemFirst { get; set; }

        /// <summary>
        /// Второй ключевой объект.
        /// </summary>
        public TSecond ItemSecond { get; set; }

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
                return Expression.PropertyOrField(_newTOuterParamExpression, 
                                                  nameof(KeyValuePairHolder<TOuter, IEnumerable<TInner>>.ItemFirst));
            else
                throw new ArgumentException($"Неизвестный параметр: {node}");
        }

        #endregion
    }
}