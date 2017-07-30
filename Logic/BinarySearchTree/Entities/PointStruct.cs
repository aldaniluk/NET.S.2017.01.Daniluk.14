using System;

namespace Logic
{
    /// <summary>
    /// Struct for representation a point entity.
    /// </summary>
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
