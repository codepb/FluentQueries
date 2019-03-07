using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentQueries
{
    /// <summary>
    /// Extension methods against string types for Queries.
    /// </summary>
    public static class QueryBuilderStringExpressionExtensions
    {
        private static MethodCallExpression CreateExpression<T>(string methodName, QueryBuilderExpression<T, string> queryBuilderExpression, string other)
        {
            var otherEntity = Expression.Constant(other, typeof(string));
            var method = typeof(string).GetMethod(methodName, new[] { typeof(string) });
            var expression = Expression.Call(queryBuilderExpression._expression.Body, method, otherEntity);
            return expression;
        }

        private static Query<T> BuildQuery<T>(string methodName, QueryBuilderExpression<T, string> queryBuilderExpression, string other)
        {
            var expression = CreateExpression(methodName, queryBuilderExpression, other);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        private static Query<T> BuildNotQuery<T>(string methodName, QueryBuilderExpression<T, string> queryBuilderExpression, string other)
        {
            var expression = CreateExpression(methodName, queryBuilderExpression, other);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property contains the supplied string.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="other">The value to test the property contains.</param>
        /// <returns>True if the property contains the supplied value, false otherwise.</returns>
        public static Query<T> Containing<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildQuery("Contains", queryBuilderExpression, other);

        /// <summary>
        /// The property does not contain the supplied string.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="other">The value to test the property does not contain.</param>
        /// <returns>True if the property does not contain the supplied value, false otherwise.</returns>
        public static Query<T> NotContaining<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildNotQuery("Contains", queryBuilderExpression, other);

        /// <summary>
        /// The property starts with the supplied string.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="other">The value to test the property starts with.</param>
        /// <returns>True if the property starts with the supplied value, false otherwise.</returns>
        public static Query<T> StartingWith<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildQuery("StartsWith", queryBuilderExpression, other);

        /// <summary>
        /// The property does not start with the supplied string.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="other">The value to test the property does not start with.</param>
        /// <returns>True if the property does not start with the supplied value, false otherwise.</returns>
        public static Query<T> NotStartingWith<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildNotQuery("StartsWith", queryBuilderExpression, other);

        /// <summary>
        /// The property ends with the supplied string.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="other">The value to test the property ends with.</param>
        /// <returns>True if the property ends with the supplied value, false otherwise.</returns>
        public static Query<T> EndingWith<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildQuery("EndsWith", queryBuilderExpression, other);

        /// <summary>
        /// The property does not end with the supplied string.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <param name="other">The value to test the property does not end with.</param>
        /// <returns>True if the property does not end with the supplied value, false otherwise.</returns>
        public static Query<T> NotEndingWith<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildNotQuery("EndsWith", queryBuilderExpression, other);

        /// <summary>
        /// The property is null or whitespace.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <returns>True if the property is null or whitespace, false otherwise.</returns>
        public static Query<T> NullOrWhitespace<T>(this QueryBuilderExpression<T, string> queryBuilderExpression)
        {
            var method = typeof(string).GetMethod("IsNullOrWhiteSpace", BindingFlags.Public | BindingFlags.Static);
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property is not null or whitespace.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <returns>True if the property is not null or whitespace, false otherwise.</returns>
        public static Query<T> NotNullOrWhitespace<T>(this QueryBuilderExpression<T, string> queryBuilderExpression)
        {
            var method = typeof(string).GetMethod("IsNullOrWhiteSpace", BindingFlags.Public | BindingFlags.Static);
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <returns>True if the property is null or empty, false otherwise.</returns>
        public static Query<T> NullOrEmpty<T>(this QueryBuilderExpression<T, string> queryBuilderExpression)
        {
            var method = typeof(string).GetMethod("IsNullOrEmpty", BindingFlags.Public | BindingFlags.Static);
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property is not null or empty.
        /// </summary>
        /// <typeparam name="T">The type of object the Query is defined against.</typeparam>
        /// <param name="queryBuilderExpression">The Query.</param>
        /// <returns>True if the property is not null or empty, false otherwise.</returns>
        public static Query<T> NotNullOrEmpty<T>(this QueryBuilderExpression<T, string> queryBuilderExpression)
        {
            var method = typeof(string).GetMethod("IsNullOrEmpty", BindingFlags.Public | BindingFlags.Static);
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }
    }
}