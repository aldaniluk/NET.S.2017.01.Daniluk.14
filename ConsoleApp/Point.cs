using System;

namespace ConsoleApp
{
    public class Point : IEquatable<Point>
    {
        public int X { get; }
        public int Y { get; }

        public Point() {}

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point other) => X == other.X && Y == other.Y;

        public override string ToString() => $"(x: {X}, y:{Y})";
    }
}
