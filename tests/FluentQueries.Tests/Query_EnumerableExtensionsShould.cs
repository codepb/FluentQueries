using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace FluentQueries.Tests
{
    public class Query_EnumerableExtensionsShould
    {
        private IEnumerable<int> _testEnumerable = new[] { 1, 2, 3, 4, 5 };
        private IEnumerable<int> _emptyEnumerable = new int[] { };

        [Fact]
        public void ContainsShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.Containing(3);
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void ContainsShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.Containing(6);
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void NotContainsShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.NotContaining(6);
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotContainsShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.NotContaining(3);
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void NotEmptyShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.NotEmpty();
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEmptyShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.NotEmpty();
            _emptyEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void EmptyShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.Empty();
            _emptyEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EmptyShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.Empty();
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void WithAnyShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.WithAny(i => i == 3);
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithAnyShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.WithAny(i => i == 9);
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void WithoutAnyShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.WithoutAny(i => i == 7);
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithoutAnyShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.WithoutAny(i => i == 3);
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void WithAllShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.WithAll(i => i < 6 && i > 0);
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithAllShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.WithAll(i => i < 5);
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void WithNotAllShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.WithNotAll(i => i < 5);
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithNotAllShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.WithNotAll(i => i < 6 && i > 0);
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void EqualToSequenceShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.EqualToSequence(new[] { 1, 2, 3, 4, 5 });
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EqualToSequenceShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.EqualToSequence(new[] { 1, 2, 4, 5 });
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void NotEqualToSequenceShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.NotEqualToSequence(new[] { 1, 2, 3, 4, 5, 6 });
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEqualToSequenceShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.NotEqualToSequence(new[] { 1, 2, 3, 4, 5 });
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void EqualToSequenceWorksOnCollections()
        {
            var query = Query<ICollection<int>>.Is.EqualToSequence(new[] { 1, 2, 3, 4, 5 });
            ICollection<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEqualToSequenceWorksOnCollections()
        {
            var query = Query<ICollection<int>>.Is.NotEqualToSequence(new[] { 1, 2, 4, 4, 5 });
            ICollection<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithAllWorksOnCollections()
        {
            var query = Query<ICollection<int>>.Is.WithAll(i => i < 6 && i > 0);
            ICollection<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithNotAllWorksOnCollections()
        {
            var query = Query<ICollection<int>>.Is.WithNotAll(i => i < 5 && i > 0);
            ICollection<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithAnyWorksOnCollections()
        {
            var query = Query<ICollection<int>>.Is.WithAny(i => i < 2 && i > 0);
            ICollection<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithoutAnyWorksOnCollections()
        {
            var query = Query<ICollection<int>>.Is.WithoutAny(i => i < 1);
            ICollection<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EmptyWorksOnCollections()
        {
            var query = Query<ICollection<int>>.Is.Empty();
            ICollection<int> collection = new int[] { };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEmptyWorksOnCollections()
        {
            var query = Query<ICollection<int>>.Is.NotEmpty();
            ICollection<int> collection = new int[] { 1,2,3 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void ContainingWorksOnCollections()
        {
            var query = Query<ICollection<int>>.Is.Containing(3);
            ICollection<int> collection = new int[] { 1,2,3,4,5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotContainingWorksOnCollections()
        {
            var query = Query<ICollection<int>>.Is.NotContaining(6);
            ICollection<int> collection = new int[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EqualToSequenceWorksOnReadOnlyCollections()
        {
            var query = Query<IReadOnlyCollection<int>>.Is.EqualToSequence(new[] { 1, 2, 3, 4, 5 });
            IReadOnlyCollection<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEqualToSequenceWorksOnReadOnlyCollections()
        {
            var query = Query<IReadOnlyCollection<int>>.Is.NotEqualToSequence(new[] { 1, 2, 4, 4, 5 });
            IReadOnlyCollection<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithAllWorksOnReadOnlyCollections()
        {
            var query = Query<IReadOnlyCollection<int>>.Is.WithAll(i => i < 6 && i > 0);
            IReadOnlyCollection<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithNotAllWorksOnReadOnlyCollections()
        {
            var query = Query<IReadOnlyCollection<int>>.Is.WithNotAll(i => i < 5 && i > 0);
            IReadOnlyCollection<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithAnyWorksOnReadOnlyCollections()
        {
            var query = Query<IReadOnlyCollection<int>>.Is.WithAny(i => i < 2 && i > 0);
            IReadOnlyCollection<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithoutAnyWorksOnReadOnlyCollections()
        {
            var query = Query<IReadOnlyCollection<int>>.Is.WithoutAny(i => i < 1);
            IReadOnlyCollection<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EmptyWorksOnReadOnlyCollections()
        {
            var query = Query<IReadOnlyCollection<int>>.Is.Empty();
            IReadOnlyCollection<int> collection = new int[] { };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEmptyWorksOnReadOnlyCollections()
        {
            var query = Query<IReadOnlyCollection<int>>.Is.NotEmpty();
            IReadOnlyCollection<int> collection = new int[] { 1, 2, 3 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void ContainingWorksOnReadOnlyCollections()
        {
            var query = Query<IReadOnlyCollection<int>>.Is.Containing(3);
            IReadOnlyCollection<int> collection = new int[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotContainingWorksOnReadOnlyCollections()
        {
            var query = Query<IReadOnlyCollection<int>>.Is.NotContaining(6);
            IReadOnlyCollection<int> collection = new int[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EqualToSequenceWorksOnIList()
        {
            var query = Query<IList<int>>.Is.EqualToSequence(new[] { 1, 2, 3, 4, 5 });
            IList<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEqualToSequenceWorksOnIList()
        {
            var query = Query<IList<int>>.Is.NotEqualToSequence(new[] { 1, 2, 4, 4, 5 });
            IList<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithAllWorksOnIList()
        {
            var query = Query<IList<int>>.Is.WithAll(i => i < 6 && i > 0);
            IList<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithNotAllWorksOnIList()
        {
            var query = Query<IList<int>>.Is.WithNotAll(i => i < 5 && i > 0);
            IList<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithAnyWorksOnIList()
        {
            var query = Query<IList<int>>.Is.WithAny(i => i < 2 && i > 0);
            IList<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithoutAnyWorksOnIList()
        {
            var query = Query<IList<int>>.Is.WithoutAny(i => i < 1);
            IList<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EmptyWorksOnIList()
        {
            var query = Query<IList<int>>.Is.Empty();
            IList<int> collection = new int[] { };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEmptyWorksOnIList()
        {
            var query = Query<IList<int>>.Is.NotEmpty();
            IList<int> collection = new int[] { 1, 2, 3 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void ContainingWorksOnIList()
        {
            var query = Query<IList<int>>.Is.Containing(3);
            IList<int> collection = new int[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotContainingWorksOnIList()
        {
            var query = Query<IList<int>>.Is.NotContaining(6);
            IList<int> collection = new int[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EqualToSequenceWorksOnList()
        {
            var query = Query<List<int>>.Is.EqualToSequence(new[] { 1, 2, 3, 4, 5 });
            List<int> collection = new List<int> { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEqualToSequenceWorksOnList()
        {
            var query = Query<List<int>>.Is.NotEqualToSequence(new[] { 1, 2, 4, 4, 5 });
            List<int> collection = new List<int> { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithAllWorksOnList()
        {
            var query = Query<IList<int>>.Is.WithAll(i => i < 6 && i > 0);
            IList<int> collection = new[] { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithNotAllWorksOnList()
        {
            var query = Query<List<int>>.Is.WithNotAll(i => i < 5 && i > 0);
            List<int> collection = new List<int> { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithAnyWorksOnList()
        {
            var query = Query<List<int>>.Is.WithAny(i => i < 2 && i > 0);
            List<int> collection = new List<int> { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithoutAnyWorksOnList()
        {
            var query = Query<List<int>>.Is.WithoutAny(i => i < 1);
            List<int> collection = new List<int> { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EmptyWorksOnList()
        {
            var query = Query<List<int>>.Is.Empty();
            List<int> collection = new List<int> { };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEmptyWorksOnList()
        {
            var query = Query<List<int>>.Is.NotEmpty();
            List<int> collection = new List<int> { 1, 2, 3 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void ContainingWorksOnList()
        {
            var query = Query<List<int>>.Is.Containing(3);
            List<int> collection = new List<int> { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotContainingWorksOnList()
        {
            var query = Query<List<int>>.Is.NotContaining(6);
            List<int> collection = new List<int> { 1, 2, 3, 4, 5 };
            collection.Satisfies(query).Should().BeTrue();
        }
    }
}
