using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAB201_AT2.Obstacles;

namespace CAB201_AT2
{
    internal class Checker
    {
        /// <summary>
        /// Horizontal position of this object.
        /// </summary>
        public int XPos { get;}
        /// <summary>
        /// Vertical position of this object.
        /// </summary>
        public int YPos { get;}
        /// <summary>
        /// True if this object is positioned within range of an obstacle.
        /// </summary>
        public bool IsDanger { get; private set; }

        /// <summary>
        /// Child Checker object immediately north of this object.
        /// </summary>
        public Checker? NorthTile { get; private set; }
        /// <summary>
        /// Child Checker object immediately east of this object.
        /// </summary>
        public Checker? EastTile { get; private set; }
        /// <summary>
        /// Child Checker object immediately south of this object.
        /// </summary>
        public Checker? SouthTile { get; private set; }
        /// <summary>
        /// Child Checker object immediately west of this object.
        /// </summary>
        public Checker? WestTile { get; private set; }

        private bool CheckChildTile;

        /// <summary>
        /// Parent Checker constructor used by the Check command.
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public Checker(int xPos, int yPos)
        {
            XPos = xPos;
            YPos= yPos;
            CheckChildTile = false;


            StartCheck();
            if (!CheckChildTile) { CheckNeighbourTiles(); }
        }

        // Private constructor used to create child objects.
        private Checker(int xPos, int yPos, bool checkChildTile = false)
        {
            XPos = xPos;
            YPos = yPos;
            this.CheckChildTile = checkChildTile;

            StartCheck();
            if (!this.CheckChildTile) { CheckNeighbourTiles(); }
        }

        // Checks if the position is occupied by an obstacle, then creates adjacent children if tile is safe.
        private void StartCheck()
        {
            foreach (Obstacle ob in Map.obstaclesList)
            {
                bool danger = ob.CheckDanger(XPos, YPos);
                if (danger)
                {
                    IsDanger = true;
                    break;
                }
                else
                {
                    if (!CheckChildTile)
                    {
                        NorthTile = new Checker(XPos, YPos + 1, true);
                        EastTile = new Checker(XPos + 1, YPos, true);
                        SouthTile = new Checker(XPos, YPos - 1, true);
                        WestTile = new Checker(XPos - 1, YPos, true);
                    }
                }
            }
        }

        // Checks if the adjacent children are in danger, then reports which directions are clear.
        private void CheckNeighbourTiles()
        {
            bool northDanger = false;
            bool eastDanger = false;
            bool southDanger = false;
            bool westDanger = false;
            int dangerCount = 0;

            if (NorthTile != null)
            {
                northDanger = NorthTile.IsDanger;
                if (northDanger) dangerCount++;
            }
            if (EastTile != null)
            {
                eastDanger = EastTile.IsDanger;
                if (eastDanger) dangerCount++;
            }
            if (SouthTile != null) 
            { 
                southDanger = SouthTile.IsDanger;
                if (southDanger) dangerCount++;
            }
            if (WestTile != null)
            {
                westDanger = WestTile.IsDanger;
                if (westDanger) dangerCount++;
            }

            if (IsDanger)
            {
                Console.WriteLine("Agent, your location is compromised. Abort mission.");
            }
            else if (dangerCount >=4)
            {
                Console.WriteLine("You cannot safely move in any direction. Abort mission.");
            }
            else
            {
                Console.WriteLine("You can safely take any of the following directions:");
                if (!northDanger) { Console.WriteLine("North"); }
                if (!eastDanger) { Console.WriteLine("East"); }
                if (!southDanger) { Console.WriteLine("South"); }
                if (!westDanger) { Console.WriteLine("West"); }
            }
        }
    }
}
