using NUnit.Framework;
using System;

namespace Logic.Tests
{
    [TestFixture]
    class MatrixTest
    {
        [Test]
        public void NewSquareMatrix_NotSuitableSize_TrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new DiagonalMatrix<int>(0, new int[] { 1 }));
        }

        [Test]
        public void NewSquareMatrix_NotSuitableSizeLessThan0_TrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new DiagonalMatrix<int>(-1, new int[] { 1 }));
        }

        [Test]
        public void NewSquareMatrix_NullInputCollection_TrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new DiagonalMatrix<int>(2, null));
        }
    }
}
