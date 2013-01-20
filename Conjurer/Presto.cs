using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Conjurer
{
    public class Presto
    {
        #region Factories Collection

        private static FactoryCollection factories = new FactoryCollection();

        public static FactoryCollection Factories
        {
            get { return factories; }
        }

        #endregion

        #region Sequences Collection

        private static SequenceCollection sequences = new SequenceCollection();

        public static SequenceCollection Sequence
        {
            get { return sequences; }
        }

        #endregion

        #region Define Basic Factory

        public static SimpleFactory<Product> Define<Product>() where Product : new()
        {
            return Define<Product>(null as string, null as Action<Product>);
        }

        public static SimpleFactory<Product> Define<Product>(string factoryName) where Product : new()
        {
            return Define(factoryName, null as Action<Product>);
        }

        public static SimpleFactory<Product> Define<Product>(Action<Product> action) where Product : new()
        {
            return Define(null as string, action);
        }

        public static SimpleFactory<Product> Define<Product>(string factoryName, Action<Product> action) where Product : new()
        {
            SimpleFactory<Product> factory = new SimpleFactory<Product>(action);
            factories.Add<Product>(factory, factoryName);
            return factory;
        }

        #endregion

        #region Define Constructor Factory

        public static ConstructorFactory<Product> Define<Product>(Func<Product> action) where Product : new()
        {
            return Define<Product>(null, action);
        }

        public static ConstructorFactory<Product> Define<Product>(string factoryName, Func<Product> action) where Product : new()
        {
            ConstructorFactory<Product> factory = new ConstructorFactory<Product>(action);
            factories.Add<Product>(factory, factoryName);
            return factory;
        }

        #endregion

        #region Define Chained Factory

        public static IFactory<Product> Define<Product>(string factoryName, IFactory<Product> parentFactory, Action<Product> action)
        {
            ChainedFactory<Product> factory = new ChainedFactory<Product>(parentFactory, action);
            factories.Add<Product>(factory, factoryName);
            return factory;
        }

        #endregion

        #region Create

        public static Product Create<Product>()
        {
            return Create<Product>(null, null);
        }

        public static Product Create<Product>(Action<Product> customBuilder)
        {
            return Create<Product>(null, customBuilder);
        }

        public static Product Create<Product>(string factoryName)
        {
            return Create<Product>(factoryName, null);
        }

        public static Product Create<Product>(string factoryName, Action<Product> customBuilder)
        {
            Product product = factories.Get<Product>(factoryName).Create();
            if (customBuilder != null) customBuilder.Invoke(product);

            return product;
        }

        public static IList<Product> Create<Product>(int count)
        {
            return Create<Product>(count, null, null);
        }

        public static IList<Product> Create<Product>(int count, string factoryName)
        {
            return Create<Product>(count, factoryName, null);
        }

        public static IList<Product> Create<Product>(int count, Action<Product> customBuilder)
        {
            return Create<Product>(count, null, customBuilder);
        }

        public static IList<Product> Create<Product>(int count, string factoryName, Action<Product> customBuilder)
        {
            return BuildList(count, () => { return Presto.Create<Product>(factoryName, customBuilder); });
        }

        private static IList<Product> BuildList<Product>(int count, Func<Product> function)
        {
            IList<Product> list = new List<Product>();

            while (list.Count < count)
            {
                list.Add(function.Invoke());
            }

            return list;
        }


        #endregion

        #region Persist

        public static Action<object> PersistAction { get; set; }

        public static Product Persist<Product>()
        {
            return Persist<Product>(null, null);
        }

        public static Product Persist<Product>(string factoryName)
        {
            return Persist<Product>(factoryName, null);
        }

        public static Product Persist<Product>(Action<Product> customAction)
        {
            return Persist<Product>(null, customAction);
        }

        public static Product Persist<Product>(string factoryName, Action<Product> customAction)
        {
            Product product = Create<Product>(factoryName, customAction);
            PersistAction.Invoke(product);
            return product;
        }

        public static IList<Product> Persist<Product>(int count)
        {
            return Persist<Product>(count, null, null);
        }

        public static IList<Product> Persist<Product>(int count, string factoryName)
        {
            return Persist<Product>(count, factoryName, null);
        }

        public static IList<Product> Persist<Product>(int count, Action<Product> customAction)
        {
            return Persist<Product>(count, null, customAction);
        }

        public static IList<Product> Persist<Product>(int count, string factoryName, Action<Product> customAction)
        {
            return BuildList(count, () => { return Presto.Persist<Product>(factoryName, customAction); });
        }

        #endregion

        #region Create or Persist

        public static Product CreateOrPersist<Product>()
        {
            return CreateOrPersist<Product>(null, null);
        }

        public static Product CreateOrPersist<Product>(string factoryName)
        {
            return CreateOrPersist<Product>(factoryName, null);
        }

        public static Product CreateOrPersist<Product>(Action<Product> customAction)
        {
            return CreateOrPersist<Product>(null, customAction);
        }

        public static Product CreateOrPersist<Product>(string factoryName, Action<Product> customAction)
        {
            StackFrame[] frames = new StackTrace().GetFrames();

            foreach (StackFrame frame in frames)
            {
                MethodBase method = frame.GetMethod();

                if (method.Name == "Persist" && method.ReflectedType == typeof(Presto))
                {
                    return Persist<Product>(factoryName, customAction);
                }
            }

            return Create<Product>(factoryName, customAction);
        }

        public static IList<Product> CreateOrPersist<Product>(int count)
        {
            return CreateOrPersist<Product>(count, null, null);
        }

        public static IList<Product> CreateOrPersist<Product>(int count, string factoryName)
        {
            return CreateOrPersist<Product>(count, factoryName, null);
        }

        public static IList<Product> CreateOrPersist<Product>(int count, Action<Product> customAction)
        {
            return CreateOrPersist<Product>(count, null, customAction);
        }

        public static IList<Product> CreateOrPersist<Product>(int count, string factoryName, Action<Product> customAction)
        {
            return BuildList(count, () => { return Presto.CreateOrPersist<Product>(factoryName, customAction); });
        }

        #endregion

        public static void Clear()
        {
            factories.Clear();
            sequences.Clear();
        }
    }    
}
