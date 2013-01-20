using System;

namespace Conjurer
{
    public abstract class BaseFactory<Product> : IFactory<Product>
    {
        public abstract Product Create();

        public virtual Product Create(Action<Product> customAction)
        {
            Product product = Create();
            if (customAction != null) customAction.Invoke(product);
            return product;
        }
    }
}
