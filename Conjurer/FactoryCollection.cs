using System;
using System.Collections;
using System.Collections.Generic;

namespace Conjurer
{
    public class FactoryCollection : IEnumerable
    {
        private Dictionary<string, object> factories = new Dictionary<string, object>();

        private string GetKey<Product>(string factoryName)
        {
            return factoryName == null ? GetKey<Product>() : string.Format("{0}#{1}", typeof(Product).FullName, factoryName);
        }

        private string GetKey<Product>()
        {
            return typeof(Product).FullName;
        }

        public void Add<Product>(IFactory<Product> factory)
        {
            Add(factory, null);
        }

        public void Add<Product>(IFactory<Product> factory, string name)
        {
            string key = GetKey<Product>(name);

            if (factories.ContainsKey(key)) throw new DuplicateFactoryException(key);

            factories.Add(key, factory);
        }

        public IFactory<Product> Get<Product>()
        {
            return Get<Product>(null);
        }

        public IFactory<Product> Get<Product>(string name)
        {
            string key = GetKey<Product>(name);

            if (!factories.ContainsKey(key)) throw new MissingFactoryException(key);

            return factories[key] as IFactory<Product>;
        }

        public IEnumerator GetEnumerator()
        {
            return factories.Values.GetEnumerator();
        }

        public void Clear()
        {
            factories.Clear();
        }
    }
}
