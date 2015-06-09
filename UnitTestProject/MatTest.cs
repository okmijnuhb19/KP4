using System;
using ManyWindows;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class MatTest
    {
        [TestMethod]
        public void TestCalculatingVectorLengthInt()
        {
            int[] vector=new int[3];
            for (int i=0;i<3;i++) 
            {
                vector[i] = i+1;
            }
            double d;
            double answer = Math.Sqrt(14);
            d=Mat.CalculateVectorLength(vector);
            Assert.AreEqual(answer, d, 0.001, "Length not calculeted correctly");
        }
        [TestMethod]
        public void TestCalculatingVectorLengthDouble()
        {
            double[] vector=new double[3];
            for (int i=0;i<3;i++) 
            {
                vector[i] = i+1;
            }
            double d;
            double answer = Math.Sqrt(14);
            d=Mat.CalculateVectorLength(vector);
            Assert.AreEqual(answer, d, 0.001, "Length not calculeted correctly");
        }
        [TestMethod]
        public void TestIsBlack() 
        {
            Color color = Color.Black;
            bool answer=true;
            bool b;
            b=Mat.isBlack(color);
            Assert.AreEqual(answer, b,"Length not calculeted correctly");
        }
        [TestMethod]
        public void TestNormalizeVector()
        {
            int[] vector = new int[3];
            for (int i = 0; i < 3; i++)
            {
                vector[i] = i + 1;
            }
            double[] d = Mat.NormaizeVector(vector);
            double[] answer = new double[3];
            for (int i = 0; i < 3; i++)
            {
                answer[i] = (i + 1) / Math.Sqrt(14);
            }
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(answer[i], d[i], "Vector not normalized correctly");
            }
        }
        [TestMethod]
        public void TestGetMatrix() 
        {
            Bitmap bmp = new Bitmap(3, 3);
            int[][] resMatrix = Mat.GetMatrix(bmp);
            int[][] matrix = new int[3][];
            for (var i = 0; i < 3; i++)
            {
                matrix[i] = new int[3];
                for (var j = 0; j < 3; j++)
                    matrix[i][j] = 1;
            }
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++) 
                {
                    Assert.AreEqual(resMatrix[i][j], matrix[i][j], "Matrix not got correctly");
                }                   
            }

        }
        [TestMethod]
        public void TestGetFullVector() 
        {
            var vector = Mat.GetFullVector();
            for (int i = 0; i < vector.Length; i++)
            {
                Assert.AreEqual(vector[i], 1, "Vector not got correctly");
            }
        }
        [TestMethod]
        public void TestGetEmptyVector()
        {
            var vector = Mat.GetEmptyVector();
            for (int i = 0; i < vector.Length; i++)
            {
                Assert.AreEqual(vector[i], 0, "Vector not got correctly");
            }
        }
        [TestMethod]
        public void TestGetVector()
        {
            Bitmap bmp = new Bitmap(16,16);
            var vector = Mat.GetFullVector();
            var fullvector = Mat.GetVector(bmp);
            for (int i = 0; i < vector.Length; i++)
            {
                Assert.AreEqual(vector[i], fullvector[i], "Vector not got correctly");
            }
        }
        [TestMethod]
        public void TestIsMethods() 
        {
            bool[] b = new bool[2];
            var fullvector = Mat.GetFullVector();
            var emptyvector = Mat.GetEmptyVector();
            b[0] = Mat.isEmptyVector(fullvector);
            b[1] = Mat.isFullVector(emptyvector);
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(b[i], false, "Is methods work not correctly");
            }
        }
    }
}
