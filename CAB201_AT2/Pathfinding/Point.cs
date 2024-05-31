using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2.Pathfinding
{
    internal class Point : IEquatable<Point>
    {
        /// <summary>
        /// Horizontal position of the Point.
        /// </summary>
        public double X { get; }
        /// <summary>
        /// Vertical position of the Point.
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Point constructor.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Finds the Manhattan distance between this and another point.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
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
            return !ReferenceEquals(other, null) && X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Returns true if the Point is within range of an obstacle.
        /// </summary>
        /// <returns></returns>
        public bool CheckDanger()
        {
            Map map = new Map();
            return map.CheckIfDanger((int)X, (int)Y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        /// <summary>
        /// Creates 4 adjacent child Points and stores them as a list.
        /// </summary>
        public List<Point> neighbours
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
