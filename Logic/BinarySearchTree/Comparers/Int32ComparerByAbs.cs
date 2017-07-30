using System;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Comparer for int32 numbers.
    /// </summary>
    public class Int32ComparerByAbs : IComparer<int>
    {
        /// <summary>
        /// Compares two int32 numbers by modulo.
        /// </summary>
        /// <param name="x">One number to compare.</param>
        /// <param name="y">Another number to compare.</param>
        /// <returns>1, if left number is greater; -1, if less; 0, if they are equal.</returns>
        public int Compare(int x, int y) => Math.Abs(x).CompareTo(Math.Abs(y));
    }
}
