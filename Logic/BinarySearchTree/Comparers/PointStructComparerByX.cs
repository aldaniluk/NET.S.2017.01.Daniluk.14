using System;
using System.Collections.Generic;

namespace Logic
{
    public class PointStructComparerByX : IComparer<PointStruct>
    {
        public int Compare(PointStruct x, PointStruct y) => x.X.CompareTo(y.X);
    }
}
