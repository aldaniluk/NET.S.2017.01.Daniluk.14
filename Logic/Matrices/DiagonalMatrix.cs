using System;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Class for the representation of the diagonal matrix.
    /// </summary>
    /// <typeparam name="T">Type for substitution.</typeparam>
    public class DiagonalMatrix<T> : SquareMatrix<T>
    {
        #region ctors
        /// <summary>
        /// Ctor without parameters.
        /// </summary>
        public DiagonalMatrix() : base() { }

        /// <summary>
        /// Ctor with parameter.
        /// </summary>
        /// <param name="size">Size of the diagonal matrix.</param>
        public DiagonalMatrix(int size) : base(size) { }

        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="size">Size of the diagonal matrix.</param>
        /// <param name="inputArray">Collection to fill diagonal matrix.</param>
        public DiagonalMatrix(int size, IEnumerable<T> inputArray) : base(size, inputArray) { }
        #endregion

        /// <summary>
        /// Checks if matrix is diagonal.
        /// </summary>
        /// <returns>True, if matrix is diagonal, and false otherwise.</returns>
        protected override bool IsSuitableMatrix()
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (i == j) continue;
                    if (!array[i, j].Equals(default(T))) throw new ArgumentException("Matrix is not diagonal.");
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
            Console.WriteLine($"Diagonal matrix! Element was changed in {e.Message} at position ({e.I}, {e.J})");
        }
    }
}
