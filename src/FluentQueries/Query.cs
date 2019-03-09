using System;
using System.Linq.Expressions;

namespace FluentQueries
{
    /// <summary>
    /// <para>
    /// The Query class, representing a query that can
    /// be converted to an expression, or passed an object
    /// to test if it satisfies the Query.
    /// </para>
    /// <para>
    /// The Query can also be combined with other Queries
    /// using either And or Or operators. Using Has or Is
    /// allows the user to continue defining the query against
    /// the object itself, or one of the object's properties
    /// </para>
    /// </summary>
    /// <typeparam name="T">
    /// The Type of object that will be Queried.
    /// </typeparam>
    public class Query<T> : IQuery<T> 
    {
        private Expression<Func<T, bool>> _expression;

        /// <summary>
        /// Constructor to be used when inheriting from Query.
        /// </summary>
        protected Query() { }

        /// <summary>
        /// Instantiate a new Query from an expression.
        /// </summary>
        /// <param name="expression">The expression the Query will represent.</param>
        public Query(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        /// <summary>
        /// Instantiate a new Query from another Query.
        /// </summary>
        /// <param name="query">The other Query the new Query will represent.</param>
        public Query(IQuery<T> query) : this(query.AsExpression())
        {
            
        }

        /// <summary>
        /// Continues the Query, with another Query that must also evaluate to true.
        /// </summary>
        /// See <see cref="Or"/> to continue the Query with an Or operator.
        public QueryBuilderContinuation<T> And
            => new QueryBuilderContinuation<T>((q2) => CreateNewQuery(q2, Expression.And));

        /// <summary>
        /// Continues the Query, with another Query, one of which (the current query,
        /// or the continuation) must evaluate to true
        /// </summary>
        /// See <see cref="And"/> to continue the Query with an And operator.
        public QueryBuilderContinuation<T> Or
            => new QueryBuilderContinuation<T>((q2) => CreateNewQuery(q2, Expression.Or));

        /// <summary>
        /// To be used when inheriting from Query to define the query. A more
        /// convenient syntax than having to pass the query to the base constructor.
        /// </summary>
        /// <param name="query">The Query to setup this Query to represent</param>
        protected void Define(IQuery<T> query)
        {
            if(_expression != null)
            {
                throw new InvalidOperationException("Query already defined.");
            }
            _expression = query.AsExpression();
        }

        protected internal virtual Query<T> CreateNewQuery(IQuery<T> query, Func<Expression, Expression, Expression> combiner)
        {
            var e2 = query.AsExpression();
            var newE2 = new ParameterVisitor(e2.Parameters, _expression.Parameters).VisitAndConvert(e2.Body, nameof(CreateNewQuery));
            var e3 = combiner(_expression.Body, newE2);
            
            var lambda = Expression.Lambda<Func<T, bool>>(e3, _expression.Parameters);
            return new Query<T>(lambda);
        }

        /// <summary>
        /// Convert the Query to an expression.
        /// </summary>
        /// <returns>
        /// An expression representing the Query.
        /// </returns>
        public Expression<Func<T, bool>> AsExpression() => _expression;

        /// <summary>
        /// Check whether a subject satisfies the Query.
        /// </summary>
        /// <param name="subject">The subject to evaluate against the Query.</param>
        /// <returns>
        /// True if the subject satisfies the Query, false otherwise.
        /// </returns>
        /// See <see cref="ObjectExtensions.Satisfies{T}(T, IQuery{T})"/> to do the same operation, passing a Query to any object.
        public bool IsSatisfiedBy(T subject) => AsExpression().Compile()(subject);

        /// <summary>
        /// Select the property to query against.
        /// </summary>
        /// <typeparam name="TProp">The type of the property selected.</typeparam>
        /// <param name="expression">A lambda expression to access the property.</param>
        /// <returns>An object allowing the definition of a test to perform against the property</returns>
        public static QueryBuilderExpression<T, TProp> Has<TProp>(Expression<Func<T, TProp>> expression)
            => new QueryBuilderExpression<T, TProp>(expression);

        /// <summary>
        /// Query against the object itself. The equivalent of doing <c>Has(x => x)</c>.
        /// </summary>
        public static QueryBuilderExpression<T, T> Is => Has(e => e);

        /// <summary>
        /// Convert an expression to a Query implictly.
        /// </summary>
        /// <param name="query">The expression to convert to a Query.</param>
        public static implicit operator Query<T>(Expression<Func<T, bool>> query) => new Query<T>(query);
    }
}