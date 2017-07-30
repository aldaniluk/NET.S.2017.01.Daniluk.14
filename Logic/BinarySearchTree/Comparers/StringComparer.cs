using System;
using System.Collections.Generic;

namespace Logic
{
    public class StringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (ReferenceEquals(x, null)) throw new ArgumentNullException($"{nameof(x)} is null.");
            if (ReferenceEquals(y, null)) throw new ArgumentNullException($"{nameof(y)} is null.");

            return x.Length.CompareTo(y.Length);
        }
    }
}
