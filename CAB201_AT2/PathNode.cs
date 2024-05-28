using CAB201_AT2.Obstacles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2
{
    internal class PathNode
    {
        public int XPos {  get; set; }
        public int YPos { get; set; }

        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }

        public double DistanceToEnd { get; set; }
        public double DistanceFromStart { get; set; }
        public double Cost {  get; set; }

        public bool IsDanger = false;
        public PathNode? ParentNode { get; set; }

        public List<PathNode> neighbours = new List<PathNode>();
        List<Obstacle> obstacleList = new List<Obstacle>();

        public PathNode(int xPos, int yPos, int startX, int startY, int endX, int endY, List<Obstacle> obstacleListArg, PathNode? parentNode = null)
        {
            XPos = xPos;
            YPos = yPos;
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;

            DistanceToEnd = (Math.Abs(endX - xPos) + Math.Abs(endY - yPos));
            DistanceFromStart = (Math.Abs(startX - xPos) + Math.Abs(startY - yPos));

            Cost = DistanceFromStart + DistanceToEnd;

            obstacleList = obstacleListArg;


            foreach (Obstacle obstacle in obstacleList)
            {
                IsDanger = obstacle.CheckDanger(xPos, yPos);
            }

            ParentNode = parentNode;
        }

        public void CreateNeighbours()
        {
            //North
            neighbours.Add(new PathNode(XPos, YPos + 1, StartX, StartY, EndX, EndY, obstacleList, this));
            //East
            neighbours.Add(new PathNode(XPos + 1, YPos, StartX, StartY, EndX, EndY, obstacleList, this));
            //South
            neighbours.Add(new PathNode(XPos, YPos - 1, StartX, StartY, EndX, EndY, obstacleList, this));
            //West
            neighbours.Add(new PathNode(XPos - 1, YPos, StartX, StartY, EndX, EndY, obstacleList, this));

            List<PathNode> holdNeighboru = neighbours.ToList();
            foreach (PathNode node in holdNeighboru)
            {
                if (node.IsDanger)
                {
                    neighbours.Remove(node);
                }
            }
        }

        public bool CheckIfAtEnd()
        {
            if (XPos == EndX && YPos == EndY)
            {
                return true;
            }
            else return false;
        }
    }
}
