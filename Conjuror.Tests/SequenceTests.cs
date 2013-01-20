using Xunit;

namespace Conjurer.Tests
{
    [Trait("Subject", "Sequence")]
    public class SequenceTests
    {
        [Fact]
        public void Yields_consecutive_numbers()
        {
            var sequence = new Sequence();
            Assert.Equal(0, sequence.Next);
            Assert.Equal(1, sequence.Next);
        }

        [Fact]
        public void Can_be_seeded()
        {
            var sequence = new Sequence(100);
            Assert.Equal(100, sequence.Next);
        }

        [Fact]
        public void Can_be_reset()
        {
            var sequence = new Sequence(100);
            var value = sequence.Next;
            sequence.Reset();
            Assert.Equal(100, sequence.Next);
        }
    }
}
