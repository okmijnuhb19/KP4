using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ManyWindows
{
    public static class Mat
    {

        static public int[][] GetMatrix(Bitmap image)
        {
            int[][] matrix = new int[image.Height][];
            for (var i = 0; i < image.Height; i++)
            {
                matrix[i] = new int[image.Width];
                for (var j = 0; j < image.Width; j++)
                    if (isBlack(image.GetPixel(j, i)))
                        matrix[i][j] = 1;
                    else
                        matrix[i][j] = 0;
            }

            return matrix;
        }

        static public int[] GetVector(Bitmap image)
        {
            var vector = new int[16 * 16];
            for (var i = 0; i < 16; i++)
                for (var j = 0; j < 16; j++)
                    if (isBlack(image.GetPixel(j, i)))
                        vector[i * 16 + j] = 1;
                    else
                        vector[i * 16 + j] = 0;

            return vector;
        }

        static public int[] GetEmptyVector()
        {
            var vector = new int[16 * 16];
            for (var i = 0; i < 16; i++)
                for (var j = 0; j < 16; j++)
                        vector[i * 16 + j] = 0;

            return vector;
        }

        static public int[] GetFullVector()
        {
            var vector = new int[16 * 16];
            for (var i = 0; i < 16; i++)
                for (var j = 0; j < 16; j++)
                    vector[i * 16 + j] = 1;

            return vector;
        }

        static public int[] GetEnterVector()
        {
            return new int[1];
        }

        static public int[] GetSpaceVector()
        {
            return new int[2];
        }

        static public bool isEmptyVector(int[] vector)
        {
            var Empty = true;
            var i = 0;
            while (i < 16 * 16 && Empty)
                Empty = vector[i] == 0;

            return Empty;
        }

        static public bool isFullVector(int[] vector)
        {
            var Empty = true;
            var i = 0;
            while (i < 16 * 16 && Empty)
                Empty = vector[i] == 1;

            return Empty;
        }

        static public bool isSpaceVector(int[] vector)
        {
            return vector.Length == 2;
        }

        static public bool isEnterVector(int[] vector)
        {
            return vector.Length == 1;
        }

        static public bool isBlack(Color color)
        {
            if (color.R == 0 && color.G == 0 && color.B == 0)
                return true;
            else
                return false;
        }

        static public double CalculateVectorLength(int[] input)
        {
            double sumOfSquares = 0;
            for (var i = 0; i < input.Length; i++)
            {
                sumOfSquares += input[i] * input[i];
            }

            return Math.Sqrt(sumOfSquares);
        }

        static public double CalculateVectorLength(double[] input)
        {
            double sumOfSquares = 0;
            for (var i = 0; i < input.Length; i++)
            {
                sumOfSquares += input[i] * input[i];
            }

            return Math.Sqrt(sumOfSquares);
        }

        static public double[] NormaizeVector(int[] input)
        {
            double[] normalizedVector = new double[input.Length];
            var vectorLength = CalculateVectorLength(input);

            for (var i = 0; i < input.Length; i++)
            {
                normalizedVector[i] = input[i] / vectorLength;
            }
            return normalizedVector;
        }
    }
}
