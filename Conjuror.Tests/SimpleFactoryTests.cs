using Xunit;

namespace Conjurer.Test
{
    [Trait("Subject", "Simple Factory")]
    public class SimpleFactoryTests
    {
        [Fact]
        public void Can_build_product()
        {
            var factory = new SimpleFactory<Rabbit>();
            var product = factory.Create();
            Assert.NotNull(product);
        }

        [Fact]
        public void Accepts_action()
        {
            var factory = new SimpleFactory<Rabbit>(x => x.Name = "Fluffy");
            var product = factory.Create();

            Assert.Equal("Fluffy", product.Name);
        }

        [Fact]
        public void Can_handle_null_action()
        {
            Rabbit product = null;

            Assert.DoesNotThrow(() =>
            {
                var factory = new SimpleFactory<Rabbit>(null);
                product = factory.Create();
            });

            Assert.NotNull(product);
        }

        [Fact]
        public void Accepts_custom_action()
        {
            var factory = new SimpleFactory<Rabbit>(x => x.Age = 3);
            var product = factory.Create(x => x.Name = "Fluffy");
            Assert.Equal(3, product.Age);
            Assert.Equal("Fluffy", product.Name);
        }

        [Fact]
        public void Can_handle_null_custom_action()
        {
            var factory = new SimpleFactory<Rabbit>();
            Rabbit product = null;
            Assert.DoesNotThrow(() =>
            {
                product = factory.Create(null);
            });
            Assert.NotNull(product);
        }
    }
}
