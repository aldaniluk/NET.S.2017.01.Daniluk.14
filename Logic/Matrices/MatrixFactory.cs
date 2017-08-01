using System;

namespace Logic
{
    /// <summary>
    /// Creates different matrices based on the input values.
    /// </summary>
    public static class MatrixFactory
    {
        /// <summary>
        /// Creates new matrix based on the input values.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="size">Size of the square matrix.</param>
        /// <param name="values">Values to insert into new matrix.</param>
        /// <returns>New matrix of the appropriate type.</returns>
        public static AbstractMatrix<T> Create<T>(int size, T[,] values)
        {
            if (IsDiagonal<T>(values))
                return new DiagonalMatrix<T>(size, GetValuesForDiagonal<T>(size, values)); 
            else if (IsSymmetric<T>(values))
                return new SymmetricMatrix<T>(size, GetValuesForSymmetric<T>(size, values));
            else
                return new SquareMatrix<T>(size, GetValuesForSquare<T>(size, values));
        }

        #region gets matrix type
        private static bool IsDiagonal<T>(T[,] values)
        {
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    if (i != j && !values[i, j].Equals(default(T))) return false;
                }
            }
            return true;
        }

        private static bool IsSymmetric<T>(T[,] values)
        {
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    if (!values[i, j].Equals(values[j, i])) return false;
                }
            }
            return true;
        }
        #endregion

        #region gets values for insert in a suitable form
        private static T[] GetValuesForSquare<T>(int size, T[,] values)
        {
            T[] result = new T[size*size];
            int indexRes = 0;
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    result[indexRes++] = values[i, j];
                }
            }
            return result;
        }

        private static T[] GetValuesForSymmetric<T>(int size, T[,] values)
        {
            T[] result = new T[size * (size + 1) / 2];
            int indexRes = 0;
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    result[indexRes++] = values[i, j];
                }
            }
            return result;
        }

        private static T[] GetValuesForDiagonal<T>(int size, T[,] values)
        {
            T[] result = new T[size];
            int indexRes = 0;
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (i == j) result[indexRes++] = values[i, j];
                }
            }
            return result;
        }
        #endregion
    }
}
