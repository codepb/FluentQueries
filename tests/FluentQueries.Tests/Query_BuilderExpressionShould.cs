using FluentAssertions;
using Xunit;

namespace FluentQueries.Tests
{
    public class Query_BuilderExpressionShould
    {
        [Fact]
        public void NullCorrectlyReturnTrue()
        {
            Query<string>.Is.Null().IsSatisfiedBy(null).Should().BeTrue();
        }

        [Fact]
        public void NullCorrectlyReturnFalse()
        {
            Query<string>.Is.Null().IsSatisfiedBy("a").Should().BeFalse();
        }

        [Fact]
        public void NotNullCorrectlyReturnTrue()
        {
            Query<string>.Is.NotNull().IsSatisfiedBy("a").Should().BeTrue();
        }

        [Fact]
        public void NotNullCorrectlyReturnFalse()
        {
            Query<string>.Is.NotNull().IsSatisfiedBy(null).Should().BeFalse();
        }

        [Fact]
        public void LessThanCorrectlyReturnTrue()
        {
            Query<int>.Is.LessThan(10).IsSatisfiedBy(9).Should().BeTrue();
        }

        [Fact]
        public void LessThanCorrectlyReturnFalse()
        {
            Query<int>.Is.LessThan(10).IsSatisfiedBy(10).Should().BeFalse();
        }

        [Fact]
        public void LessThanOrEqualToCorrectlyReturnTrue()
        {
            Query<int>.Is.LessThanOrEqualTo(10).IsSatisfiedBy(10).Should().BeTrue();
            Query<int>.Is.LessThanOrEqualTo(10).IsSatisfiedBy(7).Should().BeTrue();
        }

        [Fact]
        public void LessThanOrEqualToCorrectlyReturnFalse()
        {
            Query<int>.Is.LessThanOrEqualTo(10).IsSatisfiedBy(11).Should().BeFalse();
        }

        [Fact]
        public void GreaterThanCorrectlyReturnTrue()
        {
            Query<int>.Is.GreaterThan(10).IsSatisfiedBy(11).Should().BeTrue();
        }

        [Fact]
        public void GreaterThanCorrectlyReturnFalse()
        {
            Query<int>.Is.GreaterThan(10).IsSatisfiedBy(10).Should().BeFalse();
        }

        [Fact]
        public void GreaterThanOrEqualToCorrectlyReturnTrue()
        {
            Query<int>.Is.GreaterThanOrEqualTo(10).IsSatisfiedBy(10).Should().BeTrue();
            Query<int>.Is.GreaterThanOrEqualTo(10).IsSatisfiedBy(11).Should().BeTrue();
        }

        [Fact]
        public void GreaterThanOrEqualToCorrectlyReturnFalse()
        {
            Query<int>.Is.GreaterThanOrEqualTo(10).IsSatisfiedBy(9).Should().BeFalse();
        }

        [Fact]
        public void EqualToCorrectlyReturnTrue()
        {
            Query<int>.Is.EqualTo(10).IsSatisfiedBy(10).Should().BeTrue();
        }

        [Fact]
        public void EqualToCorrectlyReturnFalse()
        {
            Query<int>.Is.EqualTo(10).IsSatisfiedBy(11).Should().BeFalse();
        }

        [Fact]
        public void NotEqualToCorrectlyReturnTrue()
        {
            Query<int>.Is.NotEqualTo(10).IsSatisfiedBy(9).Should().BeTrue();
        }

        [Fact]
        public void NotEqualToCorrectlyReturnFalse()
        {
            Query<int>.Is.NotEqualTo(10).IsSatisfiedBy(10).Should().BeFalse();
        }

        [Fact]
        public void EqualToAnyOfCorrectlyReturnTrue()
        {
            Query<int>.Is.EqualToAnyOf(new[] { 10, 12, 14, 16 }).IsSatisfiedBy(10).Should().BeTrue();
        }

        [Fact]
        public void EqualToAnyOfCorrectlyReturnFalse()
        {
            Query<int>.Is.EqualToAnyOf(new[] { 10, 12, 14, 16 }).IsSatisfiedBy(11).Should().BeFalse();
        }

        [Fact]
        public void NotEqualToAnyOfCorrectlyReturnTrue()
        {
            Query<int>.Is.NotEqualToAnyOf(new[] { 10, 12, 14, 16 }).IsSatisfiedBy(11).Should().BeTrue();
        }

        [Fact]
        public void NotEqualToAnyOfCorrectlyReturnFalse()
        {
            Query<int>.Is.NotEqualToAnyOf(new[] { 10, 12, 14, 16 }).IsSatisfiedBy(10).Should().BeFalse();
        }

        [Fact]
        public void EqualToAnyOfParmeterSyntaxCorrectlyReturnTrue()
        {
            Query<int>.Is.EqualToAnyOf(1,2,3,4).IsSatisfiedBy(4).Should().BeTrue();
        }

        [Fact]
        public void EqualToAnyOfParmeterSyntaxCorrectlyReturnFalse()
        {
            Query<int>.Is.EqualToAnyOf(1,2,3,4).IsSatisfiedBy(5).Should().BeFalse();
        }
    }
}
