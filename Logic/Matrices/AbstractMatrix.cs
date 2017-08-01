using System;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Class for the representation of the square matrix.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractMatrix<T> : IEquatable<AbstractMatrix<T>>
    {
        protected int size = 1;
        /// <summary>
        /// Size of the matrix.
        /// </summary>
        public int Size { get => size; }

        /// <summary>
        /// Change element event.
        /// </summary>
        public event EventHandler<NewElementEventArgs> NewElement = delegate { };

        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="i">Row number.</param>
        /// <param name="j">Column number.</param>
        /// <returns></returns>
        public T this[int i, int j]
        {
            get
            {
                CheckIndexes(i - 1, j - 1);
                return GetElement(i - 1, j - 1);
            }
        }

        /// <summary>
        /// Changes an element on the (i,j) position.
        /// </summary>
        /// <param name="element">Element to change.</param>
        /// <param name="i">Row number.</param>
        /// <param name="j">Column number.</param>
        public void ChangeElement(T element, int i, int j)
        {
            SetElement(element, i - 1, j - 1);
            NewElement.Invoke(this, new NewElementEventArgs(GetType().ToString(), i, j));
        }

        protected abstract T GetElement(int i, int j);
        protected abstract void SetElement(T element, int i, int j);

        protected void CheckSize(int size)
        {
            if (size <= 0) throw new ArgumentException($"{nameof(size)} can't be less than or equal to 0.");
        }

        protected void CheckIndexes(int i, int j)
        {
            if (i >= Size || i < 0) throw new ArgumentException($"Index {nameof(i)} is unsuitable.");
            if (j >= Size || j < 0) throw new ArgumentException($"Index {nameof(j)} is unsuitable.");
        }

        /// <summary>
        /// Compares two matrix for equality.
        /// </summary>
        /// <param name="other">Other matrix to compare.</param>
        /// <returns>True, if they are equal, and false otherwise.</returns>
        public bool Equals(AbstractMatrix<T> other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;
            if (Size != other.Size) return false;

            for (int i = 1; i <= Size; i++)
            {
                for (int j = 1; j <= Size; j++)
                {
                    if (!this[i, j].Equals(other[i, j])) return false;
                }
            }
            return true;
        }
    }
}
