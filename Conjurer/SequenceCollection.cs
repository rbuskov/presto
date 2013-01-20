using System;
using System.Collections.Generic;
using System.Linq;

namespace Conjurer
{
    public class SequenceCollection
    {
        private Dictionary<string, Sequence> sequences = new Dictionary<string, Sequence>();

        private Sequence defaultSequence = new Sequence();

        public int Next()
        {
            return defaultSequence.Next;
        }

        public int Next(string name)
        {
            if (!sequences.ContainsKey(name)) throw new MissingSequenceException(name);

            return sequences[name].Next;
        }

        public int Next<T>()
        {
            return Next<T>(null);
        }

        public int Next<T>(string name)
        {
            return Next(GetKey<T>(name));
        }

        public Sequence Add(string name)
        {
            return Add(name, 0);
        }

        public Sequence Add(string name, int seed)
        {
            if (sequences.ContainsKey(name)) throw new DuplicateSequenceException(name);

            Sequence sequence = new Sequence(seed);
            sequences.Add(name, sequence);
            return sequence;
        }

        public Sequence Add<T>()
        {
            return Add<T>(null);
        }

        public Sequence Add<T>(int seed)
        {
            return Add<T>(null, seed);
        }

        public Sequence Add<T>(string name)
        {
            return Add<T>(name, 0);
        }

        public Sequence Add<T>(string name, int seed)
        {
            return Add(GetKey<T>(name), seed);
        }

        public bool Exists(string name)
        {
            return sequences.Keys.Contains(name);
        }

        public void Reset()
        {
            defaultSequence.Reset();
            foreach (Sequence sequence in sequences.Values)
            {
                sequence.Reset();
            }
        }

        public void Clear()
        {
            sequences.Clear();
            Reset();
        }

        private string GetKey<T>(string name)
        {
            string key = typeof(T).FullName;
            if (name != null)
            {
                key += "_" + name;
            }
            return key;
        }
    }
}
