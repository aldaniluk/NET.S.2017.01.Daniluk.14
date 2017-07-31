using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    /// <summary>
    /// Generic collection set.
    /// </summary>
    /// <typeparam name="T">Type for substitution.</typeparam>
    public class Set<T> : ISet<T> where T: class
    {
        #region fields      
        private T[] array;
        private int capacity = 8;
        private int count;
        private readonly IEqualityComparer<T> equalityComparer;
        #endregion

        #region properties
        /// <summary>
        /// Returns true, if set can't be modified, and false otherwise.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Quantity of elements.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Returns true, if set is empty, and false otherwise.
        /// </summary>
        public bool IsEmpty => count == 0;

        private bool IsFull => count == capacity;
        #endregion

        #region ctors
        /// <summary>
        /// Ctor without parameters.
        /// </summary>
        public Set(IEqualityComparer<T> equalityComparer = null)
        {
            this.equalityComparer = ReferenceEquals(equalityComparer, null) ? EqualityComparer<T>.Default : equalityComparer;
            array = new T[capacity];
        }

        /// <summary>
        /// Ctor with parameter.
        /// </summary>
        /// <param name="capacity">Initial size of set.</param>
        public Set(int capacity, IEqualityComparer<T> equalityComparer = null)
        {
            if (capacity <= 0) throw new ArgumentException($"{nameof(capacity)} is unsuitable.");
            this.equalityComparer = ReferenceEquals(equalityComparer, null) ? EqualityComparer<T>.Default : equalityComparer;

            this.capacity = capacity;
            array = new T[capacity];
        }

        /// <summary>
        /// Ctor with parameter.
        /// </summary>
        /// <param name="collection">Collection to copy elements into set.</param>
        public Set(IEnumerable<T> collection, IEqualityComparer<T> equalityComparer = null)
        {
            if (ReferenceEquals(collection, null)) throw new ArgumentNullException($"{nameof(collection)} is null.");
            this.equalityComparer = ReferenceEquals(equalityComparer, null) ? EqualityComparer<T>.Default : equalityComparer;

            foreach (var item in collection)
            {
                Add(item);
            }
        }
        #endregion

        #region enumerator
        /// <summary>
        /// Enumerator for set.
        /// </summary>
        /// <returns>Object IEnumerator<T> to iterate.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i < count; i++)
            {
                yield return array[i];
            }
        }
       
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        #region public methods
        #region Add, Remove, Contains, Clear
        /// <summary>
        /// Adds an element into the set.
        /// </summary>
        /// <param name="item">Element to add.</param>
        /// <returns>True, if element was added, and false otherwise.</returns>
        public bool Add(T item)
        {
            if (ReferenceEquals(item, null)) throw new ArgumentNullException($"{nameof(item)} is null.");

            if (this.Contains(item))
                return false;

            if (IsFull) Expansion();

            array[count++] = item;
            return true;
        }

        void ICollection<T>.Add(T item) => Add(item);

        /// <summary>
        /// Removes an element from the set.
        /// </summary>
        /// <param name="item">Element to remove.</param>
        /// <returns>True, if element was removed, and false otherwise.</returns>
        public bool Remove(T item)
        {
            if (!Contains(item)) return false;

            int index = 0;
            for (int i = 0; i < Count; i++)
            {
                if (equalityComparer.Equals(array[i], item)) index = i;
            }
            array[index] = array[Count - 1];
            array[Count - 1] = default(T);
            count--;
            return true;
        }

        /// <summary>
        /// Checks the presence of the element in the set.
        /// </summary>
        /// <param name="item">Element to check.</param>
        /// <returns>True, if element presents, and false otherwise.</returns>
        public bool Contains(T item)
        {
            for (int i = 0; i < count; i++)
            {
                if (equalityComparer.Equals(array[i], item))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Clears the set.
        /// </summary>
        public void Clear()
        {
            this.count = 0;
            this.capacity = 8;
            this.array = new T[this.capacity];
        }
        #endregion

        #region UnionWith, IntersectWith, ExceptWith, SymmetricExceptWith
        /// <summary>
        /// Modifies the current set so that it contains all elements that are present in the in both collections.
        /// </summary>
        /// <param name="other">Other collection.</param>
        public void UnionWith(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            foreach(var item in other)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that are also in a specified collection.
        /// </summary>
        /// <param name="other">Other collection.</param>
        public void IntersectWith(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            foreach (var item in other)
            {
                if (!Contains(item))
                    Remove(item);
            }
        }

        /// <summary>
        /// Removes all elements in the specified collection from the current set.
        /// </summary>
        /// <param name="other">Other collection.</param>
        public void ExceptWith(IEnumerable<T> other) 
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            foreach (var item in other)
            {
                Remove(item);
            }
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both.
        /// </summary>
        /// <param name="other">Other collection.</param>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            foreach (var item in other)
            {
                if (Contains(item))
                    Remove(item);
                else
                    Add(item);
            }
        }
        #endregion

        #region IsSubsetOf, IsSupersetOf, IsProperSupersetOf, IsProperSubsetOf, Overlaps
        /// <summary>
        /// Determines whether a set is a subset of a specified collection.
        /// </summary>
        /// <param name="other">Other collection.</param>
        /// <returns>True, if set is subset, and false otherwise.</returns>
        public bool IsSubsetOf(IEnumerable<T> other) 
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");
            if (this.Count > other.Count()) return false;

            for (int i = 0; i < Count; i++)
            {
                if (!other.Contains(array[i])) return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the current set is a superset of a specified collection.
        /// </summary>
        /// <param name="other">Other collection.</param>
        /// <returns>True, if set is superset, and false otherwise.</returns>
        public bool IsSupersetOf(IEnumerable<T> other) 
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");
            if (this.Count < other.Count()) return false;

            foreach (var item in other)
            {
                if (!Contains(item)) return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the current set is a proper (strict) superset of a specified collection.
        /// </summary>
        /// <param name="other">Other collection.</param>
        /// <returns>True, if set is proper superset, and false otherwise.</returns>
        public bool IsProperSupersetOf(IEnumerable<T> other) 
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            return IsSupersetOf(other) && this.count != other.Count();
        }

        /// <summary>
        /// Determines whether the current set is a proper (strict) subset of a specified collection.
        /// </summary>
        /// <param name="other">Other collection.</param>
        /// <returns>True, if set is proper subset, and false otherwise.</returns>
        public bool IsProperSubsetOf(IEnumerable<T> other) //Определяет, является ли текущий набор должным(строгим) подмножеством заданной коллекции.
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            return IsSubsetOf(other) && this.count != other.Count();
        }

        /// <summary>
        /// Determines whether the current set overlaps with the specified collection.
        /// </summary>
        /// <param name="other">Other collection.</param>
        /// <returns>True, if set overlaps with the collection, and false otherwise.</returns>
        public bool Overlaps(IEnumerable<T> other) 
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            foreach (var item in this)
            {
                if (other.Contains(item)) return true;
            }
            return false;
        }
        #endregion

        #region SetEquals
        /// <summary>
        /// Determines whether the current set and the specified collection contain the same elements.
        /// </summary>
        /// <param name="other">Other collection.</param>
        /// <returns>True, if they contain the same elements, and false otherwise.</returns>
        public bool SetEquals(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            if (ReferenceEquals(this, other)) return true;
            if (Count != other.Count()) return false;

            return IsSupersetOf(other) && IsSubsetOf(other);
        }
        #endregion

        #region CopyTo
        /// <summary>
        /// Copies the elements of the ICollection<T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">Array to copy.</param>
        /// <param name="arrayIndex">Start index.</param>
        public void CopyTo(T[] array, int arrayIndex) 
        {
            if (ReferenceEquals(array, null)) throw new ArgumentNullException($"{nameof(array)} is null.");
            if (arrayIndex < 0) throw new ArgumentException($"{nameof(arrayIndex)} is unsuitable.");
            for (int i = arrayIndex; i < array.Length; i++)
            {
                if (i - arrayIndex > Count) return;
                array[i] = this.array[i - arrayIndex];
            }
        }
        #endregion

        #region ToString
        /// <summary>
        /// Returns string representation of the set.
        /// </summary>
        /// <returns>String representation of the set</returns>
        public override string ToString()
        {
            if (Count == 0) return "Set is empty.";
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                str.Append(array[i].ToString() + " ");
            }
            return str.ToString();
        }
        #endregion
        #endregion

        #region private methods
        private void Expansion()
        {
            capacity *= 2;
            Array.Resize(ref array, capacity);
        }
        #endregion
    }
}
