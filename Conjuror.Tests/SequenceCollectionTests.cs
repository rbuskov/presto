using Xunit;

namespace Conjurer.Test
{
    [Trait("Subject", "Sequence Collection")]
    public class SequenceCollectionTests
    {
        [Fact]
        public void Has_default_sequence()
        {
            var collection = new SequenceCollection();
            Assert.Equal(0, collection.Next());
            Assert.Equal(1, collection.Next());
        }

        [Fact]
        public void Can_seed_default_sequence()
        {
            var collection = new SequenceCollection();
            collection.Seed(100);
            Assert.Equal(100, collection.Next());
        }

        [Fact]
        public void Can_add_named_sequence()
        {
            var collection = new SequenceCollection();
            collection.Add("sequence");
            Assert.Equal(0, collection.Next("sequence"));
        }

        [Fact]
        public void Can_add_named_sequence_with_seed()
        {
            var collection = new SequenceCollection();
            collection.Add("sequence", 100);
            Assert.Equal(100, collection.Next("sequence"));
        }

        [Fact]
        public void Can_add_type_sequence()
        {
            var collection = new SequenceCollection();
            collection.Add<Rabbit>();
            Assert.Equal(0, collection.Next<Rabbit>());
        }

        [Fact]
        public void Can_add_type_sequence_with_seed()
        {
            var collection = new SequenceCollection();
            collection.Add<Rabbit>(100);
            Assert.Equal(100, collection.Next<Rabbit>());
        }

        [Fact]
        public void Can_add_named_type_sequence()
        {
            var collection = new SequenceCollection();
            collection.Add<Rabbit>("sequence");
            Assert.Equal(0, collection.Next<Rabbit>("sequence"));
        }

        [Fact]
        public void Can_add_named_type_sequence_with_seed()
        {
            var collection = new SequenceCollection();
            collection.Add<Rabbit>("sequence", 100);
            Assert.Equal(100, collection.Next<Rabbit>("sequence"));
        }

        [Fact]
        public void Can_check_if_sequence_exists()
        {
            var collection = new SequenceCollection();
            collection.Add("sequence");
            Assert.True(collection.Exists("sequence"));
        }

        [Fact]
        public void Can_check_if_sequence_not_exists()
        {
            var collection = new SequenceCollection();
            Assert.False(collection.Exists("sequence"));
        }

        [Fact]
        public void Can_reset_default_sequence()
        {
            var collection = new SequenceCollection();
            var value = collection.Next();
            collection.Reset();
            Assert.Equal(0, collection.Next());
        }

        [Fact]
        public void Can_reset_named_sequence()
        {
            var collection = new SequenceCollection();
            collection.Add("sequence");
            var value = collection.Next("sequence");
            collection.Reset();
            Assert.Equal(0, collection.Next("sequence"));
        }

        [Fact]
        public void Can_clear_named_sequences()
        {
            var collection = new SequenceCollection();
            collection.Add("sequence");
            collection.Clear();
            Assert.False(collection.Exists("sequence"));
        }

        [Fact]
        public void Clear_resets_default_sequence()
        {
            var collection = new SequenceCollection();
            collection.Next();
            collection.Clear();
            Assert.Equal(0, collection.Next());
        }

        [Fact]
        public void Cannot_add_duplicate_sequence()
        {
            var collection = new SequenceCollection();
            collection.Add("sequence");
            Assert.Throws<DuplicateSequenceException>(() => collection.Add("sequence"));
        }

        [Fact]
        public void Cannot_get_next_from_missing_sequence()
        {
            var collection = new SequenceCollection();
            Assert.Throws<MissingSequenceException>(() => collection.Next("sequence"));
        }
    }
}
