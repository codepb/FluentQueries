using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FluentQueries
{
    /// <summary>
    /// Extension methods against boolean types for Queries.
    /// </summary>
    public static class QueryBuilderBooleanExtensions
    {
        private static Query<T> BuildQuery<T>(ConstantExpression otherEntity, QueryBuilderExpression<T, bool> queryBuilderExpression)
        {
            var expression = Expression.Equal(queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }
        
        /// <summary>
        /// The property is true.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <returns>True if property is true, false otherwise.</returns>
        public static Query<T> True<T>(this QueryBuilderExpression<T, bool> queryBuilderExpression)
            => BuildQuery(Expression.Constant(true, typeof(bool)), queryBuilderExpression);

        /// <summary>
        /// The property is false.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <returns>True if property is false, false otherwise.</returns>
        public static Query<T> False<T>(this QueryBuilderExpression<T, bool> queryBuilderExpression)
            => BuildQuery(Expression.Constant(false, typeof(bool)), queryBuilderExpression);
    }
}
