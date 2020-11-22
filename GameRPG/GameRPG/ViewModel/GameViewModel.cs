using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
        private bool northButtonEnabled;
        private bool eastButtonEnabled;
        private bool southButtonEnabled;
        private bool westButtonEnabled;

        int mapListIndex;
        int selectedEventIndex;

        List<Map> mapLists = new List<Map>();
        List<Quest> questList = new List<Quest>();

        public GameViewModel()
        {
            this.NorthButtonCommand = new DelegateCommand(this.NorthButton);
            this.EastButtonCommand = new DelegateCommand(this.EastButton);
            this.SouthButtonCommand = new DelegateCommand(this.SouthButton);
            this.WestButtonCommand = new DelegateCommand(this.WestButton);
            this.EventLocatorCommand = new DelegateCommand(this.EventLocatorButton);

            AddLocations();
            MaxCoordinates();
            UpdateMapLocation();
            AddQuests();
            LocationButtonDefaults();
            AddEvents();
        }

        public ICommand NorthButtonCommand { get; set; }
        
        public ICommand EastButtonCommand { get; set; }
        
        public ICommand SouthButtonCommand { get; set; }
        
        public ICommand WestButtonCommand { get; set; }

        public ICommand EventLocatorCommand { get; set; }

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

        public bool NorthButtonEnabled
        {
            get
            {
                return northButtonEnabled;
            }
            set
            {
                northButtonEnabled = value;
                RaisePropertyChanged(nameof(this.NorthButtonEnabled));
            }
        }

        public bool EastButtonEnabled
        {
            get
            {
                return eastButtonEnabled;
            }
            set
            {
                eastButtonEnabled = value;
                RaisePropertyChanged(nameof(this.EastButtonEnabled));
            }
        }

        public bool SouthButtonEnabled
        {
            get
            {
                return southButtonEnabled;
            }
            set
            {
                southButtonEnabled = value;
                RaisePropertyChanged(nameof(this.SouthButtonEnabled));
            }
        }

        public bool WestButtonEnabled
        {
            get
            {
                return westButtonEnabled;
            }
            set
            {
                westButtonEnabled = value;
                RaisePropertyChanged(nameof(this.WestButtonEnabled));
            }
        }

        public int SelectedEventIndex
        {
            get
            {
                return selectedEventIndex;
            }
            set
            {
                selectedEventIndex = value;
                RaisePropertyChanged(nameof(SelectedEventIndex));
            }
        }

        public ObservableCollection<string> QuestTitles { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> EventNames { get; } = new ObservableCollection<string>();

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
            maxX = mapLists.Max(map => map.CoordinateX);
            maxY = mapLists.Max(map => map.CoordinateY);
            minX = mapLists.Min(map => map.CoordinateX);
            minY = mapLists.Min(map => map.CoordinateY);
        }

        public void UpdateMapLocation()
        {
            mapListIndex = mapLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth && i.CoordinateX == CurrentEastWest);

            MapImage = $"pack://application:,,,/Images/locations/{mapLists[mapListIndex].Description.ToLower()}.png";
            MapTitle = mapLists[mapListIndex].Title;
            MapEvent = mapLists[mapListIndex].LocationEvent;
            MapDescription = mapLists[mapListIndex].Description;
        }

        private void NorthButton()
        {
            CurrentNorthSouth++;

            UpdateMapLocation();

            if(CurrentNorthSouth == maxX)
            {
                NorthButtonEnabled = false;
            }
            else
            {
                NorthButtonEnabled = true;
            }

            SouthButtonEnabled = true;
        }

        private void EastButton()
        {
            CurrentEastWest++;

            UpdateMapLocation();

            if(CurrentEastWest == maxY)
            {
                EastButtonEnabled = false;
            }
            else
            {
                EastButtonEnabled = true;
            }

            WestButtonEnabled = true;
        }

        private void SouthButton()
        {
            CurrentNorthSouth--;

            UpdateMapLocation();

            if (CurrentNorthSouth == minX)
            {
                SouthButtonEnabled = false;
            }
            else
            {
                SouthButtonEnabled = true;
            }

            NorthButtonEnabled = true;
        }

        private void WestButton()
        {
            CurrentEastWest--;

            UpdateMapLocation();

            if (CurrentEastWest == minY)
            {
                WestButtonEnabled = false;
            }
            else
            {
                WestButtonEnabled = true;
            }

            EastButtonEnabled = true;
        }

        private void EventLocatorButton()
        {
            var homeLocation = string.Empty;

            int currentMapListIndex = mapLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth && i.CoordinateX == CurrentEastWest);

            int itemIndex = mapLists.FindIndex(i => i.LocationEvent.Contains(EventNames[SelectedEventIndex]));

            if (currentMapListIndex == itemIndex)
            {
                MessageBox.Show("You're already at this location");
            }
            else
            {
                for (int i = CurrentNorthSouth; i > mapLists[itemIndex].CoordinateY; i--)
                {
                    homeLocation += "South\n";
                }

                for (int i = CurrentEastWest; i > mapLists[itemIndex].CoordinateX; i--)
                {
                    homeLocation += "West\n";
                }

                for (int i = CurrentNorthSouth; i < mapLists[itemIndex].CoordinateY; i++)
                {
                    homeLocation += "North\n";
                }

                for (int i = CurrentEastWest; i < mapLists[itemIndex].CoordinateX; i++)
                {
                    homeLocation += "East\n";
                }

                MessageBox.Show($"Navigate:\n{homeLocation}");
            }
        }

        public void AddQuests()
        {
            questList.Add(new Quest("Search the map", "Find out what is out there!", 1, "Peach"));
            questList.Add(new Quest("Defeat Bob", "Defeat the best Bob", 2, "Potato"));
            questList.Add(new Quest("Eat food", "Becafeful!", 3, "Sword"));
            questList.Add(new Quest("Speak to Malcolm", "He might not be who he says he is", 4, "Better sword"));
            questList.Add(new Quest("Die", "Oh dear", 5, "grape"));

            int number = 1;

            foreach (var quest in questList)
            {
                QuestTitles.Add($"Quest {number}: {quest.Title}");
                number++;
            }
        }

        public void LocationButtonDefaults()
        {
            if (CurrentNorthSouth == 0)
            {
                NorthButtonEnabled = true;
            }

            if (CurrentNorthSouth == 0)
            {
                EastButtonEnabled = true;
            }

            if (CurrentNorthSouth == 0)
            {
                SouthButtonEnabled = false;
            }

            if (CurrentEastWest == 0)
            {
                WestButtonEnabled = false;
            }
        }

        public void AddEvents()
        {
            foreach (var events in mapLists)
            {
                if (!string.IsNullOrEmpty(events.LocationEvent))
                {
                    EventNames.Add(events.LocationEvent);
                }
            }
        }
    }
}
