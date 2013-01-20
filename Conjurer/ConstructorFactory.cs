using System;

namespace Conjurer
{
    public class ConstructorFactory<Product> : BaseFactory<Product>
    {
        private Func<Product> action;

        public ConstructorFactory(Func<Product> action)
        {
            this.action = action;
        }

        public override Product Create()
        {
            return action.Invoke();
        }
    }
}