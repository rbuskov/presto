using System;

namespace Conjurer
{
    public class MissingFactoryException : Exception
    {
        public MissingFactoryException(string key) 
            : base(string.Format("Failed to create instance: No factory is defined for {0}", key))
        {
        }
    }
}
