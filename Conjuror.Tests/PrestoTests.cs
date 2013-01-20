using System;
using Xunit;

namespace Conjurer.Test.ConjurerTest
{
    [Trait("Subject", "Presto")]
    public class ConjurerTest 
    {
        public ConjurerTest()
        {
            Presto.Clear();
        }

        [Fact]
        public void Can_define_simple_factory()
        {
            Presto.Define<Rabbit>();
            Assert.NotEmpty(Presto.Factories);
            var product = Presto.Create<Rabbit>();
            Assert.NotNull(product);
            Assert.IsType<Rabbit>(product);
        }
        
        [Fact]
        public void Can_define_basic_factory_with_action()
        {
            Presto.Define<Rabbit>(x => x.Name = "Fluffy");
            var product = Presto.Create<Rabbit>();
            Assert.Equal("Fluffy", product.Name);
        }

        [Fact]
        public void Can_define_named_basic_factory()
        {
            Presto.Define<Rabbit>("named_rabbit");
            var product = Presto.Create<Rabbit>("named_rabbit");
            Assert.NotNull(product);
            Assert.IsType<Rabbit>(product);
        }

        [Fact]
        public void Can_define_named_basic_factory_with_action()
        {
            Presto.Define<Rabbit>("named_rabbit", x => x.Name = "Fluffy");
            var product = Presto.Create<Rabbit>("named_rabbit");
            Assert.Equal("Fluffy", product.Name);
        }

        [Fact]
        public void Can_define_constructor_factory()
        {
            Presto.Define<Rabbit>(() => { return new Rabbit(); });
            var product = Presto.Create<Rabbit>();
            Assert.NotNull(product);
            Assert.IsType<Rabbit>(product);
        }

        [Fact]
        public void Can_define_named_constructor_factory()
        {
            Presto.Define<Rabbit>("named_rabbit", () => { return new Rabbit(); });
            var product = Presto.Create<Rabbit>("named_rabbit");
            Assert.NotNull(product);
            Assert.IsType<Rabbit>(product);
        }

        [Fact]
        public void Can_define_chained_factory()
        {
            var parentFactory = Presto.Define<Rabbit>(x => x.Name = "Fluffy");
            var childFactory = Presto.Define<Rabbit>("chained_factory", parentFactory, x => { x.Name += " the Bunny";  });
            var product = Presto.Create<Rabbit>("chained_factory");
            Assert.Equal("Fluffy the Bunny", product.Name);
        }

        [Fact]
        public void Can_create_product_with_custom_action()
        {
            Presto.Define<Rabbit>();
            var product = Presto.Create<Rabbit>(x => x.Name = "Fluffy");
            Assert.Equal("Fluffy", product.Name);
        }

        [Fact]
        public void Can_create_multiple_products()
        {
            Presto.Define<Rabbit>();
            var products = Presto.Create<Rabbit>(3, null, x => x.Name = "Fluffy");
            Assert.NotNull(products);
            Assert.Equal(3, products.Count);
            Assert.Equal("Fluffy", products[0].Name);
        }


        [Fact]
        public void Can_define_persist_action()
        {
            Presto.PersistAction = x => { };
            Assert.NotNull(Presto.PersistAction);
        }

        [Fact]
        public void Can_persist_product()
        {
            Rabbit product = null;
            Presto.Define<Rabbit>();
            Presto.PersistAction = x => product = x as Rabbit;
            Presto.Persist<Rabbit>();
            Assert.NotNull(product);
        }

        [Fact]
        public void Can_persist_multiple_products()
        {
            var persistCount = 0;
            Presto.PersistAction = x => persistCount += 1;
            Presto.Define<Rabbit>();
            Presto.Persist<Rabbit>(3, null, x => x.Name = "Fluffy");
            Assert.Equal(3, persistCount);
        }


        [Fact]
        public void Can_create_nested_product()
        {
            var persistCount = 0;
            Presto.PersistAction = x => persistCount += 1;
            Presto.Define<Hat>();
            Presto.Define<Rabbit>(x => x.Hat = Presto.CreateOrPersist<Hat>());
            Presto.Create<Rabbit>();
            Assert.Equal(0, persistCount);
        }

        [Fact]
        public void Can_perist_nested_product()
        {
            var persistCount = 0;
            Presto.PersistAction = x => persistCount += 1;
            Presto.Define<Hat>();
            Presto.Define<Rabbit>(x => x.Hat = Presto.CreateOrPersist<Hat>());
            Presto.Persist<Rabbit>();
            Assert.Equal(2, persistCount);
        }

        [Fact]
        public void Can_create_or_perist_nested_collection()
        {
            var persistCount = 0;
            Presto.PersistAction = x => persistCount += 1;
            Presto.Define<Rabbit>();
            Presto.Define<Hat>(x => x.Rabbits = Presto.CreateOrPersist<Rabbit>(3, null, null));
            Presto.Persist<Hat>();
            Assert.Equal(4, persistCount);
        }


        [Fact]
        public void Has_factory_collection()
        {
            Presto.Define<Rabbit>();
            Assert.NotEmpty(Presto.Factories);
        }

        [Fact]
        public void Can_clear_factory_collection()
        {
            Presto.Define<Rabbit>();
            Presto.Clear();
            Assert.Empty(Presto.Factories);
        }

        [Fact]
        public void Has_sequence_collection()
        {
            Assert.Equal(0, Presto.Sequence.Next());
        }

        [Fact]
        public void Can_clear_sequence_collection()
        {
            Presto.Sequence.Add("sequence");
            Presto.Clear();
            Assert.False(Presto.Sequence.Exists("sequence"));
        }
    }
}