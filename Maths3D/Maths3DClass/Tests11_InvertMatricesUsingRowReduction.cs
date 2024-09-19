﻿using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests11_InvertMatricesUsingRowReduction
    {
        [Test, DefaultFloatingPointTolerance(0.001d)]
        public void TestInvertMatrixInstance()
        {
            Matrix<float> m = new Matrix<float>(new[,]
            {
                { 2f, 3f, 8f },
                { 6f, 0f, -3f },
                { -1f, 3f, 2f },
            });

            Matrix<float> mInverted = m.InvertByRowReduction();

            Assert.AreEqual(new[,]
            {
                { 0.066f, 0.133f, -0.066f },
                { -0.066f, 0.088f, 0.4f },
                { 0.133f, -0.066f, -0.133f }
            }, mInverted.ToArray2D());
        }

        [Test, DefaultFloatingPointTolerance(0.001d)]
        public void TestInvertMatrixStatic()
        {
            Matrix<float> m = new Matrix<float>(new[,]
            {
                { 1f, 2f },
                { 3f, 4f },
            });

            Matrix<float> mInverted = Matrix<float>.InvertByRowReduction(m);

            Assert.AreEqual(new[,]
            {
                { -2f, 1f },
                { 1.5f, -0.5f },
            }, mInverted.ToArray2D());
        }

        [Test]
        public void TestInvertImpossibleMatrix()
        {
            Matrix<float> m = new Matrix<float>(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f },
            });

            Assert.Throws<MatrixInvertException>(() =>
            {
                Matrix<float> mInverted = m.InvertByRowReduction();
            });
        }
    }
}