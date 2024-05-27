using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2.Obstacles
{
    internal class GuardObs : Obstacle
    {
        public GuardObs(int xPos, int yPos) 
        {
            XPos = xPos;
            YPos = yPos;
        }
        public override bool CheckDanger(int agentX, int agentY)
        {
            if (agentX == XPos && agentY == YPos) return true;
            else return false;
        }

        public override string MapMarker()
        {
            return ("G");
        }
    }
}
