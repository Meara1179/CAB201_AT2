using CAB201_AT2.Obstacles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2
{
    internal class Pathfinder
    {
        static List<PathNode> openNodes = new List<PathNode>();
        static List<PathNode> closedNodes = new List<PathNode>();

        public Pathfinder()
        {

        }

        public void StartPath(int startX, int startY, int endX, int endY, List<Obstacle> obstacleListArg)
        {
            List<Obstacle> obstacleList = new List<Obstacle>();
            bool atEnd = false;

            if (obstacleListArg.Any()) 
            {
                obstacleList = obstacleListArg;
            }
            PathNode currentNode = new PathNode(startX, startY, startX, startY, endX, endY, obstacleList);
            openNodes.Add(currentNode);
            currentNode.CreateNeighbours();
            // TODO Add limit.
            while (!atEnd)
            {
                //Console.WriteLine($"X: {currentNode.XPos}, Y: {currentNode.YPos}");
                currentNode = FindClosestNode();
                currentNode.CreateNeighbours();
                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);

                if (currentNode.CheckIfAtEnd())
                {
                    atEnd = true;
                    //Console.WriteLine($"X: {currentNode.XPos}, Y: {currentNode.YPos}");
                }
                else
                {
                    foreach (PathNode node in currentNode.neighbours)
                    {
                        if (!closedNodes.Contains(node) && !openNodes.Contains(node))
                        {
                            openNodes.Add(node);
                        }
                    }
                }
            }

            PrintDirections(currentNode);
        }

        private PathNode FindClosestNode()
        {
            List<PathNode> shortestNodes = new List<PathNode>();

            double minCost = openNodes.Min(x => x.Cost);
            foreach (PathNode node in openNodes)
            {
                if (node.Cost == minCost) { shortestNodes.Add(node); }
            }
            if (shortestNodes.Count > 1) 
            {
                double shortestDistToEnd = shortestNodes.Min(x => x.DistanceToEnd);
                foreach (PathNode node in openNodes)
                {
                    if (node.DistanceToEnd != shortestDistToEnd) { shortestNodes.Remove(node); }
                }
                if (shortestNodes.Count > 1)
                {
                    double shortestDistFromStart = shortestNodes.Min(x => x.DistanceFromStart);
                    foreach (PathNode node in openNodes)
                    {
                        if (node.DistanceFromStart != shortestDistFromStart) { shortestNodes.Remove(node); }
                    }
                }
            }
            return shortestNodes[0];
        }

        private void PrintDirections(PathNode node)
        {
            PathNode currentNode = node;
            bool atStart = false;
            List<Tuple<int, int>> cords = new List<Tuple<int, int>>();
            List<Tuple<string, int>> path = new List<Tuple<string, int>>();

            while (!atStart)
            {
                if (currentNode.ParentNode != null)
                {
                    cords.Add(new Tuple<int, int>(currentNode.XPos, currentNode.YPos));
                    currentNode = currentNode.ParentNode;
                }
                else
                {
                    cords.Add(new Tuple<int, int>(currentNode.XPos, currentNode.YPos));
                    atStart = true;
                    cords.Reverse();
                }
            }
            int count = 0;
            string currDir = "";
            string prevDir = "";

            for (int i = 0; i < cords.Count - 1; i++)
            {
                if (cords[i].Item2 < cords[i + 1].Item2)
                {
                    currDir = "north";
                }
                else if (cords[i].Item1 < cords[i + 1].Item1)
                {
                    currDir = "east";
                }
                else if (cords[i].Item2 > cords[i + 1].Item2)
                {
                    currDir = "south";
                }
                else if (cords[i].Item1 > cords[i + 1].Item1)
                {
                    currDir = "west";
                }
                
                if (i == 0) { prevDir =  currDir; }

                if (currDir == prevDir)
                {
                    count++;
                    if (i == cords.Count - 2)
                    {
                        count++;
                        path.Add(new Tuple<string, int>(currDir, count));
                    }
                }
                else
                {
                    count++;
                    path.Add(new Tuple<string, int>(prevDir, count));
                    prevDir = currDir;
                    count = 0;
                }
            }

            foreach (Tuple<string, int> p in path)
            {
                if (p.Item2 == 1)
                {
                    Console.WriteLine($"Head {p.Item1} for {p.Item2} klick.");
                }
                else
                {
                    Console.WriteLine($"Head {p.Item1} for {p.Item2} klicks.");
                }
                
            }    
        }
    }
}
