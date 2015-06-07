using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ManyWindows
{
    class Symbol
    {
        private int _x;
        private int _y;
        private int _top = int.MaxValue;
        private int _buttom = 0;
        private int _left = int.MaxValue;
        private int _right = 0;
        public bool isEmpty = true;

        public int CenterX { get { return _x; } }
        public int CenterY { get { return _y; } }
        public int Width { get { return _right - _left; } }
        public int Height { get { return _buttom - _top; } }
        public int Top { get { return _top; } }
        public int Buttom { get { return _buttom; } }
        public int Left { get { return _left; } }
        public int Rigt { get { return _right; } }

        public Bitmap Pattern { get; set; }

        public Symbol(){}

        public int[] GetVector()
        {
            return Mat.GetVector(Pattern);
        }

        private Bitmap LeadToThePattern2(Bitmap image)
        {
            if (image.Height < 5 && image.Width < 5)
                return new Bitmap(16, 16);

            Bitmap tmpFile = new Bitmap(image, new Size(16, 16));
            return tmpFile;
        }

        private Bitmap LeadToThePattern(Bitmap image)
        {
            if (image.Height < 5 && image.Width < 5)
                return new Bitmap(16, 16);
            
            int newWidth = 16;
            int newHeight = 16;

            if (image.Height > image.Width)
                newWidth = (int)(image.Width / ((float)image.Height / 16));
            
            if (image.Height < image.Width)
                newHeight = (int)(image.Height / ((float)image.Width / 16));

            Bitmap newImage = new Bitmap(image, new Size(newWidth, newHeight));

            Bitmap tmpFile = new Bitmap(16, 16);
            Graphics gr = Graphics.FromImage(tmpFile);
            gr.Clear(Color.White);
            gr.DrawImage(newImage, new Point((16 - newWidth) / 2, 0));

            return tmpFile;
        }

        public void CutSymbol(ref Bitmap image)
        {
            RectangleF cloneRect = new RectangleF(_left, _top, _right - _left + 2, _buttom - _top + 2);
            System.Drawing.Imaging.PixelFormat format =
                image.PixelFormat;
            Bitmap cloneBitmap = image.Clone(cloneRect, format);

            var img = LeadToThePattern(cloneBitmap);
            Filter.Monochrome(ref img, 200);
            Pattern = img;
        }

        public void AddBlackPoint(int x, int y)
        {
            if (y > _buttom)
                _buttom = y;
            if (y < _top)
                _top = y;
            if (x < _left)
                _left = x;
            if (x > _right)
                _right = x;
            isEmpty = false;
            CountCenter();
        }

        private void CountCenter()
        {
            _x = (_left + _right) / 2;
            _y = (_top + _buttom) / 2;
        }
    }
}
