using System;
using System.Linq.Expressions;

namespace FluentQueries
{
    public class QueryBuilderContinuation<T>
    {
        private readonly Func<Query<T>, Query<T>> _continueWith;

        internal QueryBuilderContinuation(Func<Query<T>, Query<T>> continueWith)
        {
            _continueWith = continueWith;
        }

        public QueryBuilderExpression<T, T> Is => new QueryBuilderExpression<T, T>(e => e, _continueWith);
        public QueryBuilderExpression<T, TProp> Has<TProp>(Expression<Func<T, TProp>> expression)
            => new QueryBuilderExpression<T, TProp>(expression, _continueWith);     
    }
}