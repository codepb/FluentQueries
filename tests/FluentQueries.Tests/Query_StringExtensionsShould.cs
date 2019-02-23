using FluentAssertions;
using FluentQueries.Tests.Entities;
using Xunit;

namespace FluentQueries.Tests
{
    public class Query_StringExtensionsShould
    {
        private TestEntity _testEntity = new TestEntity()
        {
            A = "abc",
            B = "def",
            C = 123
        };

        [Fact]
        public void StartsWithCorrectlyReturnsTrue()
        {
            var query = Query<TestEntity>.Has(a => a.A).StartingWith("a").And.Has(a => a.B).StartingWith("de");
            
            _testEntity.Satisfies(query).Should().BeTrue();
        }
        [Fact]
        public void StartsWithCorrectlyReturnsFalse()
        {
            var query = Query<TestEntity>.Has(a => a.A).StartingWith("a").And.Has(a => a.B).StartingWith("e");

            _testEntity.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void NotStartsWithCorrectlyReturnsTrue()
        {
            var query = Query<TestEntity>.Has(a => a.A).NotStartingWith("b").And.Has(a => a.B).NotStartingWith("e");

            _testEntity.Satisfies(query).Should().BeTrue();
        }
        [Fact]
        public void NotStartsWithCorrectlyReturnsFalse()
        {
            var query = Query<TestEntity>.Has(a => a.A).NotStartingWith("a").And.Has(a => a.B).NotStartingWith("e");

            _testEntity.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void EndsWithCorrectlyReturnsTrue()
        {
            var query = Query<TestEntity>.Has(a => a.A).EndingWith("c").And.Has(a => a.B).EndingWith("ef");

            _testEntity.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EndsWithCorrectlyReturnsFalse()
        {
            var query = Query<TestEntity>.Has(a => a.A).EndingWith("ab").And.Has(a => a.B).EndingWith("ef");

            _testEntity.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void NotEndsWithCorrectlyReturnsTrue()
        {
            var query = Query<TestEntity>.Has(a => a.A).NotEndingWith("b").And.Has(a => a.B).NotEndingWith("de");

            _testEntity.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEndsWithCorrectlyReturnsFalse()
        {
            var query = Query<TestEntity>.Has(a => a.A).NotEndingWith("ab").And.Has(a => a.B).NotEndingWith("ef");

            _testEntity.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void ContainingCorrectlyReturnsTrue()
        {
            var query = Query<TestEntity>.Has(a => a.A).Containing("b").And.Has(a => a.B).Containing("ef");

            _testEntity.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void ContainingCorrectlyReturnsFalse()
        {
            var query = Query<TestEntity>.Has(a => a.A).Containing("ab").And.Has(a => a.B).Containing("fg");

            _testEntity.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void NotContainingCorrectlyReturnsTrue()
        {
            var query = Query<TestEntity>.Has(a => a.A).NotContaining("ef").And.Has(a => a.B).NotContaining("a");

            _testEntity.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotContainingCorrectlyReturnsFalse()
        {
            var query = Query<TestEntity>.Has(a => a.A).NotContaining("ab").And.Has(a => a.B).NotContaining("fg");

            _testEntity.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void NullOrWhitespaceCorrectlyReturnsTrue()
        {
            var query = Query<string>.Is.NullOrWhitespace();

            "   ".Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NullOrWhitespaceCorrectlyReturnsFalse()
        {
            var query = Query<string>.Is.NullOrWhitespace();

            "a".Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void NotNullOrWhitespaceCorrectlyReturnsTrue()
        {
            var query = Query<string>.Is.NotNullOrWhitespace();

            "c".Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotNullOrWhitespaceCorrectlyReturnsFalse()
        {
            var query = Query<string>.Is.NotNullOrWhitespace();

            "    ".Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void NullOrEmptyCorrectlyReturnsTrue()
        {
            var query = Query<string>.Is.NullOrEmpty();

            "".Satisfies(query).Should().BeTrue();
            query.IsSatisfiedBy(null).Should().BeTrue();
        }

        [Fact]
        public void NullOrEmptyCorrectlyReturnsFalse()
        {
            var query = Query<string>.Is.NullOrEmpty();

            "    ".Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void NotNullOrEmptyCorrectlyReturnsTrue()
        {
            var query = Query<string>.Is.NotNullOrEmpty();

            "   ".Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotNullOrEmptyCorrectlyReturnsFalse()
        {
            var query = Query<string>.Is.NotNullOrEmpty();

            "".Satisfies(query).Should().BeFalse();
            query.IsSatisfiedBy(null).Should().BeFalse();
        }
    }
}
