using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class Set<T> : ISet<T> where T: class, IEquatable<T>
    {
        #region fields      
        private T[] array;
        private int capacity = 8;
        private int count;
        #endregion

        #region properties
        public bool IsReadOnly => false;
        public int Count => count;
        public bool IsEmpty => count == 0;
        private bool IsFull => count == capacity;
        #endregion

        #region ctors
        public Set()
        {
            array = new T[capacity];
        }

        public Set(int capacity)
        {
            if (capacity <= 0) throw new ArgumentException($"{nameof(capacity)} is unsuitable.");

            this.capacity = capacity;
            array = new T[capacity];
        }

        public Set(IEnumerable<T> collection)
        {
            if (ReferenceEquals(collection, null)) throw new ArgumentNullException($"{nameof(collection)} is null.");

            foreach (var item in collection)
            {
                Add(item);
            }
        }
        #endregion

        #region enumerator
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

        public bool Remove(T item) //Удаляет первое вхождение указанного объекта из коллекции
        {
            if (!Contains(item)) return false;

            int index = 0;
            for (int i = 0; i < Count; i++)
            {
                if (array[i].Equals(item)) index = i;
            }
            array[index] = array[Count - 1];
            array[Count - 1] = default(T);
            count--;
            return true;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < count; i++)
            {
                if (array[i].Equals(item))
                    return true;
            }
            return false;
        }

        public void Clear() //Удаляет все элементы из коллекции ICollection<T>
        {
            this.count = 0;
            this.capacity = 8;
            this.array = new T[this.capacity];
        }
        #endregion

        #region UnionWith, IntersectWith, ExceptWith, SymmetricExceptWith
        public void UnionWith(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            foreach(var item in other)
            {
                Add(item);
            }
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            foreach (var item in other)
            {
                if (!Contains(item))
                    Remove(item);
            }
        }

        public void ExceptWith(IEnumerable<T> other) //Удаляет все элементы указанной коллекции из текущего набора.
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            foreach (var item in other)
            {
                Remove(item);
            }
        }

        public void SymmetricExceptWith(IEnumerable<T> other) //Изменяет текущий набор таким образом, чтобы он содержал только элементы, которые есть либо в нем, либо в указанной коллекции, но не одновременно там и там.
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
        public bool IsSubsetOf(IEnumerable<T> other) //Определяет, является ли набор подмножеством заданной коллекции.
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");
            if (this.Count > other.Count()) return false;

            for (int i = 0; i < Count; i++)
            {
                if (!other.Contains(array[i])) return false;
            }
            return true;
        }

        public bool IsSupersetOf(IEnumerable<T> other) // Определяет, является ли текущий набор надмножеством заданной коллекции.
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");
            if (this.Count < other.Count()) return false;

            foreach (var item in other)
            {
                if (!Contains(item)) return false;
            }
            return true;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other) //Определяет, является ли текущий набор должным (строгим) надмножеством заданной коллекции.
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            return IsSupersetOf(other) && this.count != other.Count();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other) //Определяет, является ли текущий набор должным(строгим) подмножеством заданной коллекции.
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            return IsSubsetOf(other) && this.count != other.Count();
        }

        public bool Overlaps(IEnumerable<T> other) //Определяет, пересекаются ли текущий набор и указанная коллекция.
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
        public bool SetEquals(IEnumerable<T> other) //Определяет, содержат ли текущий набор и указанная коллекция одни и те же элементы.
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

            if (ReferenceEquals(this, other)) return true;
            if (Count != other.Count()) return false;

            return IsSupersetOf(other) && IsSubsetOf(other);
        }
        #endregion

        #region CopyTo
        public void CopyTo(T[] array, int arrayIndex) //Копирует элементы коллекции в массив Array, начиная с указанного индекса массива Array
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
