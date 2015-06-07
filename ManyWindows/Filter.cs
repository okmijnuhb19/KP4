using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ManyWindows
{
    class Filter
    {
        public static void Median3x3(ref Bitmap image)
        {
            int[] dx = {0, 1, 1, 0, -1, -1, 1, -1, 0};
            int[] dy = {1, 0, 1, -1, 0, 1, -1, -1, 0};
            var arrR = new int[9];
            var arrG = new int[9];
            var arrB = new int[9];
            var outImage = new Bitmap(image);

            for (int i = 1; i < image.Width - 1; i++)
                for (int j = 1; j < image.Height - 1; j++)
                {
                    for (int i1 = 0; i1 < 9; i1++)
                    {
                        var point = image.GetPixel(i + dx[i1], j + dy[i1]);

                        arrR[i1] = point.R;
                        arrG[i1] = point.G;
                        arrB[i1] = point.B;
                    }
                    Array.Sort(arrR);
                    Array.Sort(arrG);
                    Array.Sort(arrB);

                    outImage.SetPixel(i, j, Color.FromArgb(arrR[4], arrG[4], arrB[4]));
                }

            image = outImage;
        }

        public static void Median2x2(ref Bitmap image)
        {
            int[] dx = { 0, 1, 0, -1, 0 };
            int[] dy = { 1, 0, -1, 0, 0 };
            var arrR = new int[5];
            var arrG = new int[5];
            var arrB = new int[5];
            var outImage = new Bitmap(image);

            for (int i = 1; i < image.Width - 1; i++)
                for (int j = 1; j < image.Height - 1; j++)
                {
                    for (int i1 = 0; i1 < 5; i1++)
                    {
                        var point = image.GetPixel(i + dx[i1], j + dy[i1]);

                        arrR[i1] = point.R;
                        arrG[i1] = point.G;
                        arrB[i1] = point.B;
                    }
                    Array.Sort(arrR);
                    Array.Sort(arrG);
                    Array.Sort(arrB);

                    outImage.SetPixel(i, j, Color.FromArgb(arrR[2], arrG[2], arrB[2]));
                }

            image = outImage;
        }

        public static void Monochrome(ref Bitmap image, int level)
        {
            for (int j = 0; j < image.Height; j++)
            {
                for (int i = 0; i < image.Width; i++)
                {
                    var color = image.GetPixel(i, j);
                    int sr = (color.R + color.G + color.B) / 3;
                    image.SetPixel(i, j, (sr < level ? Color.Black : Color.White));
                }
            }
        }


    }
}
