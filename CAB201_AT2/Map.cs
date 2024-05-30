using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAB201_AT2.Obstacles;

namespace CAB201_AT2
{
    internal class Map
    {
        static public List<Obstacle> obstaclesList = new List<Obstacle>();

        /// <summary>
        /// Adds the supplied obstacle to the obstacle list.
        /// </summary>
        /// <param name="ob"></param>
        public void AddObstacle(Obstacle ob)
        {
            obstaclesList.Add(ob);
        }

        /// <summary>
        /// Creates a 2d List of the specified region and iterates through the obstacle list adding any that fall within the region to the 2d List.
        /// </summary>
        /// <param name="swX"></param>
        /// <param name="swY"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>

        public List<List<string>> GenerateVisualMap(int swX, int swY, int width, int height)
        {
            List<List<string>> map = new List<List<string>>();

            for (int i = 0; i < width; i++)
            {
                map.Add(new List<string>());
                for (int j = 0; j < height; j++)
                {
                    map[i].Add("");
                    if (obstaclesList.Any())
                    {
                        foreach (Obstacle ob in obstaclesList)
                        {
                            if (ob.CheckDanger(swX + i, swY + j) == true)
                            {
                                map[i][j] = ob.MapMarker();
                            }
                            else if (map[i][j] == "")
                            {
                                map[i][j] = ".";
                            }
                        }
                    }
                    else map[i][j] = ".";
                }
            }

            return map;
        }

        public List<Obstacle> ReturnObstacleList()
        {
            return obstaclesList;
        }

        public bool CheckIfDanger(int xPos, int yPos)
        {
            foreach (Obstacle ob in obstaclesList)
            {
                if (ob.CheckDanger(xPos, yPos))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
