using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyWindows
{
    class Link
    {
        public Neuron Neuron;
        public double Weight;

        public Link(Neuron neuron)
        {
            Neuron = neuron;
            Weight = 0;
        }
    }
}
