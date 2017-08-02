using System;
using System.Collections;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Class for the representation of the square matrix.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    public abstract class AbstractMatrix<T> : IEnumerable<T>
    {
        private int size = 1;

        /// <summary>
        /// Size of the matrix.
        /// </summary>
        public int Size
        {
            get => size;
            protected set
            {
                CheckSize(value);
                size = value;
            }
        }

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
            set
            {
                CheckIndexes(i - 1, j - 1);
                SetElement(value, i - 1, j - 1);
                OnNewElement(new NewElementEventArgs(GetType().ToString(), i, j));
            }
        }

        protected abstract T GetElement(int i, int j);
        protected abstract void SetElement(T element, int i, int j);

        protected virtual void OnNewElement(NewElementEventArgs args)
        {
            NewElement?.Invoke(this, args);
        }

        private void CheckSize(int size)
        {
            if (size <= 0) throw new ArgumentException($"{nameof(size)} can't be less than or equal to 0.");
        }

        protected void CheckIndexes(int i, int j)
        {
            if (i >= Size || i < 0) throw new ArgumentException($"Index {nameof(i)} is unsuitable.");
            if (j >= Size || j < 0) throw new ArgumentException($"Index {nameof(j)} is unsuitable.");
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 1; i <= Size; i++)
            {
                for (int j = 1; j <= Size; j++)
                {
                    yield return this[i, j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
