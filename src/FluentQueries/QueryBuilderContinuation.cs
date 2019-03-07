using System;
using System.Linq.Expressions;

namespace FluentQueries
{
    /// <summary>
    /// Continuation of a Query, after using <see cref="Query{T}.And" /> or <see cref="Query{T}.Or"/>.
    /// Allows for the definition of another Query to extend the previous.
    /// </summary>
    /// <typeparam name="T">The type of object the Query is against.</typeparam>
    public class QueryBuilderContinuation<T>
    {
        private readonly Func<Query<T>, Query<T>> _continueWith;

        internal QueryBuilderContinuation(Func<Query<T>, Query<T>> continueWith)
        {
            _continueWith = continueWith;
        }

        /// <summary>
        /// Query against the object itself. The equivalent of doing <c>Has(x => x)</c>.
        /// </summary>
        public QueryBuilderExpression<T, T> Is => new QueryBuilderExpression<T, T>(e => e, _continueWith);
        /// <summary>
        /// Select the property to query against.
        /// </summary>
        /// <typeparam name="TProp">The type of the property selected.</typeparam>
        /// <param name="expression">A lambda expression to access the property.</param>
        /// <returns>An object allowing the definition of a test to perform against the property</returns>
        public QueryBuilderExpression<T, TProp> Has<TProp>(Expression<Func<T, TProp>> expression)
            => new QueryBuilderExpression<T, TProp>(expression, _continueWith);     
    }
}