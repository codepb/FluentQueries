using System;
using System.Linq.Expressions;

namespace DDDToolkit.Querying
{
    public interface IQuery<T>
    {
        QueryBuilderContinuation<T> And { get; }
        QueryBuilderContinuation<T> Or { get; }
        Expression<Func<T, bool>> AsExpression();
        bool IsSatisfiedBy(T subject);
    }
}
