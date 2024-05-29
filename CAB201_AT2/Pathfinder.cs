using CAB201_AT2.Obstacles;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2
{
    internal class Pathfinder
    {

        public Tuple<Point, Point> CreateStartAndTarget(int startX, int startY, int endX, int endY)
        {
            Point start = new Point(startX, startY);
            Point target = new Point(endX, endY);

            return Tuple.Create(start, target);
        }

        public List<Node> StartPath(Point start, Point target)
        {
            List<Node> openNodes = new List<Node>();
            List<Node> closedNodes = new List<Node>();

            Node startNode = new Node(start);
            startNode.G = 0;
            startNode.H = startNode.FindDist(target);

            openNodes.Add(startNode);
            bool pathFound = start.Equals(target);

            Node? endNode = null;
            int maxIteration = 6000;
            int currentIteration = 0;

            while (openNodes.Any() && (!pathFound) && (++currentIteration <= maxIteration))
            {
                if (currentIteration == maxIteration - 1)
                {
                    Console.WriteLine("SIGH");
                }

                Node current = this.FindLowestCost(openNodes);
                closedNodes.Add(current);

                foreach (Point p in current.Neighbours)
                {
                    if (p.CheckDanger()) continue;

                    Node neighbour = new Node(p);
                    endNode = neighbour;

                    double newG = current.G + 1;
                    double newH = neighbour.FindDist(target);
                    
                    void updateNeighbour()
                    {
                        neighbour.G = newG;
                        neighbour.H = newH;
                        neighbour.ParentNode = current;
                    }

                    //bool canAddOpen = true;
                    //for (int i = 0; i < openNodes.Count; i++)
                    //{
                    //    if (neighbour.Equals(openNodes[i]))
                    //    {
                    //        canAddOpen = false;
                    //        break;
                    //    }
                    //}
                    //bool canAddClosed = true;
                    //for (int i = 0; i < closedNodes.Count; i++)
                    //{
                    //    if (neighbour.Equals(closedNodes[i]))
                    //    {
                    //        canAddClosed = false;
                    //        break;
                    //    }
                    //}

                    //Point neighbourPoint = new Point(neighbour.X, neighbour.Y);
                    if (neighbour.Equals(target))
                    {
                        updateNeighbour();
                        pathFound = true;
                        Console.WriteLine("FOUND");
                        break;
                    }
                    else if (!openNodes.Contains(neighbour) && !closedNodes.Contains(neighbour))
                    {
                        updateNeighbour();
                        openNodes.Add(neighbour);
                    }
                    //else if (neighbour.G > newG)
                    //{
                    //    updateNeighbour();
                    //    openNodes.Add(neighbour);
                    //    closedNodes.Remove(neighbour);
                    //}
                }
            }

            List<Node> path = new List<Node>();
            if (pathFound)
            {
                Node? node = endNode;

                while (node != null)
                {
                    path.Add(node);
                    node = node.ParentNode;
                }
                path.Reverse();
            }
            return path;
        }

        public void ProcessPath(List<Node> path)
        {
            List<Tuple<string, int>> directions = new List<Tuple<string, int>>();
            string currDir = "";
            string prevDir = "";
            int count = -1;

            for (int i = 0; i < path.Count - 1; i++)
            {
                count++;
                if (path[i].Y < path[i + 1].Y) { currDir = "north"; }
                else if (path[i].X < path[i + 1].X) { currDir = "east"; }
                else if (path[i].Y > path[i + 1].Y) { currDir = "south"; }
                else if (path[i].X > path[i + 1].X) { currDir = "west"; }
                if (i == 0) { prevDir = currDir; }

                if (currDir != prevDir)
                {
                    directions.Add(new Tuple<string, int>(prevDir, count));
                    count = 0;
                }
                if (i == path.Count - 2) directions.Add(new Tuple<string, int>(prevDir, count + 1));
                prevDir = currDir;
            }

            Console.WriteLine("The following path will take you to the objective:");
            foreach(Tuple<string, int> s in directions)
            {
                if (s.Item2 <= 1)
                {
                    Console.WriteLine($"Head {s.Item1} for {s.Item2} klick");
                }
                else
                {
                    Console.WriteLine($"Head {s.Item1} for {s.Item2} klicks");
                }
            }
        }

        private Node FindLowestCost(List<Node> open)
        {
            double minCost = double.MaxValue;
            double minH = double.MaxValue;
            Node? minNode = null;

            foreach (Node node in open)
            {
                if (node.F < minCost)
                {
                    minNode = node;
                    minCost = (double)node.F;
                    minH = (double)node.H;
                }
                else if (node.F ==  minCost && node.H < minH)
                {
                    minNode = node;
                    minCost = (double)node.F;
                    minH = (double)node.H;
                }
            }
            open.Remove(minNode);
            return minNode;
        }
    }
}
