using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyWindows
{
    class KohonenNetwork
    {
        private readonly Input[] _inputs;
        private readonly Neuron[] _neurons;
        public static readonly int _alphabetLen = 26;
        private readonly int _pixelsNumber;
        public int _fonts = 0;

        public double[] MatchingPercents;

        public KohonenNetwork()
        {
            _pixelsNumber = 16 * 16;
            _inputs = new Input[_pixelsNumber];
            _neurons = new Neuron[_alphabetLen];
            FieldInputs();
            FieldNeurons();
            CreateNetwork();
            SetZeroExcitations();
        }

        private void FieldInputs()
        {
            for (var i = 0; i < _pixelsNumber; i++)
                _inputs[i] = new Input(_alphabetLen);
        }

        private void FieldNeurons()
        {
            for (var i = 0; i < _alphabetLen; i++)
                _neurons[i] = new Neuron(_pixelsNumber);
        }

        private void CreateNetwork()
        {
            for (var j = 0; j < _pixelsNumber; j++)
            {
                for (var i = 0; i < _alphabetLen; i++)
                {
                    var link = new Link(_neurons[i]);
                    link.Weight = 1 / Math.Sqrt(_pixelsNumber);
                    _inputs[j].OutgoingLinks[i] = link;
                    _neurons[i].IncomingLinks[j] = link;
                }
            }
        }

        public int  Parse(int[] input)
        {
            var normalizedVector = Mat.NormaizeVector(input);
            for (var i = 0; i < _inputs.Length; i++)
            {
                var inputNeuron = _inputs[i];
                foreach (var outgoingLink in inputNeuron.OutgoingLinks)
                {
                    outgoingLink.Neuron.Power += outgoingLink.Weight * normalizedVector[i];
                }
            }
            var maxIndex = FindNeuronWithMaxExcitation();
            SetMatchingPercents();
            SetZeroExcitations();

            return maxIndex;
        }

        private int FindNeuronWithMaxExcitation()
        {
            var maxIndex = 0;
            for (var i = 1; i < _neurons.Length; i++)
            {
                if (_neurons[i].Power > _neurons[maxIndex].Power)
                    maxIndex = i;
            }
            if (maxIndex == 'T' - 'A')
                maxIndex = isFirstorSecond('T', 'I');
            if (maxIndex == 'B' - 'A')
                maxIndex = isFirstorSecond('B', 'E');
            if (maxIndex == 'Q' - 'A')
                maxIndex = isFirstorSecond('Q', 'O');
            return maxIndex;
        }

        private int isFirstorSecond(char first, char second)
        {
            var tmp = _neurons[first - 'A'].NormalizedMaxExcitation();
                      
                if (_neurons[first - 'A'].Power / tmp > 0.7)
                    return first - 'A';
                else
                    return second - 'A';
            
        }

        private int FindNeuronWithMaxMatchingPercent()
        {
            var maxIndex = 0;
            for (var i = 1; i < _neurons.Length; i++)
            {
                if (_neurons[i].Power / _neurons[i].MaxExcitation()
                    > _neurons[maxIndex].Power / _neurons[maxIndex].MaxExcitation())
                    maxIndex = i;
            }
            return maxIndex;
        }

        private void SetZeroExcitations()
        {
            foreach (var outputNeuron in _neurons)
            {
                outputNeuron.Power = 0;
            }
        }

        public void Study(int[] input, int correctAnswer)
        {
            var neuron = _neurons[correctAnswer];
            var educationSpeed = neuron.EducationSpeed;
            var alpha = neuron.Alpha;
            var normalizedVector = NormalizeStudyVector(input, alpha);

            for (var i = 0; i < neuron.IncomingLinks.Length; i++)
            {
                var incomingLink = neuron.IncomingLinks[i];
                incomingLink.Weight = incomingLink.Weight + educationSpeed * (normalizedVector[i] - incomingLink.Weight);

            }
        }

        private double[] NormalizeStudyVector(int[] input, double alpha)
        {
            double[] normalizedVector = new double[input.Length];
            int n = input.Length;

            for (var i = 0; i < input.Length; i++)
            {
                normalizedVector[i] = alpha * input[i] + (1 - alpha) / Math.Sqrt(n);
            }
            return normalizedVector;
        }

        private void SetMatchingPercents()
        {
            MatchingPercents = new double[_neurons.Length];
            for (var i = 0; i < _neurons.Length; i++)
            {
                MatchingPercents[i] = _neurons[i].Power / _neurons[i].MaxExcitation();
            }
        }
    }
}
