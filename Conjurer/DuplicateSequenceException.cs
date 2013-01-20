using System;

namespace Conjurer
{
    public class DuplicateSequenceException : Exception
    {
        public DuplicateSequenceException(string key) 
            : base(string.Format("The sequence {0} already exists.", key))
        {
        }
    }
}
