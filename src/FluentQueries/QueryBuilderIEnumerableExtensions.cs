using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentQueries
{
    /// <summary>
    /// Extension methods against IEnumerable types for Queries.
    /// </summary>
    public static class QueryBuilderIEnumerableExtensions
    {
        /// <summary>
        /// The property contains the supplied value.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <typeparam name="T1">The type of the enumerable</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="other">The value to test if contained by the property.</param>
        /// <returns>True if the property contans the supplied value, false otherwise.</returns>
        public static Query<T> Containing<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, T1 other)
        {
            var otherEntity = Expression.Constant(other, typeof(T1));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "Contains" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property does not contain the supplied value.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <typeparam name="T1">The type of the enumerable</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="other">The value to test if not contained by the property.</param>
        /// <returns>True if the property does not contans the supplied value, false otherwise.</returns>
        public static Query<T> NotContaining<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, T1 other)
        {
            var otherEntity = Expression.Constant(other, typeof(T1));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "Contains" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property contains at least one value.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <typeparam name="T1">The type of the enumerable</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <returns>True if the property contains at least one value, false otherwise.</returns>
        public static Query<T> NotEmpty<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression)
        {
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "Any" && m.GetParameters().Length == 1)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property contains no values.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <typeparam name="T1">The type of the enumerable</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <returns>True if the property contains no values, false otherwise.</returns>
        public static Query<T> Empty<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression)
        {
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "Any" && m.GetParameters().Length == 1)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property has at least one value satisfying the supplied function.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <typeparam name="T1">The type of the enumerable</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="func">The function to test the values of the enumerable property against.</param>
        /// <returns>True if the property has at least one value satisfying the supplied function, false otherwise.</returns>
        public static Query<T> WithAny<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, Func<T1, bool> func)
        {
            var otherEntity = Expression.Constant(func, typeof(Func<T1, bool>));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "Any" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property has no values satisfying the supplied function.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <typeparam name="T1">The type of the enumerable</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="func">The function to test the values of the enumerable property against.</param>
        /// <returns>True if the property contains no values satisfying the supplied function, false otherwise.</returns>
        public static Query<T> WithoutAny<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, Func<T1, bool> func)
        {
            var otherEntity = Expression.Constant(func, typeof(Func<T1, bool>));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "Any" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property has all values satisfying the supplied function.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <typeparam name="T1">The type of the enumerable</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="func">The function to test the values of the enumerable property against.</param>
        /// <returns>True if the property has every value satisfying the supplied function, false otherwise.</returns>
        public static Query<T> WithAll<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, Func<T1, bool> func)
        {
            var otherEntity = Expression.Constant(func, typeof(Func<T1, bool>));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "All" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property has at least one value not satisfying the supplied function.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <typeparam name="T1">The type of the enumerable</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="func">The function to test the values of the enumerable property against.</param>
        /// <returns>True if the property contains at least one value that does not satisfy the supplied function. false otherwise.</returns>
        public static Query<T> WithNotAll<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, Func<T1, bool> func)
        {
            var otherEntity = Expression.Constant(func, typeof(Func<T1, bool>));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "All" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property contains the same values in the same order as the supplied enumerable.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <typeparam name="T1">The type of the enumerable</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="other">The enumerable to compare the property to.</param>
        /// <returns>True if the property contains the same elements in the same order as the supplied enumerable, false otherwise</returns>
        public static Query<T> EqualToSequence<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, IEnumerable<T1> other)
        {
            var otherEntity = Expression.Constant(other, typeof(IEnumerable<T1>));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "SequenceEqual" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property does not contain the same values in the same order as the supplied enumerable.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <typeparam name="T1">The type of the enumerable</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="other">The enumerable to compare the property to.</param>
        /// <returns>True if the property does not contain the same elements in the same order as the supplied enumerable, false otherwise</returns>
        public static Query<T> NotEqualToSequence<T, T1>(this QueryBuilderExpression<T, IEnumerable<T1>> queryBuilderExpression, IEnumerable<T1> other)
        {
            var otherEntity = Expression.Constant(other, typeof(IEnumerable<T1>));
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "SequenceEqual" && m.GetParameters().Length == 2)?.MakeGenericMethod(typeof(T1));
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }
    }
}
