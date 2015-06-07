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
        public void TestCalculatingVectorLength()
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
    }
}
