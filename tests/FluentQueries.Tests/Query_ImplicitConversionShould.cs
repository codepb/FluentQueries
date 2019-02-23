using FluentAssertions;
using System;
using System.Linq.Expressions;
using Xunit;

namespace FluentQueries.Tests
{
    public class Query_ImplicitConversionShould
    {
        [Fact]
        public void CorrectlyConvertToQuery()
        {
            Expression<Func<string, bool>> expression = ((string s) => s == "a");
            ((Query<string>)expression).IsSatisfiedBy("a").Should().BeTrue();
        }
    }
}
