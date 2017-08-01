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
        public static AbstractMatrix<T> Sum<T>(this AbstractMatrix<T> lhs, AbstractMatrix<T> rhs)
        {
            if (ReferenceEquals(lhs, null)) throw new ArgumentNullException($"{nameof(lhs)} is null.");
            if (ReferenceEquals(rhs, null)) throw new ArgumentNullException($"{nameof(rhs)} is null.");
            if (lhs.Size != rhs.Size) throw new ArgumentException($"{nameof(lhs)} and {nameof(rhs)} have different sizes and can't be summarized.");

            T[,] result = new T[lhs.Size, lhs.Size];
            try
            {
                for (int i = 1; i <= result.GetLength(0); i++)
                {
                    for (int j = 1; j <= result.GetLength(1); j++)
                    {
                        result[i-1,j-1] = (dynamic)lhs[i, j] + rhs[i, j];
                    }
                }
            }
            catch(RuntimeBinderException)
            {
                throw new ArgumentException($"Operation '+' is not defined for type {nameof(T)}.");
            }

            return MatrixFactory.Create<T>(lhs.Size, result);
        }
    }
}
