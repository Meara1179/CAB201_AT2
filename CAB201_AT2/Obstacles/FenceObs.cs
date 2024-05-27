using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2.Obstacles
{
    internal class FenceObs : Obstacle
    {
        public int Direction;
        public int Length;

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
