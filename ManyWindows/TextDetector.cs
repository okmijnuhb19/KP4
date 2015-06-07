using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ManyWindows
{
    class TextDetector
    {
        private Bitmap _image;
        private int[][] _matrix;
        private List<Symbol> _symbols;

        private int[] dx = {0, 1, 1, 0, -1, -1, 1, -1};
        private int[] dy = {1, 0, 1, -1, 0, 1, -1, -1};

        public List<Symbol> Symbols { get { return _symbols; } }

        public TextDetector() { }

        public void Detect(Bitmap image)
        {
            _image = image;
            _symbols = new List<Symbol>();
            _matrix = Mat.GetMatrix(_image);
            FindSymbols();
            SortSymbols();
        }

        public List<int[]> GetVectors()
        {
            List<int[]> vectors = new List<int[]>();
            for (int i = 0; i < _symbols.Count; i++)
                vectors.Add(_symbols[i].GetVector());

            CompleteTextOrganization(vectors);

            return vectors;
        }

        public void CompleteTextOrganization(List<int[]> vectors)
        {
            var enterVector = Mat.GetEnterVector();
            var spaceVector = Mat.GetSpaceVector();
            var deltaI = 0;

            int[] spaces = FindSpaces();
            int[] enters = FindLineCompletions();

            for (var i = 1; i < _symbols.Count; i++)
            {
                if (spaces[i] == 1)
                {
                    vectors.Insert(i + deltaI, spaceVector);
                    deltaI++;
                }

                if (enters[i] == 1)
                {
                    vectors.Insert(i + deltaI, enterVector);
                    deltaI++;
                }
            }
        }

        private int[] FindLineCompletions()
        {
            var result = new int[_symbols.Count];
            var deltaY = 0;

            for (int i = 1; i < _symbols.Count; i++)
            {
                deltaY = (_symbols[i - 1].Buttom - _symbols[i].Top);
                if (deltaY < 0)
                    result[i] = 1;
                else
                    result[i] = 0;
            }

            return result;
        }

        private int[] FindSpaces()
        {
            var result = new int[_symbols.Count];
            var spaces = new List<int>(){-1};
            var deltaY = 0;
            var deltaX = 0;
            for (var i = 1; i < _symbols.Count; i++)
            {
                deltaY = (_symbols[i - 1].Buttom - _symbols[i].Top);
                deltaX = _symbols[i].Left - _symbols[i - 1].Rigt;
                if (deltaY > 0)
                    spaces.Add(deltaX);
                else
                    spaces.Add(-1);
            }
            double avSpaceWidth = spaces.Max() / 2;

            for (var i = 1; i < _symbols.Count; i++)
            {
                deltaY = (_symbols[i - 1].Buttom - _symbols[i].Top);
                if (spaces[i] > avSpaceWidth && deltaY > 0)
                    result[i] = 1;
                else
                    result[i] = 0;
            }

            return result;
        }

        private void SortSymbols()
        {
            _symbols.Sort(delegate(Symbol x, Symbol y)
            {
                if (x.CenterY - y.CenterY > 10) return 1;
                if (x.CenterY - y.CenterY < -10) return -1;
                if (x.CenterX >= y.CenterX) return 1;
                if (x.CenterX < y.CenterX) return -1;
                    return 0;
            }
            );
        }

        private void FindSymbols()
        {
            Symbol newSymbol;
            for (int i = 0; i < _image.Height; i++)
                for (int j = 0; j < _image.Width; j++)
                    if (_matrix[i][j] == 1)
                    {
                        newSymbol  = new Symbol();
                        AbsorbSymbol(j, i, newSymbol);
                        if (!newSymbol.isEmpty)
                        _symbols.Add(newSymbol);
                    }
            CutSymblos();
        }

        private void AbsorbSymbol(int x, int y, Symbol symbol)
        {
            if (x < 0 || x >= _image.Width || y < 0 || y > _image.Height)
                return;
            if (_matrix[y][x] == 0)
                return;

            symbol.AddBlackPoint(x, y);
            _matrix[y][x] = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    AbsorbSymbol(x + dx[i], y + dy[j], symbol);
        }

        private void CutSymblos()
        {
            foreach (var symbol in _symbols)
                symbol.CutSymbol(ref _image);
        }
    }
}
