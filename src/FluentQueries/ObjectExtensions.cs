using System;
using System.Collections.Generic;
using System.Text;

namespace FluentQueries
{
    /// <summary>
    /// Extensions on objects to support Querying
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Test an object against a <see cref="IQuery{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object being tested</param>
        /// <param name="query">The Query to evaluate the object against.</param>
        /// <returns>True if the object satisfies the Query, false otherwise.</returns>
        /// See <see cref="IQuery{T}.IsSatisfiedBy(T)"/> to do the same operation, passing an object to a Query.
        public static bool Satisfies<T>(this T obj, IQuery<T> query) => query.IsSatisfiedBy(obj);
    }
}
