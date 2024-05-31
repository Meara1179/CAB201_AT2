using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2.Pathfinding
{
    internal class Node : Point
    {
        /// <summary>
        /// Distance of tiles between the start of the path and this Node.
        /// </summary>
        public double G { get; set; }
        /// <summary>
        /// Distance of tiles between the target tile and this node.
        /// </summary>
        public double H { get; set; }
        /// <summary>
        /// Node cost calculated using the addition of G and H.
        /// </summary>
        public double F => G + H;
        /// <summary>
        /// Node connected to this Node, creating a chain of Nodes.
        /// </summary>
        public Node? ParentNode { get; set; }

        /// <summary>
        /// Node object constructor.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        public Node(Point p, int g = int.MaxValue, int h = int.MaxValue) : base(p.X, p.Y)
        {
            G = g;
            H = h;
        }
    }
}
