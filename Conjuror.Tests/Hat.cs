using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conjurer.Test
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
