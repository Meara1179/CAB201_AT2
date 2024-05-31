using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2.Obstacles
{
    internal class FenceObs : Obstacle
    {
        private int Direction;
        private int Length;

        /// <summary>
        /// Fence obstacle constructor.
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="direction"></param>
        /// <param name="length"></param>
        public FenceObs(int xPos, int yPos, int direction, int length)
        {
            XPos = xPos;
            YPos = yPos;
            Direction = direction;
            Length = length;
        }

        public override bool CheckDanger(int agentX, int agentY)
        {
            if (agentX == XPos && agentY == YPos) return true;
            else
            {
                for (int i = 0; i < Length; i++)
                {
                    if (Direction == (int)DirectionEnum.North)
                    {
                        if (agentX == XPos && agentY == YPos + i) return true;
                    }
                    else if (Direction == (int)DirectionEnum.East)
                    {
                        if (agentX == XPos + i && agentY == YPos) return true;
                    }
                }
                return false;
            }
        }

        public override string MapMarker()
        {
            return ("F");
        }
    }
}
