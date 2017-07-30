using System;

namespace Logic
{
    public struct PointStruct
    {
        public int X { get; }
        public int Y { get; }

        public PointStruct(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}, {Y})";
    }
}
