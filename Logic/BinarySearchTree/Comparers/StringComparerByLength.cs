using System;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Comparer for strings.
    /// </summary>
    public class StringComparerByLength : IComparer<string>
    {
        /// <summary>
        /// Compares two strings by length.
        /// </summary>
        /// <param name="x">One string to compare.</param>
        /// <param name="y">Another string to compare.</param>
        /// <returns>1, if left string is greater; -1, if less; 0, if they are equal.</returns>
        public int Compare(string x, string y)
        {
            if (ReferenceEquals(x, null)) throw new ArgumentNullException($"{nameof(x)} is null.");
            if (ReferenceEquals(y, null)) throw new ArgumentNullException($"{nameof(y)} is null.");
            return x.Length.CompareTo(y.Length);
        }
    }
}
