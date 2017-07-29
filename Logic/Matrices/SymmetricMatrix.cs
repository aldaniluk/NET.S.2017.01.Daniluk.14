using System;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Class for the representation of the symmetric matrix.
    /// </summary>
    /// <typeparam name="T">Type for substitution.</typeparam>
    public class SymmetricMatrix<T> : SquareMatrix<T>
    {
        #region ctors
        /// <summary>
        /// Ctor without parameter.
        /// </summary>
        public SymmetricMatrix() : base() { }

        /// <summary>
        /// Ctor with parameter.
        /// </summary>
        /// <param name="size">Size of the symmetric matrix.</param>
        public SymmetricMatrix(int size) : base(size) { }

        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="size">Size of the symmetric matrix.</param>
        /// <param name="inputArray">Collection to fill symmetric matrix.</param>
        public SymmetricMatrix(int size, IEnumerable<T> inputArray) : base(size, inputArray) { }
        #endregion

        /// <summary>
        /// Checks if matrix is symmetric.
        /// </summary>
        /// <returns>True, if matrix is symmetric, and false otherwise.</returns>
        protected override bool IsSuitableMatrix()
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (!array[i, j].Equals(array[j, i])) throw new ArgumentException("Matrix is not symmetric.");
                }
            }
            return true;
        }

        /// <summary>
        /// Reaction to an element change event. 
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="e">Object, that contains data about event.</param>
        public override void NewElementMessage(object sender, NewElementEventArgs e)
        {
            Console.WriteLine($"Symmetric matrix! Element was changed in {e.Message} at position ({e.I}, {e.J})");
        }
    }
}
