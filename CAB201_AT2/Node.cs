using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2
{
    internal class Node : Point
    {
        public double G { get; set; }
        public double H { get; set; }
        public double F => G + H;
        public Node? ParentNode {  get; set; }

        public Node (Point p, int g = int.MaxValue, int h = int.MaxValue) : base(p.X, p.Y)
        {
            G = g;
            H = h;
        }
    }
}
