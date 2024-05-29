using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2
{
    internal class Point : IEquatable<Point>
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double FindDist(Point p)
        {
            double distX = p.X - X;
            double distY = p.Y - Y;

            return Math.Abs(distX) + Math.Abs(distY);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Point);
        }

        public bool Equals(Point? other)
        {
            return !object.ReferenceEquals(other, null) && X == other.X && Y == other.Y;
        }

        public bool CheckDanger()
        {
            Map map = new Map();
            var obList = map.ReturnObstacleList();
            foreach (var ob in obList)
            {
                if (ob.CheckDanger((int)X, (int)Y))
                {
                    return true;
                }
            }
            return false;
        }

        public List<Point> Neighbours
        {
            get
            {
                return new List<Point>{
                    new Point(X - 1, Y),
                    new Point(X, Y - 1),
                    new Point(X + 1, Y),
                    new Point(X, Y + 1)
                };
            }
        }
    }
}
