﻿using FluentAssertions;
using Xunit;

namespace FluentQueries.Tests
{
    public class Query_OrShould
    {
        private IQuery<string> _c = Query<string>.Is.EqualTo("c");
        private IQuery<string> _a = Query<string>.Is.EqualTo("a");
        private IQuery<string> _b = Query<string>.Is.EqualTo("b");
        [Fact]
        public void CorrectlyHandleNesting()
        {
            _b.And.Is.Satisfying(_c.Or.Is.Satisfying(_a)).IsSatisfiedBy("b").Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueWhenRightHandSideIsTrue()
        {
            _b.Or.Is.Satisfying(_a).IsSatisfiedBy("a").Should().BeTrue();
        }

        [Fact]
        public void ReturnTrueWhenLeftHandSideIsTrue()
        {
            var query = _a.And.Is.Satisfying(_a).Or.Is.Satisfying(_b);
            query.IsSatisfiedBy("a").Should().BeTrue();
        }


        [Fact]
        public void ReturnFalseWhenBothSidesAreFalse()
        {
            _b.And.Is.Satisfying(_a).Or.Is.Satisfying(_c).IsSatisfiedBy("a").Should().BeFalse();
        }
    }
}
