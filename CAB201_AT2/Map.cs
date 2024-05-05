using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2
{
    internal class Map
    {
        static public List<Obstacle> obstaclesList = new List<Obstacle>();
        List<Movement> bestRoute = new List<Movement>();
        int bestMoveCount;

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
                    foreach (Obstacle ob in obstaclesList)
                    {
                        if (ob.CheckDanger(swX + i, swY + j) == true)
                        {
                            switch (ob.Type)
                            {
                                case (int)TypeEnum.Guard:
                                    map[i].Add("G");
                                    break;
                                case (int)TypeEnum.Sensor:
                                    map[i].Add("S");
                                    break;
                                case (int)TypeEnum.Camera:
                                    map[i].Add("C");
                                    break;
                                case (int)TypeEnum.Fence:
                                    map[i].Add("F");
                                    break;
                            }
                        }
                        else map[i].Add(".");
                    }
                }
            }

            return map;
        }
    }
}
