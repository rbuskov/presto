using System;

namespace Conjurer.Test
{
    public class Rabbit
    {
        public Rabbit() 
        { }

        public Rabbit(string name) 
        {
            this.Name = name;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public Hat Hat { get; set; }
    }
}
