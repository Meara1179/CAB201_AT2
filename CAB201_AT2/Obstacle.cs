using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2
{
    public enum TypeEnum { Guard, Sensor, Camera, Fence }
    public enum DirectionEnum { North, East, South, West }

    internal class Obstacle
    {
        public int Type { get; set; }
        public double Radius { get; set; }
        public int Direction { get; set; }

        public int XPos { get; set; }
        public int YPos { get; set; }

        // Public empty constructor for public method access.
        public Obstacle()
        {

        }
        // Private constructor that is accessed through methods.
        private Obstacle(int type, double radius, int direction, int xPos, int yPos)
        {
            Type = type;
            Radius = radius;
            Direction = direction;
            XPos = xPos;
            YPos = yPos;
        }

        /// <summary>
        /// Takes supplied arguments and creates a new Obstacle object with the Guard type.
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <returns></returns>
        public Obstacle CreateGuard(int xPos, int yPos)
        {
            return new Obstacle((int)TypeEnum.Guard, 0, (int)DirectionEnum.North, xPos, yPos);
        }
        /// <summary>
        /// Takes supplied arguments and creates a new Obstacle object with the Sensor type.
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public Obstacle CreateSensor(int xPos, int yPos, double radius) 
        {
            return new Obstacle((int)TypeEnum.Sensor, radius, (int)DirectionEnum.North, xPos, yPos);
        }
        /// <summary>
        /// Takes supplied arguments and creates a new Obstacle object with the Camera type.
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Obstacle CreateCamera(int xPos, int yPos, int direction)
        {
            return new Obstacle((int)TypeEnum.Camera, 0, direction, xPos, yPos);
        }
        /// <summary>
        /// Takes supplied arguments and creates a list of new Obstacle objects with the Fence type.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public Obstacle CreateFence(int xPos, int yPos, int direction, int length)
        {
            return new Obstacle((int)TypeEnum.Fence, length, direction, xPos, yPos);
        }

        /// <summary>
        /// Takes the supplied arguments and checks if the specified tile falls within the danger field of the Obstacle.
        /// </summary>
        /// <param name="agentX"></param>
        /// <param name="agentY"></param>
        /// <returns></returns>
        public bool CheckDanger(int agentX, int agentY)
        {
            if (agentX == XPos && agentY == YPos) return true;
            else if (Type == (int)TypeEnum.Sensor)
            {
                return Math.Sqrt(((agentX - XPos) ^ 2) + ((agentY - YPos) ^ 2)) <= Radius ? true : false;
            }
            else if (Type == (int)TypeEnum.Camera)
            {
                return CameraCheckDanger(XPos, YPos, agentX, agentY, Direction);
            }
            else if (Type == (int)TypeEnum.Fence)
            {
                for (int i = 0; i < (int)Radius; i++)
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
            else return false;
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
                if (length + (i * numSign) == agentWidth && width == agentWidth)
                {
                    return true;
                }
                for (int j = 1; j <= i; j++)
                {
                    if (agentWidth == width + j && agentLength == length + (i * numSign))
                    {
                        return true;
                    }
                    else if (agentWidth == width - j && agentLength == length + (i * numSign))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
