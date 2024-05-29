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
        public int XPos {  get; set; }
        public int YPos { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public int MoveCount { get; set; }

        public Checker? NorthTile { get; set; }
        public Checker? EastTile { get; set; }
        public Checker? SouthTile { get; set; }
        public Checker? WestTile { get; set; }
        public Checker? ParentTile { get; set; }

        public bool CheckChildTile { get; set; }
        public bool IsDanger = false;

        public Checker()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <param name="parentTile"></param>
        /// <param name="moveCount"></param>
        /// <param name="findPath"></param>
        /// <param name="checkChildTile"></param>
        
        // Main constructor used to create child objects.
        public Checker(int xPos, int yPos, int startX, int startY, int endX, int endY, Checker? parentTile, int moveCount, bool findPath = false, bool checkChildTile = false)
        {
            XPos = xPos;
            YPos = yPos;
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
            ParentTile = parentTile;
            MoveCount = moveCount;
            CheckChildTile = checkChildTile;

            StartCheck();
            if (!CheckChildTile) { CheckNeighbourTiles(); }
        }
        // Checker constructor used by the Check command.
        public Checker(int xPos, int yPos)
        {
            XPos = xPos;
            YPos= yPos;
            StartX = xPos;
            StartY = yPos;
            EndX = xPos;
            EndY = yPos;
            ParentTile = null;
            MoveCount = 0;
            CheckChildTile = false;


            StartCheck();
            if (!CheckChildTile) { CheckNeighbourTiles(); }
        }

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
                        NorthTile = new Checker(XPos, YPos + 1, StartX, StartY, EndX, EndY, this, MoveCount++, false, true);
                        EastTile = new Checker(XPos + 1, YPos, StartX, StartY, EndX, EndY, this, MoveCount++, false, true);
                        SouthTile = new Checker(XPos, YPos - 1, StartX, StartY, EndX, EndY, this, MoveCount++, false, true);
                        WestTile = new Checker(XPos - 1, YPos, StartX, StartY, EndX, EndY, this, MoveCount++, false, true);
                    }
                }
            }
        }
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
