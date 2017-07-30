using System;
using System.Collections.Generic;

namespace Logic
{
    public class BookComparerByYear : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            if (ReferenceEquals(x, null)) throw new ArgumentNullException($"{nameof(x)} is null.");
            if (ReferenceEquals(y, null)) throw new ArgumentNullException($"{nameof(y)} is null.");
            return x.Year.CompareTo(y.Year);
        }
    }
}
