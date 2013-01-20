using System;

namespace Conjurer
{
    public class Sequence
    {
        private int value;
        private int seed;

        public Sequence() : this(0) { }

        public Sequence(int seed)
        {
            this.seed = seed;
            Reset();
        }

        public virtual int Next
        {
            get { return value++; }
        }

        public virtual void Reset()
        {
            value = seed;
        }
    }
}
