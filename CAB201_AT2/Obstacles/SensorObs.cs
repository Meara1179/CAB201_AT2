﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_AT2.Obstacles
{
    internal class SensorObs : Obstacle
    {
        public double Radius { get; set; }

        public SensorObs(int xPos, int yPos, double radius)
        {
            XPos = xPos;
            YPos = yPos;
            Radius = radius;
        }

        public override bool CheckDanger(int agentX, int agentY)
        {
            if (agentX == XPos && agentY == YPos) return true;
            else
            {
                bool danger = Math.Sqrt(Math.Pow((agentX - XPos), 2) + Math.Pow((agentY - YPos), 2)) <= Radius ? true : false;
                return danger;
            }
        }
        public override string MapMarker()
        {
            return ("S");
        }
    }
}
