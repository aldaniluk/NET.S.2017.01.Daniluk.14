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
    public class SquareMatrix<T> : IEquatable<SquareMatrix<T>>
    {
        #region fields
        protected T[,] array;
        #endregion

        #region properties
        /// <summary>
        /// Size of the square matrix.
        /// </summary>
        public int Size { get; }
        #endregion

        #region ctors
        /// <summary>
        /// Ctor without parameters.
        /// </summary>
        public SquareMatrix()
        {
            Size = 1;
            array = new T[Size, Size];
        }

        /// <summary>
        /// Ctor with parameter.
        /// </summary>
        /// <param name="size">Size of the square matrix.</param>
        public SquareMatrix(int size)
        {
            CheckSize(size);
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

        /// <summary>
        /// Element change event.
        /// </summary>
        public event EventHandler<NewElementEventArgs> NewElement = delegate { };

        #region public methods
        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="i">Number of row.</param>
        /// <param name="j">Number of column.</param>
        /// <returns>Element in appropriate position.</returns>
        public T this[int i, int j]
        {
            get
            {
                if (i > array.GetLength(0) || i <= 0) throw new ArgumentException($"Index {nameof(i)} is unsuitable.");
                if (j > array.GetLength(0) || j <= 0) throw new ArgumentException($"Index {nameof(j)} is unsuitable.");
                return array[i - 1, j - 1];
            }
        }

        /// <summary>
        /// Change an element.
        /// </summary>
        /// <param name="element">Element to insert.</param>
        /// <param name="i">Row number.</param>
        /// <param name="j">Column number.</param>
        public void ChangeElement(T element, int i, int j)
        {
            if (i > array.GetLength(0) || i <= 0) throw new ArgumentException($"Index {nameof(i)} is unsuitable.");
            if (j > array.GetLength(0) || j <= 0) throw new ArgumentException($"Index {nameof(j)} is unsuitable.");

            array[i - 1, j - 1] = element;
            IsSuitableMatrix();

            NewElement.Invoke(this, new NewElementEventArgs(GetType().ToString(), i, j));
        }

        /// <summary>
        /// Reaction to an element change event. 
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="e">Object, that contains data about event.</param>
        public virtual void NewElementMessage(object sender, NewElementEventArgs e)
        {
            Console.WriteLine($"Square matrix! Element was changed in {e.Message} at position ({e.I}, {e.J})");
        }

        /// <summary>
        /// Returns string representation of the matrix.
        /// </summary>
        /// <returns>String representation of the matrix.</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for(int i  = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result.Append(array[i, j].ToString() + ' ');
                }
                if (i != array.GetLength(0)-1) result.Append('\n');
            }
            return result.ToString();
        }

        /// <summary>
        /// Checks two objects for equality.
        /// </summary>
        /// <param name="obj">Second object to check.</param>
        /// <returns>True, if objects are equal, and false otherwise.</returns>
        public override bool Equals(object obj)
        {
            SquareMatrix<T> matr = obj as SquareMatrix<T>;
            return Equals(matr);
        }

        /// <summary>
        /// Checks two matrices for equality.
        /// </summary>
        /// <param name="obj">Second matrix to check.</param>
        /// <returns>True, if matrices are equal, and false otherwise.</returns>
        public bool Equals(SquareMatrix<T> obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (Size != obj.Size) return false;

            for (int i = 1; i <= array.GetLength(0); i++)
            {
                for (int j = 1; j <= array.GetLength(1); j++)
                {
                    if (!this[i, j].Equals(obj[i, j])) return false;
                }
            }
            return true;
        }
        #endregion

        #region private & protected methods
        private void FillMatrix(IEnumerable<T> inputArray)
        {
            if (ReferenceEquals(inputArray, null)) throw new ArgumentNullException($"{nameof(inputArray)} is null.");

            int inputArrayIndex = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (inputArrayIndex < inputArray.Count()) array[i, j] = inputArray.ElementAt(inputArrayIndex);
                    inputArrayIndex++;
                }
            }
            IsSuitableMatrix();
        }

        /// <summary>
        /// Checks if matrix is suitable.
        /// </summary>
        /// <returns>True, if matrix is suitable, and false otherwise.</returns>
        protected virtual bool IsSuitableMatrix() => true;

        private void CheckSize(int size)
        {
            if (size <= 0) throw new ArgumentException($"{nameof(size)} can't be less than or equal to 0.");
        }
        #endregion
    }
}
