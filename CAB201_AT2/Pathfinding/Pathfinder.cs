using CAB201_AT2.Obstacles;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2.Pathfinding
{
    internal class Pathfinder
    {
        /// <summary>
        /// Takes the supplied pair of coordinates and returns of Points
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        public Tuple<Point, Point> CreateStartAndTarget(int startX, int startY, int endX, int endY)
        {
            Point start = new Point(startX, startY);
            Point target = new Point(endX, endY);

            return Tuple.Create(start, target);
        }

        /// <summary>
        /// Attempts to find a viable path between both points, avoiding any obstacles if possible.
        /// Returns a List of Nodes forming a path from start to target.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <returns></returns>
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
            const int MAX_ITERATIONS = 10000;
            int currentIteration = 0;

            // Continues looping as long as a path hasn't been found, there are still nodes in the open list,
            // and the loop hasn't iterated for the specified limit
            while (openNodes.Any() && !pathFound && ++currentIteration <= MAX_ITERATIONS)
            {
                if (currentIteration == MAX_ITERATIONS - 1)
                {
                    Console.WriteLine("Timed Out");
                }

                Node current = FindLowestCost(openNodes);
                closedNodes.Add(current);

                // Iterates through each neighbour of the current node and checks if already in either the open or closed node list.
                foreach (Point p in current.neighbours)
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

                    if (neighbour.Equals(target))
                    {
                        updateNeighbour();
                        pathFound = true;
                        break;
                    }
                    else if (!openNodes.Contains(neighbour) && !closedNodes.Contains(neighbour))
                    {
                        updateNeighbour();
                        openNodes.Add(neighbour);
                    }
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
            else
            {
                Console.WriteLine("There is no safe path to the objective.");
            }
            return path;
        }

        /// <summary>
        /// Determines the cardinal directions between the Nodes in the path, then prints out directions instructing 
        /// how to travel from the start to the target.
        /// </summary>
        /// <param name="path"></param>
        public void ProcessPath(List<Node> path)
        {
            List<List<string>> directionsList = new List<List<string>>();
            List<string> directions = new List<string>();
            string currDir = "";

            for (int i = 0; i < path.Count - 1; i++)
            {
                if (path[i].Y < path[i + 1].Y) { currDir = "north"; }
                else if (path[i].X < path[i + 1].X) { currDir = "east"; }
                else if (path[i].Y > path[i + 1].Y) { currDir = "south"; }
                else if (path[i].X > path[i + 1].X) { currDir = "west"; }

                if (directions.Contains(currDir) || i == 0)
                {
                    directions.Add(currDir);
                }
                else if (!directions.Contains(currDir))
                {
                    directionsList.Add(directions);
                    directions = new List<string>();
                    directions.Add(currDir);
                }
                if (i == path.Count - 2)
                {
                    Console.WriteLine("The following path will take you to the objective:");
                    directionsList.Add(directions);
                }
            }

            foreach (var d in directionsList)
            {
                if (d.Count <= 1)
                {
                    Console.WriteLine($"Head {d[0]} for {d.Count} klick.");
                }
                else
                {
                    Console.WriteLine($"Head {d[0]} for {d.Count} klicks.");
                }
            }
        }

        // Returns the Node in the open list that either has the lowest cost, or the same cost and lower H.
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
                else if (node.F == minCost && node.H < minH)
                {
                    minNode = node;
                    minCost = (double)node.F;
                    minH = (double)node.H;
                }
            }
            if (minNode != null)
            {
                open.Remove(minNode);
                return minNode;
            }
            else return new Node(new Point(0, 0), 0, 0);

        }
    }
}
