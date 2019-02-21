using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace DDDToolkit.Querying.Tests
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
