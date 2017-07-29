using NUnit.Framework;
using System;
using Logic;

namespace Logic.Tests
{
    [TestFixture]
    public class MatrixOperationTest
    {
        [Test]
        public void Sum_IntInputValues_PositiveTest()
        {
            SquareMatrix<int> matr = new SquareMatrix<int>(2, new int[] { 1, 2, 3, 4 });
            SquareMatrix<int> smatr = new SymmetricMatrix<int>(2, new int[] {1,0,0,1});
            SquareMatrix<int> actualResult = MatrixOperation.Sum<int>(matr, smatr);
            SquareMatrix<int> expectedResult = new SquareMatrix<int>(2, new int[] { 2, 2, 3, 5 });
            Assert.IsTrue(actualResult.Equals(expectedResult));
        }

        [Test]
        public void Sum_OperationPlusIsNotDefinedForType_TrowArgumentException()
        {
            SquareMatrix<object> matr = new SquareMatrix<object>(2, new object[] { new object() });
            SquareMatrix<object> smatr = new SquareMatrix<object>(2, new object[] { new object() });
            Assert.Throws<ArgumentException>(() => MatrixOperation.Sum<object>(matr, smatr));
        }

        [Test]
        public void Sum_LeftMatrixIsNull_TrowArgumentNullException()
        {
            SquareMatrix<int> matr = new SquareMatrix<int>(2, new int[] { 1 });
            Assert.Throws<ArgumentNullException>(() => MatrixOperation.Sum<int>(null, matr));
        }

        [Test]
        public void Sum_RightMatrixIsNull_TrowArgumentNullException()
        {
            SquareMatrix<int> matr = new SquareMatrix<int>(2, new int[] { 1 });
            Assert.Throws<ArgumentNullException>(() => MatrixOperation.Sum<int>(matr, null));
        }

        [Test]
        public void Sum_MatricesHaveDifferentSizes_TrowArgumentException()
        {
            SquareMatrix<int> matr = new SquareMatrix<int>(3, new int[] { 1 });
            SquareMatrix<int> smatr = new SquareMatrix<int>(2, new int[] { 2 });
            Assert.Throws<ArgumentException>(() => MatrixOperation.Sum<int>(matr, smatr));
        }
    }
}
