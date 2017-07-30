using System;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Comparer for Book class.
    /// </summary>
    public class BookComparerByYear : IComparer<Book>
    {
        /// <summary>
        /// Compares two book by year.
        /// </summary>
        /// <param name="x">One book to compare.</param>
        /// <param name="y">Another book to compare.</param>
        /// <returns>1, if left book is greater; -1, if less; 0, if they are equal.</returns>
        public int Compare(Book x, Book y)
        {
            if (ReferenceEquals(x, null)) throw new ArgumentNullException($"{nameof(x)} is null.");
            if (ReferenceEquals(y, null)) throw new ArgumentNullException($"{nameof(y)} is null.");
            return x.Year.CompareTo(y.Year);
        }
    }
}
