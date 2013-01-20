using System;

namespace Conjurer
{
    public class SimpleFactory<Product> : BaseFactory<Product> where Product : new()
    {
        private Action<Product> action;

        public SimpleFactory()
        {
        }

        public SimpleFactory(Action<Product> action)
        {
            this.action = action;
        }

        public override Product Create()
        {
            Product product = new Product();
            if (action != null) action.Invoke(product);
            return product;
        }
    }
}