using System;
using System.Collections.Generic;

namespace Logic.Tests
{
    internal class MatrixComparer<T> : IEqualityComparer<AbstractMatrix<T>>
    {
        public bool Equals(AbstractMatrix<T> x, AbstractMatrix<T> y)
        {
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;

            if (ReferenceEquals(x, y)) return true;
            if (x.Size != y.Size) return false;

            for (int i = 1; i <= x.Size; i++)
            {
                for (int j = 1; j <= x.Size; j++)
                {
                    if (!x[i, j].Equals(y[i, j])) return false;
                }
            }
            return true;
        }

        public int GetHashCode(AbstractMatrix<T> obj)
        {
            throw new NotImplementedException();
        }
    }
}
