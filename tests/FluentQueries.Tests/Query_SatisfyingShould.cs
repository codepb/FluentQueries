﻿using FluentAssertions;
using FluentQueries.Tests.Entities;
using Xunit;

namespace FluentQueries.Tests
{
    public class Query_SatisfyingShould
    {
        private readonly TestEntity _testEntity = new TestEntity()
        {
            A = "abc",
            B = "def",
            C = 123,
            D = new TestEntityChild()
            {
                E = "ghi",
                F = true,
                G = 456
            }
        };

        [Fact]
        public void ReturnFalseIfLeftHandSideIsFalse()
        {
            var query = Query<TestEntity>.Has(q => q.A).EqualTo("def").And.Has(q => q.D).Satisfying(d => d.E == "ghi");
            query.IsSatisfiedBy(_testEntity).Should().BeFalse();
        }
    }
}
