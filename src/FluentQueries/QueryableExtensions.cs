using System;
using System.Linq;
using System.Linq.Expressions;

namespace FluentQueries
{
    /// <summary>
    /// Extensions for Queryables
    /// </summary>
    public static class QueryableExtensions
    {
        internal static Expression<Func<TResult, bool>> WithParameter<TResult, TSource>(this Expression<Func<TSource, bool>> source, Expression<Func<TResult, TSource>> selector)
        {
            // Replace parameter with body of selector
            var replaceParameter = new ParameterVisitor(source.Parameters, selector.Body);
            // This will be the new body of the expression
            var newExpressionBody = replaceParameter.Visit(source.Body);
            return Expression.Lambda<Func<TResult, bool>>(newExpressionBody, selector.Parameters);
        }

        /// <summary>
        /// Filters the queryable to only those that satisfy the supplied query.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="source">The source queryable.</param>
        /// <param name="query">The query to test objects against.</param>
        /// <returns>A queryable that tests objects against the supplied query.</returns>
        public static IQueryable<T> WhereSatisfies<T>(this IQueryable<T> source, Query<T> query)
        {
            return source.Where(query.AsExpression());
        }
    }
}
