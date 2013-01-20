using System;

namespace Conjurer
{
    public class DuplicateFactoryException : Exception
    {
        public DuplicateFactoryException(string key) 
            : base(string.Format("A factory already exists in the collection for {0}", key))
        {
        }
    }
}
