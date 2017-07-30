using System;
using System.Collections.Generic;

namespace Logic
{
    public class Int32Comparer : IComparer<int>
    {
        public int Compare(int x, int y) => Math.Abs(x).CompareTo(Math.Abs(y));
    }
}
