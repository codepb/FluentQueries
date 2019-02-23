using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentQueries
{
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

        public static Query<T> Containing<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildQuery("Contains", queryBuilderExpression, other);

        public static Query<T> NotContaining<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildNotQuery("Contains", queryBuilderExpression, other);

        public static Query<T> StartingWith<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildQuery("StartsWith", queryBuilderExpression, other);

        public static Query<T> NotStartingWith<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildNotQuery("StartsWith", queryBuilderExpression, other);

        public static Query<T> EndingWith<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildQuery("EndsWith", queryBuilderExpression, other);

        public static Query<T> NotEndingWith<T>(this QueryBuilderExpression<T, string> queryBuilderExpression, string other)
            => BuildNotQuery("EndsWith", queryBuilderExpression, other);

        public static Query<T> NullOrWhitespace<T>(this QueryBuilderExpression<T, string> queryBuilderExpression)
        {
            var method = typeof(string).GetMethod("IsNullOrWhiteSpace", BindingFlags.Public | BindingFlags.Static);
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> NotNullOrWhitespace<T>(this QueryBuilderExpression<T, string> queryBuilderExpression)
        {
            var method = typeof(string).GetMethod("IsNullOrWhiteSpace", BindingFlags.Public | BindingFlags.Static);
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> NullOrEmpty<T>(this QueryBuilderExpression<T, string> queryBuilderExpression)
        {
            var method = typeof(string).GetMethod("IsNullOrEmpty", BindingFlags.Public | BindingFlags.Static);
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }

        public static Query<T> NotNullOrEmpty<T>(this QueryBuilderExpression<T, string> queryBuilderExpression)
        {
            var method = typeof(string).GetMethod("IsNullOrEmpty", BindingFlags.Public | BindingFlags.Static);
            var expression = Expression.Call(method, queryBuilderExpression._expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(expression), queryBuilderExpression._expression.Parameters);
            return queryBuilderExpression._continueWith(new Query<T>(lambda));
        }
    }
}