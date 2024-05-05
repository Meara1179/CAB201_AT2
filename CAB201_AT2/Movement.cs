using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2
{
    internal class Movement
    {
        public enum DirectionEnum { North, East, South, West }

        public int Direction { get; set; }
        public int MoveCount { get; set; }

        public Movement(int direction, int moveCount)
        {
            Direction = direction;
            MoveCount = moveCount;
        }
    }
}
