using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ManyWindows
{
    class Recognizer
    {
        private KohonenNetwork _kn;
        private TextDetector _td;

        private double[][] _matchingPercents;

        public Recognizer(KohonenNetwork kn, TextDetector td)
        {
            _kn = kn;
            _td = td;
            StudyNetwork();
        }

        public List<Symbol> GetSymbols()
        {
            return _td.Symbols;
        }

        public double[][] GetMatchingPercents()
        {
            return _matchingPercents;
        }

        public string Parse(Bitmap image)
        {
            Filter.Median3x3(ref image);
            Filter.Monochrome(ref image, 100);
            

            image.Save(@"C:\xampp\One.bmp");
            _td.Detect(image);
            var vectros = _td.GetVectors();
            var message = String.Empty;

            _matchingPercents = new double[vectros.Count][];
            int i = 0;
            int temp;
            foreach (var vector in vectros)
                if (Mat.isSpaceVector(vector))
                    message += ' ';
                else if (Mat.isEnterVector(vector))
                    message += '\n';
                else
                {
                    temp = _kn.Parse(vector);
                    message += (char)(temp + 'A');
                    _matchingPercents[i++] = _kn.MatchingPercents;
                }

            PrintChars();
            return message;
        }

        public void PrintChars()
        {
            int name = 1;
            foreach (var symbol in _td.Symbols)
            {
                symbol.Pattern.Save(@"text\" + name.ToString() + ".bmp");
                name++;
               
            }
        }

        private void StudyNetwork()
        {
            for (var i = 0; i < 10; i++)
            {
                LearnFont("Arial16x16(photo)changed");
                LearnFont("Arial16x16(50)");
                LearnFont("Arial16x16photo");
                LearnFont("Arial16x16(photo)");
                LearnFont("Arial16x16");
            }
               
        }

        private void LearnFont(string Font)
        {
            Bitmap bmp;
            int[] vector;
            char symbol;

            _kn._fonts++;

            for (var i = 0; i < 26; i++)
            {
                symbol = (char)('A' + i); 
                bmp = new Bitmap(Bitmap.FromFile(Font + @"\" + symbol + ".bmp"));
                vector = Mat.GetVector(bmp);
                _kn.Study(vector, i);
            }
        }

        public void SetKohonenNetwork(KohonenNetwork kn)
        {
            _kn = kn;
        }

        public void SetTextDetector(TextDetector td)
        {
            _td = td;
        }
    }
}
