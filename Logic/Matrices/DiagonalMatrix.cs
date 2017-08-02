using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    /// <summary>
    /// Class for the representation of the diagonal matrix.
    /// </summary>
    /// <typeparam name="T">Type for substitution.</typeparam>
    public class DiagonalMatrix<T> : AbstractMatrix<T>
    {
        #region fields
        private T[] array;
        #endregion

        #region ctors
        /// <summary>
        /// Ctor without parameters.
        /// </summary>
        public DiagonalMatrix()
        {
            array = new T[Size];
        }

        /// <summary>
        /// Ctor with parameter.
        /// </summary>
        /// <param name="size">Size of the diagonal matrix.</param>
        public DiagonalMatrix(int size)
        {
            Size = size;
            array = new T[Size];
        }

        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="size">Size of the diagonal matrix.</param>
        /// <param name="inputArray">Collection to fill diagonal matrix.</param>
        public DiagonalMatrix(int size, IEnumerable<T> inputArray) : this(size)
        {
            FillMatrix(inputArray);
        }
        #endregion

        #region private & protected methods
        private void FillMatrix(IEnumerable<T> inputArray)
        {
            if (inputArray == null) throw new ArgumentNullException($"{nameof(inputArray)} is null.");

            int inputArrayIndex = 0;
            for (int i = 0; i < Size; i++)
            {
                if (inputArrayIndex < inputArray.Count())
                    array[i] = inputArray.ElementAt(inputArrayIndex);
                inputArrayIndex++;
            }
        }

        protected override T GetElement(int i, int j)
        {
            CheckIndexes(i, j);
            return (i != j) ? default(T) : array[i];
        }

        protected override void SetElement(T element, int i, int j)
        {
            if (i != j) throw new ArgumentException("You can't change element which is not on the diagonal.");
            CheckIndexes(i, j);
            if (ReferenceEquals(element, null)) throw new ArgumentNullException($"{nameof(element)} is null.");
            array[i] = element;
        }
        #endregion
    }
}
