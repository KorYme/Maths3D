using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests13_Determinants
    {
        [Test, DefaultFloatingPointTolerance(0.001d)]
        public void TestDeterminantMatrix2x2()
        {
            Matrix<float> m = new Matrix<float>(new[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            });

            float determinant = Matrix<float>.Determinant(m);

            Assert.AreEqual(-2f, determinant);
        }

        [Test, DefaultFloatingPointTolerance(0.001d)]
        public void TestDeterminantMatrix3x3()
        {
            Matrix<float> m = new Matrix<float>(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f },
            });

            float determinant = Matrix<float>.Determinant(m);

            Assert.AreEqual(0f, determinant);
        }
        
        [Test, DefaultFloatingPointTolerance(0.01d)]
        public void TestDeterminantMatrix4x4()
        {
            Matrix<float> m = new Matrix<float>(new[,]
            {
                { 0.707f, 2.449f, 4.243f, 1.000f },
                { 0.707f, 2.449f, -4.243f, 2.000f },
                { -1.732f, 2.000f, 0.000f, 3.000f },
                { 0.000f, 0.000f, 0.000f, 1.000f },
            });

            float determinant = Matrix<float>.Determinant(m);

            Assert.AreEqual(48f, determinant);
        }
        
        [Test, DefaultFloatingPointTolerance(0.001d)]
        public void TestDeterminantIdentityMatrices()
        {
            //Identity 2
            Matrix<float> identity2 = Matrix<float>.Identity(2);
            float determinantIdentity2 = Matrix<float>.Determinant(identity2);
            Assert.AreEqual(1f, determinantIdentity2);
            
            //Identity 3
            Matrix<float> identity3 = Matrix<float>.Identity(3);
            float determinantIdentity3 = Matrix<float>.Determinant(identity3);
            Assert.AreEqual(1f, determinantIdentity3);
            
            //Identity 10
            Matrix<float> identity10 = Matrix<float>.Identity(10);
            float determinantIdentity10 = Matrix<float>.Determinant(identity10);
            Assert.AreEqual(1f, determinantIdentity10);
        }
    }
}