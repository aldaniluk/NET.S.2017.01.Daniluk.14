using System;

namespace ConsoleApp
{
    /// <summary>
    /// Class Point.
    /// </summary>
    public class Point : IEquatable<Point>
    {
        #region properties
        /// <summary>
        /// Сoordinate along the x axis.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Сoordinate along the y axis.
        /// </summary>
        public int Y { get; }
        #endregion

        #region ctors
        /// <summary>
        /// Ctor without parameters.
        /// </summary>
        public Point() {}

        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        #endregion

        /// <summary>
        /// Compares two points for equality.
        /// </summary>
        /// <param name="other">Other point to compare.</param>
        /// <returns>True, if points are equal, and false otherwise.</returns>
        public bool Equals(Point other) => X == other.X && Y == other.Y;

        /// <summary>
        /// Returns string representation of the point.
        /// </summary>
        /// <returns>String representation of the point.</returns>
        public override string ToString() => $"(x: {X}, y:{Y})";

    }
}
