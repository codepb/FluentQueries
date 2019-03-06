using System;
using System.Linq.Expressions;

namespace FluentQueries
{
    /// <summary>
    /// <para>
    /// The Query interface, representing a query that can
    /// be converted to an expression, or passed an object
    /// to test if it satisfies the Query.
    /// </para>
    /// <para>
    /// The Query can also be combined with other Queries
    /// using either And or Or operators.
    /// </para>
    /// </summary>
    /// <typeparam name="T">
    /// The Type of object that will be Queried.
    /// </typeparam>
    public interface IQuery<T>
    {
        /// <summary>
        /// Continues the Query, with another Query that must also evaluate to true.
        /// </summary>
        /// See <see cref="Or"/> to continue the Query with an Or operator.
        QueryBuilderContinuation<T> And { get; }
        /// <summary>
        /// Continues the Query, with another Query, one of which (the current query,
        /// or the continuation) must evaluate to true
        /// </summary>
        /// See <see cref="And"/> to continue the Query with an And operator.
        QueryBuilderContinuation<T> Or { get; }
        /// <summary>
        /// Convert the Query to an expression.
        /// </summary>
        /// <returns>
        /// An expression representing the Query.
        /// </returns>
        Expression<Func<T, bool>> AsExpression();
        /// <summary>
        /// Check whether a subject satisfies the Query.
        /// </summary>
        /// <param name="subject">The subject to evaluate against the Query.</param>
        /// <returns>
        /// True if the subject satisfies the Query, false otherwise.
        /// </returns>
        /// See <see cref="ObjectExtensions.Satisfies{T}(T, IQuery{T})"/> to do the same operation, passing a Query to any object.
        bool IsSatisfiedBy(T subject);
    }
}
