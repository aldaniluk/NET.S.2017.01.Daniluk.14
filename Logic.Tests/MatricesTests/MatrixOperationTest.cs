using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Logic.Tests
{
    [TestFixture]
    public class MatrixOperationTest
    {
        [Test]
        public void Sum_IntInputValues_PositiveTest()
        {
            AbstractMatrix<int> matr = new SquareMatrix<int>(2, new int[] { 1, 2, 3, 4 });
            AbstractMatrix<int> smatr = new SymmetricMatrix<int>(2, new int[] {1, 1});
            AbstractMatrix<int> actualResult = matr.Sum<int>(smatr);
            AbstractMatrix<int> expectedResult = new SquareMatrix<int>(2, new int[] { 2, 3, 4, 4 });
            Assert.IsTrue((new MatrixComparer<int>()).Equals(actualResult, expectedResult));
        }

        [Test]
        public void Sum_SquarePlusSquareEqualsDiagonal_PositiveTest()
        {
            AbstractMatrix<int> matr = new SquareMatrix<int>(2, new int[] { 1, 2, 3, 4 });
            AbstractMatrix<int> smatr = new SquareMatrix<int>(2, new int[] { 1, -2, -3, 4 });
            AbstractMatrix<int> actualResult = matr.Sum<int>(smatr);
            Assert.IsTrue(actualResult.GetType().ToString() == "Logic.DiagonalMatrix`1[System.Int32]");
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
