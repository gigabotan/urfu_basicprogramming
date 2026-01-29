using System;

namespace Geometry
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength() => Geometry.GetLength(this);
        public Vector Add(Vector other) => Geometry.Add(this, other);
        public bool Belongs(Segment segment) => Geometry.IsVectorInSegment(this, segment);
    }

    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static double GetLength(Segment segment)
        {
            var deltaX = segment.End.X - segment.Begin.X;
            var deltaY = segment.End.Y - segment.Begin.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        public static Vector Add(Vector vector, Vector otherVector)
        {
            return new Vector
            {
                X = vector.X + otherVector.X,
                Y = vector.Y + otherVector.Y
            };
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var crossProduct = (vector.Y - segment.Begin.Y) * (segment.End.X - segment.Begin.X) -
                               (vector.X - segment.Begin.X) * (segment.End.Y - segment.Begin.Y);
            if (Math.Abs(crossProduct) > 1e-10)
                return false;

            var minX = Math.Min(segment.Begin.X, segment.End.X);
            var maxX = Math.Max(segment.Begin.X, segment.End.X);
            var minY = Math.Min(segment.Begin.Y, segment.End.Y);
            var maxY = Math.Max(segment.Begin.Y, segment.End.Y);

            return vector.X >= minX && vector.X <= maxX && vector.Y >= minY && vector.Y <= maxY;
        }
    }

    public class Segment
    {
        public required Vector Begin;
        public required Vector End;

        public double GetLength() => Geometry.GetLength(this);
        public bool Contains(Vector vector) => Geometry.IsVectorInSegment(vector, this);
    }
}
