﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameRPG
{
    public class Location
    {
        public int CoordinateY { get; set; }
        public int CoordinateX { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LocationEvent { get; set; }

        public Location(int coordinateY, int coordinateX, string title, string description, string locationEvent)
        {
            CoordinateY = coordinateY;
            CoordinateX = coordinateX;
            Title = title;
            Description = description;
            LocationEvent = locationEvent;
        }
    }
}
