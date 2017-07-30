using System;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Comparer for Point struct.
    /// </summary>
    public class PointStructComparerByX : IComparer<PointStruct>
    {
        /// <summary>
        /// Compares two points by X.
        /// </summary>
        /// <param name="x">One point to compare.</param>
        /// <param name="y">Another point to compare.</param>
        /// <returns>1, if left point is greater; -1, if less; 0, if they are equal.</returns>
        public int Compare(PointStruct x, PointStruct y) => x.X.CompareTo(y.X);
    }
}
