using System;

namespace Conjurer
{
    public class ChainedFactory<Product> : BaseFactory<Product>
    {
        private IFactory<Product> baseFactory;
        private Action<Product> action;

        public ChainedFactory(IFactory<Product> baseFactory, Action<Product> action)
        {
            this.baseFactory = baseFactory;
            this.action = action;
        }

        public override Product Create()
        {
            return baseFactory.Create(action);
        }
    }
}
