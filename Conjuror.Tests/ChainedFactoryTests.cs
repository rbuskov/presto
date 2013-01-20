using Xunit;

namespace Conjurer.Test
{
    [Trait("Subject", "Chained Factory")]
    public class ChainedFactoryTests
    {
        [Fact]
        public void Can_build_product()
        {
            var parentFactory = new SimpleFactory<Rabbit>(x => x.Name = "Fluffy");
            var childFactory = new ChainedFactory<Rabbit>(parentFactory, x => x.Name += " the Bunny");
            var product = childFactory.Create();
            Assert.Equal("Fluffy the Bunny", product.Name);
        }

        [Fact]
        public void Accepts_custom_action()
        {
            var parentFactory = new SimpleFactory<Rabbit>(x => x.Age = 3);
            var childFactory = new ChainedFactory<Rabbit>(parentFactory, x => x.Name = "Fluffy");
            var product = childFactory.Create(x => x.Name += " the Bunny");
            Assert.Equal(3, product.Age);
            Assert.Equal("Fluffy the Bunny", product.Name);
        }

        [Fact]
        public void Can_handle_null_factory()
        {
            var parentFactory = new SimpleFactory<Rabbit>();
            var childFactory = new ChainedFactory<Rabbit>(parentFactory, x => x.Name = "Fluffy");
            var product = childFactory.Create(null);
            Assert.Equal("Fluffy", product.Name);
        }
    }
}
