using System;
using System.Linq.Expressions;

namespace FluentQueries
{
    public interface IQuery<T>
    {
        QueryBuilderContinuation<T> And { get; }
        QueryBuilderContinuation<T> Or { get; }
        Expression<Func<T, bool>> AsExpression();
        bool IsSatisfiedBy(T subject);
    }
}
