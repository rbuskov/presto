using Xunit;

namespace Conjurer.Tests
{
    public class FactoryCollectionTests
    {
        [Fact]
        public void Can_add_default_factory()
        {
            var collection = new FactoryCollection();
            collection.Add(new SimpleFactory<Rabbit>());
            Assert.NotEmpty(collection);
        }

        [Fact]
        public void Can_add_named_factory()
        {
            var collection = new FactoryCollection();
            collection.Add(new SimpleFactory<Rabbit>(), "bunny");
            Assert.NotEmpty(collection);
        }

        [Fact]
        public void Can_get_default_factory()
        {
            var collection = new FactoryCollection();
            collection.Add(new SimpleFactory<Rabbit>());
            Assert.NotNull(collection.Get<Rabbit>());
        }

        [Fact]
        public void Can_get_named_factory()
        {
            var collection = new FactoryCollection();
            collection.Add(new SimpleFactory<Rabbit>(), "bunny");
            Assert.NotNull(collection.Get<Rabbit>("bunny"));
        }

        [Fact]
        public void Can_clear()
        {
            var collection = new FactoryCollection();
            collection.Add(new SimpleFactory<Rabbit>());
            collection.Clear();
            Assert.Empty(collection);
        }

        [Fact]
        public void Cannot_get_missing_factory()
        {
            var collection = new FactoryCollection();
            Assert.Throws<MissingSequenceException>(() => collection.Get<Rabbit>());
        }

        [Fact]
        public void Cannot_get_missing_named_factory()
        {
            var collection = new FactoryCollection();
            collection.Add<Rabbit>(new SimpleFactory<Rabbit>());
            Assert.Throws<MissingSequenceException>(() => collection.Get<Rabbit>("rabbit"));
        }

        [Fact]
        public void Cannot_add_duplicate_factory()
        {
            var collection = new FactoryCollection();
            collection.Add<Rabbit>(new SimpleFactory<Rabbit>());
            Assert.Throws<DuplicateSequenceException>(() => collection.Add<Rabbit>(new SimpleFactory<Rabbit>()));
        }


    }
}
