using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2.Obstacles
{
    /// <summary>
    /// Enumerable which maps each cardinal direction to an integer.
    /// </summary>
    public enum DirectionEnum { North, East, South, West }

    internal abstract class Obstacle
    {
        /// <summary>
        /// Horizontal position of this object.
        /// </summary>
        public int XPos { get; set; }
        /// <summary>
        /// Vertical position of this object.
        /// </summary>
        public int YPos { get; set; }

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

        /// <summary>
        /// Returns the alphabetical used to indicate the obstacle type in the Map command. 
        /// </summary>
        /// <returns></returns>
        public virtual string MapMarker()
        {
            return ("O");
        }
    }
}
