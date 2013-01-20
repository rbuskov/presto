using System;

namespace Conjurer
{
    public class MissingSequenceException : Exception
    {
        public MissingSequenceException(string key) 
            : base(string.Format("The sequence {0} does not exist.", key))
        {
        }
    }
}
