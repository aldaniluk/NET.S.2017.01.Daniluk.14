using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Linq.Expressions;

namespace Logic
{
    /// <summary>
    /// Class contains operations that are defined for square matrices.
    /// </summary>
    public static class MatrixOperation
    {
        /// <summary>
        /// Summarizes two matrices.
        /// </summary>
        /// <typeparam name="T">Type for substitution.</typeparam>
        /// <param name="lhs">One matrix to sum.</param>
        /// <param name="rhs">Second matrix to sum.</param>
        /// <returns>Matrix-result of summation.</returns>
        public static SquareMatrix<T> Sum<T>(SquareMatrix<T> lhs, SquareMatrix<T> rhs)
        {
            if (ReferenceEquals(lhs, null)) throw new ArgumentNullException($"{nameof(lhs)} is null.");
            if (ReferenceEquals(rhs, null)) throw new ArgumentNullException($"{nameof(rhs)} is null.");

            SquareMatrix<T> result = new SquareMatrix<T>(lhs.Size);

            try
            {
                for (int i = 1; i <= result.Size; i++)
                {
                    for (int j = 1; j <= result.Size; j++)
                    {
                        result.ChangeElement((dynamic)lhs[i, j] + (dynamic)rhs[i, j], i, j);
                    }
                }
            }
            catch(RuntimeBinderException)
            {
                throw new ArgumentException($"Operation '+' is not defined for type {nameof(T)}.");
            }

            return result;
        }
    }
}
