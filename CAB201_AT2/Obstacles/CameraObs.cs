using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2.Obstacles
{
    internal class CameraObs : Obstacle
    {
        public int Direction { get; set; }

        /// <summary>
        /// Camera object constructor.
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="direction"></param>
        public CameraObs(int xPos, int yPos, int direction)
        {
            XPos = xPos;
            YPos = yPos;
            Direction = direction;
        }

        public override bool CheckDanger(int agentX, int agentY)
        {
            if (agentX == XPos && agentY == YPos) return true;
            else
            {
                return CameraCheckDanger(XPos, YPos, agentX, agentY, Direction);
            }
        }
        public override string MapMarker()
        {
            return ("C");
        }

        // Private method called by the CheckDanger method to check if the specified tile falls within the danger field of
        // the camera's cone field of view.
        private bool CameraCheckDanger(int xValue, int yValue, int agentX, int agentY, int direction)
        {
            int length = 0;
            int width = 0;
            int agentLength = 0;
            int agentWidth = 0;
            int distance = 0;
            int numSign = 1;

            switch (Direction)
            {
                case (int)DirectionEnum.North:
                    length = yValue;
                    agentLength = yValue;
                    width = xValue;
                    agentWidth = xValue;
                    distance = Math.Abs(agentY - yValue);
                    numSign = 1;
                    break;
                case (int)DirectionEnum.East:
                    length = xValue;
                    agentLength = xValue;
                    width = yValue;
                    agentWidth = yValue;
                    distance = Math.Abs(agentX - xValue);
                    numSign = 1;
                    break;
                case (int)DirectionEnum.South:
                    length = yValue;
                    agentLength = yValue;
                    width = xValue;
                    agentWidth = xValue;
                    distance = Math.Abs(agentY - yValue);
                    numSign = -1;
                    break;
                case (int)DirectionEnum.West:
                    length = xValue;
                    agentLength = xValue;
                    width = yValue;
                    agentWidth = yValue;
                    distance = Math.Abs(agentX - xValue);
                    numSign = -1;
                    break;
            }

            for (int i = 1; i <= distance; i++)
            {
                if (length + i * numSign == agentWidth && width == agentWidth)
                {
                    return true;
                }
                for (int j = 1; j <= i; j++)
                {
                    if (agentWidth == width + j && agentLength == length + i * numSign)
                    {
                        return true;
                    }
                    else if (agentWidth == width - j && agentLength == length + i * numSign)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
