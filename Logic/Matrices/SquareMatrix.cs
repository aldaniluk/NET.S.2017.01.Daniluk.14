using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    /// <summary>
    /// Class for the representation of the square matrix.
    /// </summary>
    /// <typeparam name="T">Type for substitution.</typeparam>
    public class SquareMatrix<T> : AbstractMatrix<T>
    {
        #region fields
        private T[,] array;
        #endregion

        #region ctors
        /// <summary>
        /// Ctor without parameters.
        /// </summary>
        public SquareMatrix()
        {
            array = new T[Size, Size];
        }

        /// <summary>
        /// Ctor with parameter.
        /// </summary>
        /// <param name="size">Size of the square matrix.</param>
        public SquareMatrix(int size)
        {
            Size = size;
            array = new T[Size, Size];
        }

        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="size">Size of the square matrix.</param>
        /// <param name="inputArray">Collection to fill square matrix.</param>
        public SquareMatrix(int size, IEnumerable<T> inputArray) : this(size)
        {
            FillMatrix(inputArray);
        }
        #endregion

        #region private & protected methods
        private void FillMatrix(IEnumerable<T> inputArray)
        {
            if (inputArray == null) throw new ArgumentNullException($"{nameof(inputArray)} is null.");

            int inputArrayIndex = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (inputArrayIndex < inputArray.Count())
                        array[i, j] = inputArray.ElementAt(inputArrayIndex);
                    inputArrayIndex++;
                }
            }
        }

        protected override T GetElement(int i, int j)
        {
            CheckIndexes(i, j);
            return array[i, j];
        }

        protected override void SetElement(T element, int i, int j)
        {
            CheckIndexes(i, j);
            if (ReferenceEquals(element, null)) throw new ArgumentNullException($"{nameof(element)} is null.");
            array[i, j] = element;
        }
        #endregion
    }
}
