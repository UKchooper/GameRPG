using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GameRPG.ViewModel
{
    public class GameViewModel : BindableBase
    {
        private int CurrentNorthSouth = 0;
        private int CurrentEastWest = 0;
        private int maxX;
        private int maxY;
        private int minX;
        private int minY;
        private string mapTitle;
        private string mapEvent;
        private string mapDescription;
        private string mapImage;
        int mapListIndex;

        List<Map> mapLists = new List<Map>();
        public GameViewModel()
        {
            this.NorthButtonCommand = new DelegateCommand(this.NorthButton);
            this.EastButtonCommand = new DelegateCommand(this.EastButton);
            this.SouthhButtonCommand = new DelegateCommand(this.SouthButton);
            this.WestButtonCommand = new DelegateCommand(this.WestButton);

            AddLocations();
            MaxCoordinates();
            UpdateMapLocation();
        }

        public ICommand NorthButtonCommand { get; set; }
        public ICommand EastButtonCommand { get; set; }
        public ICommand SouthhButtonCommand { get; set; }
        public ICommand WestButtonCommand { get; set; }
        public string MapTitle
        {
            get
            {
                return mapTitle;
            }
            set
            {
                mapTitle = value;
                RaisePropertyChanged(nameof(this.MapTitle));
            }
        }
        public string MapEvent
        {
            get
            {
                return mapEvent;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(mapLists[mapListIndex].LocationEvent))
                {
                    mapEvent = value;
                }
                else
                {
                    mapEvent = "None";
                }
                RaisePropertyChanged(nameof(this.MapEvent));
            }
        }
        public string MapDescription
        {
            get
            {
                return mapDescription;
            }
            set
            {
                mapDescription = value;
                RaisePropertyChanged(nameof(this.MapDescription));
            }
        }
        public string MapImage
        {
            get
            {
                return mapImage;
            }
            set
            {
                mapImage = value;
                RaisePropertyChanged(nameof(this.MapImage));
            }
        }

        public void AddLocations()
        {
            mapLists.Add(new Map(0, 0, "Starting location - Grassland #1", "Grass", ""));
            mapLists.Add(new Map(1, 0, "Ocean #1", "Ocean", "Shop"));
            mapLists.Add(new Map(2, 0, "Desert #1", "Sand", ""));
            mapLists.Add(new Map(1, 1, "Desert #2", "Sand", "Fight #1"));
            mapLists.Add(new Map(2, 1, "Grassland #2", "Grass", ""));
            mapLists.Add(new Map(2, 2, "Grassland #3", "Grass", ""));
            mapLists.Add(new Map(0, 1, "Desert #3", "Sand", "Fight #2"));
            mapLists.Add(new Map(0, 2, "Ocean #2", "Ocean", "Quest #1"));
            mapLists.Add(new Map(1, 2, "Ocean #3", "Ocean", ""));
        }

        public void MaxCoordinates()
        {
            maxX = mapLists.Max(map => map.CoorX);
            maxY = mapLists.Max(map => map.CoorY);
            minX = mapLists.Min(map => map.CoorX);
            minY = mapLists.Min(map => map.CoorY);
        }

        public void UpdateMapLocation()
        {
            mapListIndex = mapLists.FindIndex(i => i.CoorY == CurrentNorthSouth && i.CoorX == CurrentEastWest);

            MapImage = $"pack://application:,,,/Images/locations/{mapLists[mapListIndex].Description.ToLower()}.png";
            MapTitle = mapLists[mapListIndex].Title;
            MapEvent = mapLists[mapListIndex].LocationEvent;
            MapDescription = mapLists[mapListIndex].Description;
        }

        private void NorthButton()
        {
            CurrentNorthSouth++;

            UpdateMapLocation();
        }

        private void EastButton()
        {
            CurrentEastWest++;

            UpdateMapLocation();
        }
        private void SouthButton()
        {
            CurrentNorthSouth--;

            UpdateMapLocation();
        }

        private void WestButton()
        {
            CurrentEastWest--;

            UpdateMapLocation();
        }

    }
}
