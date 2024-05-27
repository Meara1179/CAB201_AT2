using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2.Obstacles
{
    public enum DirectionEnum { North, East, South, West }

    internal class Obstacle
    {

        public int XPos { get; set; }
        public int YPos { get; set; }

        // Public empty constructor for public method access.
        public Obstacle()
        {

        }

        /// <summary>
        /// Takes the supplied arguments and checks if the specified tile falls within the danger field of the Obstacle.
        /// </summary>
        /// <param name="agentX"></param>
        /// <param name="agentY"></param>
        /// <returns></returns>
        public virtual bool CheckDanger(int agentX, int agentY)
        {
            if (agentX == XPos && agentY == YPos) return true;
            else return false;
        }

        public virtual string MapMarker()
        {
            return ("O");
        }
    }
}
