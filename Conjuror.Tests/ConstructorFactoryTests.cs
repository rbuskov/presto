using Xunit;

namespace Conjurer.Test
{
    [Trait("Subject", "Constructor Factory")]
    public class ConstructorFactoryTests
    {
        [Fact]
        public void Can_build_product()
        {
            var factory = new ConstructorFactory<Rabbit>(() => { return new Rabbit(); });
            var product = factory.Create();
            Assert.NotNull(product);
            Assert.IsType<Rabbit>(product);
        }

        [Fact]
        public void Accepts_custom_action()
        {
            var factory = new ConstructorFactory<Rabbit>(() => { return new Rabbit { Age = 3 }; });
            var product = factory.Create(x => x.Name = "Fluffy");
            Assert.Equal(3, product.Age);
            Assert.Equal("Fluffy", product.Name);
        }

        [Fact]
        public void Can_handle_null_custom_action()
        {
            var factory = new ConstructorFactory<Rabbit>(() => { return new Rabbit(); });
            Rabbit product = null;
            Assert.DoesNotThrow(() =>
            {
                product = factory.Create(null);
            });
            Assert.NotNull(product);
        }
    }
}
