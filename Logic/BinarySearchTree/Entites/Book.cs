using System;

namespace Logic
{
    public class Book : IComparable<Book>
    {
        public string Name { get; }
        public string Author { get; }
        public int Year { get; }

        public Book(string name, string author, int year)
        {
            if (CheckName(name)) Name = name;
            if (CheckAuthor(author)) Author = author;
            if (CheckYear(year)) Year = year;
        }

        public int CompareTo(Book other)
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null.");
            return Author.CompareTo(other.Author);
        }

        public override string ToString() => $"\"{Name}\", {Author}, {Year}";

        private bool CheckName(string name)
        {
            if (ReferenceEquals(name, null)) throw new ArgumentNullException($"{nameof(name)} is null.");
            return true;
        }
        private bool CheckAuthor(string author)
        {
            if (ReferenceEquals(author, null)) throw new ArgumentNullException($"{nameof(author)} is null.");
            return true;
        }

        private bool CheckYear(int year)
        {
            if (year > DateTime.Now.Year || year < 1000) throw new ArgumentException($"{nameof(year)} is unsuitable.");
            return true;
        }
    }
}
