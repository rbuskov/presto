using System;

namespace Conjurer
{
    public interface IFactory<Product>
    {
        Product Create();
        Product Create(Action<Product> action);
    }
}