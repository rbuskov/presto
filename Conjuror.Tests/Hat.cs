using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conjurer.Tests
{
    public class Hat
    {
        public IList<Rabbit> Rabbits { get; set; }

        public Hat()
        {
            Rabbits = new List<Rabbit>();
        }
    }
}
