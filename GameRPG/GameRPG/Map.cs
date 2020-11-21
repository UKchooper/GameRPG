using System;
using System.Collections.Generic;
using System.Text;

namespace GameRPG
{
    public class Map
    {
        public int CoorY { get; set; }
        public int CoorX { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LocationEvent { get; set; }

        public Map(int coorY, int coorX, string title, string description, string locationEvent)
        {
            CoorY = coorY;
            CoorX = coorX;
            Title = title;
            Description = description;
            LocationEvent = locationEvent;
        }
    }
}
