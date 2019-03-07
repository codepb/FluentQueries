using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentQueries
{
    /// <summary>
    /// Once a property has been selected to execute the Query against, this class
    /// allows the selection of a test to perform against the property, such as 
    /// <see cref="EqualTo(TProp)"/> or <see cref="GreaterThan(TProp)"/>.
    /// </summary>
    /// <typeparam name="T">The type of object the overall Query is against.</typeparam>
    /// <typeparam name="TProp">The type of the property that will be tested.</typeparam>
    public class QueryBuilderExpression<T, TProp>
    {
        internal readonly Expression<Func<T, TProp>> _expression;
        internal readonly Func<Query<T>, Query<T>> _continueWith;

        internal QueryBuilderExpression(Expression<Func<T, TProp>> expression, Func<Query<T>, Query<T>> continueWith = null)
        {
            _expression = expression;
            _continueWith = continueWith ?? (q => new Query<T>(q));
        }

        private Query<T> CreateQuery(TProp other, Func<Expression, Expression, BinaryExpression> expressionCreator)
        {
            var otherEntity = Expression.Constant(other, typeof(TProp));
            var expression = expressionCreator(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return _continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property is equal to the supplied value.
        /// </summary>
        /// <param name="other">The value to compare the property to.</param>
        /// <returns>True if the property is equal to the supplied value, false otherwise.</returns>
        public Query<T> EqualTo(TProp other)
            => CreateQuery(other, Expression.Equal);

        /// <summary>
        /// The property is not equal to the supplied value.
        /// </summary>
        /// <param name="other">The value to compare the property to.</param>
        /// <returns>True if the property is not equal to the supplied value, false otherwise.</returns>
        public Query<T> NotEqualTo(TProp other)
            => CreateQuery(other, Expression.NotEqual);

        /// <summary>
        /// The property is equal to one of the supplied values.
        /// </summary>
        /// <param name="values">An enumerable of values to compare the property to.</param>
        /// <returns>True if the property is equal to one of the supplied values, false otherwise.</returns>
        public Query<T> EqualToAnyOf(IEnumerable<TProp> values)
        {
            var method = typeof(Enumerable).GetMethods().Where(m => m.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2).
                    MakeGenericMethod(typeof(TProp));
            var otherEntity = Expression.Constant(values, typeof(IEnumerable<TProp>));
            var expression = Expression.Call(method, otherEntity, _expression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return _continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property is not equal to all of the supplied values.
        /// </summary>
        /// <param name="values">An enumerable of values to compare the property to.</param>
        /// <returns>True if the property is not equal to all of the supplied values, false otherwise.</returns>
        public Query<T> NotEqualToAnyOf(IEnumerable<TProp> values)
        {
            var method = typeof(Enumerable).GetMethods().Where(m => m.Name == "Contains")
                .Single(x => x.GetParameters().Length == 2).
                    MakeGenericMethod(typeof(TProp));
            var otherEntity = Expression.Constant(values, typeof(IEnumerable<TProp>));
            var expression = Expression.Call(method, otherEntity, _expression.Body);
            var notExpression = Expression.Not(expression);
            var lambda = Expression.Lambda<Func<T, bool>>(notExpression, _expression.Parameters);
            return _continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property is equal to one of the supplied values.
        /// </summary>
        /// <param name="values">An array of values to compare the property to.</param>
        /// <returns>True if the property is equal to one of the supplied values, false otherwise.</returns>
        public Query<T> EqualToAnyOf(params TProp[] values) =>
            EqualToAnyOf((IEnumerable<TProp>)values);

        /// <summary>
        /// The property is not equal to all of the supplied values.
        /// </summary>
        /// <param name="values">An array of values to compare the property to.</param>
        /// <returns>True if the property is not equal to all of the supplied values, false otherwise.</returns>
        public Query<T> NotEqualToAnyOf(params TProp[] values) =>
            NotEqualToAnyOf((IEnumerable<TProp>) values);

        /// <summary>
        /// The property is greater than the supplied value.
        /// </summary>
        /// <param name="other">The value to compare the property to.</param>
        /// <returns>True if the property is greater than the supplied value, false otherwise.</returns>
        public Query<T> GreaterThan(TProp other)
            => CreateQuery(other, Expression.GreaterThan);

        /// <summary>
        /// The property is greater than or equal to the supplied value.
        /// </summary>
        /// <param name="other">The value to compare the property to.</param>
        /// <returns>True if the property is greater than or equal to the supplied value, false otherwise.</returns>
        public Query<T> GreaterThanOrEqualTo(TProp other)
            => CreateQuery(other, Expression.GreaterThanOrEqual);

        /// <summary>
        /// The property is less than the supplied value.
        /// </summary>
        /// <param name="other">The value to compare the property to.</param>
        /// <returns>True if the property is less than the supplied value, false otherwise.</returns>
        public Query<T> LessThan(TProp other)
            => CreateQuery(other, Expression.LessThan);

        /// <summary>
        /// The property is less than or equal to the supplied value.
        /// </summary>
        /// <param name="other">The value to compare the property to.</param>
        /// <returns>True if the property is less than or equal to the supplied value, false otherwise.</returns>
        public Query<T> LessThanOrEqualTo(TProp other)
            => CreateQuery(other, Expression.LessThanOrEqual);

        /// <summary>
        /// The property satisfies the supplied Query.
        /// </summary>
        /// <param name="query">The query to test the property against.</param>
        /// <returns>True if the property satisfies the supplied Query, false otherwise.</returns>
        public Query<T> Satisfying(IQuery<TProp> query)
            => Satisfying(query.AsExpression());

        /// <summary>
        /// The property satisfies the supplied Expression.
        /// </summary>
        /// <param name="query">The expression to test the property against.</param>
        /// <returns>True if the property satisfies the supplied Query, false otherwise.</returns>
        public Query<T> Satisfying(Expression<Func<TProp, bool>> query)
            => _continueWith(new Query<T>(query.WithParameter(_expression)));

        /// <summary>
        /// The property is null.
        /// </summary>
        /// <returns>True if the property is null, false otherwise.</returns>
        public Query<T> Null()
        {
            var otherEntity = Expression.Constant(null, typeof(TProp));
            var expression = Expression.Equal(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return _continueWith(new Query<T>(lambda));
        }

        /// <summary>
        /// The property is not null
        /// </summary>
        /// <returns>True if the property is not null, false otherwise.</returns>
        public Query<T> NotNull()
        {
            var otherEntity = Expression.Constant(null, typeof(TProp));
            var expression = Expression.NotEqual(_expression.Body, otherEntity);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, _expression.Parameters);
            return _continueWith(new Query<T>(lambda));
        }
    }
}