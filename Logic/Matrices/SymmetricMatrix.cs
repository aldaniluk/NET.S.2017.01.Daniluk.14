using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    /// <summary>
    /// Class for the representation of the symmetric matrix.
    /// </summary>
    /// <typeparam name="T">Type for substitution.</typeparam>
    public class SymmetricMatrix<T> : AbstractMatrix<T>
    {
        #region fields
        private T[][] array;
        #endregion

        #region ctors
        /// <summary>
        /// Ctor without parameter.
        /// </summary>
        public SymmetricMatrix()
        {
            array = new T[Size][];
            for(int i = 0; i < Size; i++)
            {
                array[i] = new T[i + 1];
            }
        }

        /// <summary>
        /// Ctor with parameter.
        /// </summary>
        /// <param name="size">Size of the symmetric matrix.</param>
        public SymmetricMatrix(int size)
        {
            Size = size;
            array = new T[Size][];
            for (int i = 0; i < Size; i++)
            {
                array[i] = new T[i + 1];
            }
        }

        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="size">Size of the symmetric matrix.</param>
        /// <param name="inputArray">Collection to fill symmetric matrix.</param>
        public SymmetricMatrix(int size, IEnumerable<T> inputArray) : this(size)
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
                for (int j = 0; j <= i; j++)
                {
                    if (inputArrayIndex < inputArray.Count())
                        array[i][j] = inputArray.ElementAt(inputArrayIndex);
                    inputArrayIndex++;
                }
            }
        }

        protected override T GetElement(int i, int j)
        {
            CheckIndexes(i, j);
            return (j <= i) ? array[i][j] : array[j][i];
        }

        protected override void SetElement(T element, int i, int j) //changes elements on (i,j) and (j,i)
        {
            CheckIndexes(i, j);
            if (ReferenceEquals(element, null)) throw new ArgumentNullException($"{nameof(element)} is null.");
            if (j <= i)
                array[i][j] = element;
            else
                array[j][i] = element;
        }
        #endregion
    }
}
