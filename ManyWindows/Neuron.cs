using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyWindows
{
    class Neuron
    {
        public Link[] IncomingLinks;
        public double Power { get; set; }
        private double _es = 0.79;
        private double _alpha = 0.001;
        public double Alpha { get { _alpha *= 1.28; return _alpha; } }
        public double EducationSpeed { get { _es *= 0.9; return _es; } }

        public Neuron(int LinksCount)
        {
            IncomingLinks = new Link[LinksCount];
            Power = 0;
        }

        public double MaxExcitation()
        {
            double exitation = 0;
            for (var i = 0; i < IncomingLinks.Length; i++)
                exitation += IncomingLinks[i].Weight;

            return exitation;
        }

        public double NormalizedMaxExcitation()
        {
            double excitation = MaxExcitation();
            var input = IncomingLinksToArray();
            return excitation / Mat.CalculateVectorLength(input);
        }

        private double[] IncomingLinksToArray()
        {
            var input = new double[IncomingLinks.Length];
            for (var i = 0; i < input.Length; i++)
            {
                input[i] = IncomingLinks[i].Weight;
            }
            return input;
        }
    }
}
