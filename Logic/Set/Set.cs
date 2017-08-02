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
            this.equalityComparer = equalityComparer == null ? EqualityComparer<T>.Default : equalityComparer;
            array = new T[capacity];
        }

        /// <summary>
        /// Ctor with parameter.
        /// </summary>
        /// <param name="capacity">Initial size of set.</param>
        public Set(int capacity, IEqualityComparer<T> equalityComparer = null)
        {
            if (capacity <= 0) throw new ArgumentException($"{nameof(capacity)} is unsuitable.");
            this.equalityComparer = equalityComparer == null ? EqualityComparer<T>.Default : equalityComparer;

            this.capacity = capacity;
            array = new T[capacity];
        }

        /// <summary>
        /// Ctor with parameter.
        /// </summary>
        /// <param name="collection">Collection to copy elements into set.</param>
        public Set(IEnumerable<T> collection, IEqualityComparer<T> equalityComparer = null)
        {
            if (collection == null) throw new ArgumentNullException($"{nameof(collection)} is null.");
            this.equalityComparer = equalityComparer == null ? EqualityComparer<T>.Default : equalityComparer;

            this.capacity = collection.Count();
            array = new T[capacity]; 

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
            for (int i = 0; i < count; i++)
            {
                yield return array[i];
            }
        }
       
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        #region public methods
        #region static methods
        /// <summary>
        /// Returns set that contains all elements that are present in the in both collections.
        /// </summary>
        /// <param name="lhs">One collection.</param>
        /// <param name="rhs">Another collection.</param>
        /// <returns>Set that contains all elements that are present in the in both collections.</returns>
        public static Set<T> Union(IEnumerable<T> lhs, IEnumerable<T> rhs, IEqualityComparer<T> equalityComparer = null)
        {
            if (lhs == null) throw new ArgumentNullException($"{nameof(lhs)} is null.");
            if (rhs == null) throw new ArgumentNullException($"{nameof(rhs)} is null.");

            Set<T> result = new Set<T>(lhs.Union<T>(rhs), equalityComparer);

            return result;
        }

        /// <summary>
        /// Returns set that contains only elements that are also in both collections at once.
        /// </summary>
        /// <param name="lhs">One collection.</param>
        /// <param name="rhs">Another collection.</param>
        /// <returns>Set that contains only elements that are also in both collections at once.</returns>
        public static Set<T> Intersect(IEnumerable<T> lhs, IEnumerable<T> rhs, IEqualityComparer<T> equalityComparer = null)
        {
            if (lhs == null) throw new ArgumentNullException($"{nameof(lhs)} is null.");
            if (rhs == null) throw new ArgumentNullException($"{nameof(rhs)} is null.");

            Set<T> result = new Set<T>(lhs.Intersect<T>(rhs), equalityComparer); 

            return result;
        }

        /// <summary>
        /// Returns set that contains only elements that present only in left collection.
        /// </summary>
        /// <param name="lhs">One collection.</param>
        /// <param name="rhs">Another collection.</param>
        /// <returns>Set that contains only elements that present only in left collection.</returns>
        public static Set<T> Except(IEnumerable<T> lhs, IEnumerable<T> rhs, IEqualityComparer<T> equalityComparer = null)
        {
            if (lhs == null) throw new ArgumentNullException($"{nameof(lhs)} is null.");
            if (rhs == null) throw new ArgumentNullException($"{nameof(rhs)} is null.");

            Set<T> result = new Set<T>(lhs.Except<T>(rhs), equalityComparer);

            return result;
        }

        /// <summary>
        /// Returns set that contains only elements that present only in right collection.
        /// </summary>
        /// <param name="lhs">One collection.</param>
        /// <param name="rhs">Another collection.</param>
        /// <returns>Set that contains only elements that present only in right collection.</returns>
        public static Set<T> SymmetricExcept(IEnumerable<T> lhs, IEnumerable<T> rhs, IEqualityComparer<T> equalityComparer = null)
        {
            if (lhs == null) throw new ArgumentNullException($"{nameof(lhs)} is null.");
            if (rhs == null) throw new ArgumentNullException($"{nameof(rhs)} is null.");

            Set<T> result = new Set<T>(rhs.Except<T>(lhs), equalityComparer);

            return result;
        }
        #endregion

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

            if (IsFull)
                Expansion();

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
                if (equalityComparer.Equals(array[i], item))
                    index = i;
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
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");

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
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");

            for (int i = 0; i < Count; i++)
            {
                if (!other.Contains(array[i], equalityComparer))
                {
                    Remove(array[i--]);
                }
            }
        }

        /// <summary>
        /// Removes all elements in the specified collection from the current set.
        /// </summary>
        /// <param name="other">Other collection.</param>
        public void ExceptWith(IEnumerable<T> other) 
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");

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
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");

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
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");
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
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");
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
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");

            return IsSupersetOf(other) && this.count != other.Count();
        }

        /// <summary>
        /// Determines whether the current set is a proper (strict) subset of a specified collection.
        /// </summary>
        /// <param name="other">Other collection.</param>
        /// <returns>True, if set is proper subset, and false otherwise.</returns>
        public bool IsProperSubsetOf(IEnumerable<T> other) //Определяет, является ли текущий набор должным(строгим) подмножеством заданной коллекции.
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");

            return IsSubsetOf(other) && this.count != other.Count();
        }

        /// <summary>
        /// Determines whether the current set overlaps with the specified collection.
        /// </summary>
        /// <param name="other">Other collection.</param>
        /// <returns>True, if set overlaps with the collection, and false otherwise.</returns>
        public bool Overlaps(IEnumerable<T> other) 
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");

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
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");

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
            if (array == null) throw new ArgumentNullException($"{nameof(array)} is null.");
            if (arrayIndex < 0) throw new ArgumentException($"{nameof(arrayIndex)} is unsuitable.");
            for (int i = arrayIndex; i < array.Length; i++)
            {
                if (i - arrayIndex > Count) return;
                array[i] = this.array[i - arrayIndex];
            }
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
