using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GameRPG.ViewModel
{
    public class GameViewModel : BindableBase
    {
        private string name;
        private string type;
        private int level;
        private int xp;
        private int hp;
        private int strength;
        private int intelligence;
        private int agility;

        private int CurrentNorthSouth = 0;
        private int CurrentEastWest = 0;
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

        public List<Location> locationLists = new List<Location>();
        public List<Quest> questList;
        // Unused atm
        //List<Enemy> enemyList = new List<Enemy>();

        public GameViewModel()
        {
            this.NorthButtonCommand = new DelegateCommand(this.NorthButton);
            this.EastButtonCommand = new DelegateCommand(this.EastButton);
            this.SouthButtonCommand = new DelegateCommand(this.SouthButton);
            this.WestButtonCommand = new DelegateCommand(this.WestButton);
            this.EventLocatorCommand = new DelegateCommand(this.EventLocatorButton);

            //AddLocations();

            RandomLocations();

            UpdateDirectionalButtons();
            UpdateMapLocation();
            AddQuests();
            AddEvents();
            FakePerson();
        }

        public ICommand NorthButtonCommand { get; set; }

        public ICommand EastButtonCommand { get; set; }

        public ICommand SouthButtonCommand { get; set; }

        public ICommand WestButtonCommand { get; set; }

        public ICommand EventLocatorCommand { get; set; }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(this.Name));
            }
        }

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                RaisePropertyChanged(nameof(this.Type));
            }
        }

        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                RaisePropertyChanged(nameof(this.Level));
            }
        }

        public int XP
        {
            get
            {
                return xp;
            }
            set
            {
                xp = value;
                RaisePropertyChanged(nameof(this.XP));
            }
        }

        public int HP
        {
            get
            {
                return hp;
            }
            set
            {
                hp = value;
                RaisePropertyChanged(nameof(this.HP));
            }
        }

        public int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
                RaisePropertyChanged(nameof(this.Strength));
            }
        }

        public int Intelligence
        {
            get
            {
                return intelligence;
            }
            set
            {
                intelligence = value;
                RaisePropertyChanged(nameof(this.Intelligence));
            }
        }

        public int Agility
        {
            get
            {
                return agility;
            }
            set
            {
                agility = value;
                RaisePropertyChanged(nameof(this.Agility));
            }
        }

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
                if (!string.IsNullOrWhiteSpace(locationLists[mapListIndex].LocationEvent))
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

        public TrulyObservableCollection<Quest> Quest { get; set; } = new TrulyObservableCollection<Quest>();

        public ObservableCollection<string> EventNames { get; } = new ObservableCollection<string>();

        public void AddLocations()
        {
            const string locationFile = @"C:\Users\carl.hooper\Desktop\GameoStuffo\TextFiles\Locations.txt";

            TxtReader locationReader = new TxtReader(locationFile);
            locationLists = locationReader.ReadLocations();
        }

        public void UpdateMapLocation()
        {
            mapListIndex = locationLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth && i.CoordinateX == CurrentEastWest);

            MapImage = $"pack://application:,,,/Images/locations/{locationLists[mapListIndex].Description.ToLower()}.png";
            MapTitle = locationLists[mapListIndex].Title;
            MapEvent = locationLists[mapListIndex].LocationEvent;
            MapDescription = locationLists[mapListIndex].Description;
        }

        private void NorthButton()
        {
            CurrentNorthSouth++;

            UpdateMapLocation();
            UpdateDirectionalButtons();
            TestingFight();
            TestingActivatingSecondQuest();
        }

        private void EastButton()
        {
            CurrentEastWest++;

            UpdateMapLocation();
            UpdateDirectionalButtons();
            TestingFight();
            TestingActivatingSecondQuest();
        }

        private void SouthButton()
        {
            CurrentNorthSouth--;

            UpdateMapLocation();
            UpdateDirectionalButtons();
            TestingFight();
            TestingActivatingSecondQuest();
        }

        private void WestButton()
        {
            CurrentEastWest--;

            UpdateMapLocation();
            UpdateDirectionalButtons();
            TestingFight();
            TestingActivatingSecondQuest();
        }

        public void UpdateDirectionalButtons()
        {
            int mapListIndexNorthMax = locationLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth + 1 && i.CoordinateX == CurrentEastWest);
            int mapListIndexEastMax = locationLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth && i.CoordinateX == CurrentEastWest + 1);
            int mapListIndexSouthMax = locationLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth - 1 && i.CoordinateX == CurrentEastWest);
            int mapListIndexWestMax = locationLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth && i.CoordinateX == CurrentEastWest - 1);

            if (mapListIndexNorthMax == -1)
            {
                NorthButtonEnabled = false;
            }
            else
            {
                NorthButtonEnabled = true;
            }

            if (mapListIndexEastMax == -1)
            {
                EastButtonEnabled = false;
            }
            else
            {
                EastButtonEnabled = true;
            }

            if (mapListIndexSouthMax == -1)
            {
                SouthButtonEnabled = false;
            }
            else
            {
                SouthButtonEnabled = true;
            }

            if (mapListIndexWestMax == -1)
            {
                WestButtonEnabled = false;
            }
            else
            {
                WestButtonEnabled = true;
            }
        }

        private void EventLocatorButton()
        {
            var homeLocation = string.Empty;

            int currentMapListIndex = locationLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth && i.CoordinateX == CurrentEastWest);

            int itemIndex = locationLists.FindIndex(i => i.LocationEvent.Contains(EventNames[SelectedEventIndex]));

            if (currentMapListIndex == itemIndex)
            {
                MessageBox.Show("You're already at this location");
            }
            else
            {
                for (int i = CurrentNorthSouth; i > locationLists[itemIndex].CoordinateY; i--)
                {
                    homeLocation += "South\n";
                }

                for (int i = CurrentEastWest; i > locationLists[itemIndex].CoordinateX; i--)
                {
                    homeLocation += "West\n";
                }

                for (int i = CurrentNorthSouth; i < locationLists[itemIndex].CoordinateY; i++)
                {
                    homeLocation += "North\n";
                }

                for (int i = CurrentEastWest; i < locationLists[itemIndex].CoordinateX; i++)
                {
                    homeLocation += "East\n";
                }

                MessageBox.Show($"Navigate:\n{homeLocation}");
            }
        }

        public void AddQuests()
        {
            const string questFile = @"C:\Users\carl.hooper\Desktop\GameoStuffo\TextFiles\Quests.txt";

            TxtReader questReader = new TxtReader(questFile);
            questList = questReader.ReadQuests();

            AddActiveQuests();
        }

        public void AddEvents()
        {
            foreach (var events in locationLists)
            {
                if (!string.IsNullOrEmpty(events.LocationEvent))
                {
                    EventNames.Add(events.LocationEvent);
                }
            }
        }

        public void AddActiveQuests()
        {
            foreach (var quest in questList)
            {
                if (!Quest.Contains(quest) && (quest.IsActive))
                {
                    Quest.Add(quest);
                }
            }
        }

        // Redo FakePerson, TestingFight and TestingActivatingSecondQuest, Add Enemies

        public void FakePerson()
        {
            Name = "Carl";
            Type = "Warrior";
            Level = 1;
            XP = 0;
            HP = 100;
            Strength = 3;
            Intelligence = 4;
            Agility = 5;
        }

        public void TestingFight()
        {
            if (locationLists[mapListIndex].LocationEvent == "Fight #1" || locationLists[mapListIndex].LocationEvent == "Fight #2")
            {
                HP--;
                XP += 5;
            }
        }

        public void TestingActivatingSecondQuest()
        {
            if (mapListIndex == locationLists.FindIndex(ml => ml.LocationEvent.Contains("Quest #1")))
            {
                foreach (var quest in Quest.Where(x => x.Title == "1. Search the map"))
                {
                    quest.IsComplete = true;
                }

                questList[1].IsActive = true;

                AddActiveQuests();
            }
        }

        public void AddEnemies()
        {
           // enemyList.Add(new Enemy())
        }

        public void RandomLocations()
        {
            locationLists.Add(new Location(0, 0, "Starting location - Grassland #1", "Grass", ""));

            Random random = new Random();

            int index = 2;

            HashSet<int> coordYList = new HashSet<int>();
            HashSet<int> coordXList = new HashSet<int>();

            do
            {
                foreach (var coord in locationLists)
                {
                    coordYList.Add(coord.CoordinateY);
                }

                foreach (var coord in locationLists)
                {
                    coordXList.Add(coord.CoordinateX);
                }

                int randomY = random.Next(coordYList.Min() - 1, coordYList.Max() + 1);
                int randomX = random.Next(coordXList.Min() - 1, coordXList.Max() + 1);

                int randomNum = random.Next(0, 4);

                if (randomNum == 0)
                {
                    int temp = randomY + 1;

                    if(!(locationLists.FindIndex(i => i.CoordinateY == randomY && i.CoordinateX == randomX) == -1))
                    {
                        if (locationLists.FindIndex(i => i.CoordinateY == temp && i.CoordinateX == randomX) == -1)
                        {
                            locationLists.Add(new Location(temp, randomX, $"Random location #{index}", "Grass", ""));
                            index++;
                        }
                        
                    }
                }

                if (randomNum == 1)
                {
                    int temp = randomX + 1;

                    if(!(locationLists.FindIndex(i => i.CoordinateY == randomY && i.CoordinateX == randomX) == -1))
                    {
                        if(locationLists.FindIndex(i => i.CoordinateY == randomY && i.CoordinateX == temp) == -1)
                        {
                            locationLists.Add(new Location(randomY, temp, $"Random location #{index}", "Grass", ""));
                            index++;
                        }
                    }
                }
                
                if (randomNum == 2)
                {
                    int temp = randomY - 1;

                    if(!(locationLists.FindIndex(i => i.CoordinateY == randomY && i.CoordinateX == randomX) == -1))
                    {
                        if(locationLists.FindIndex(i => i.CoordinateY == temp && i.CoordinateX == randomX) == -1)
                        {
                            locationLists.Add(new Location(temp, randomX, $"Random location #{index}", "Grass", ""));
                            index++;
                        }
                    }
                }
                
                if (randomNum == 3)
                {
                    int temp = randomX - 1;

                    if(!(locationLists.FindIndex(i => i.CoordinateY == randomY && i.CoordinateX == randomX) == -1))
                    {
                        if (!(locationLists.FindIndex(i => i.CoordinateY == randomY && i.CoordinateX == temp) == -1))
                        {
                            locationLists.Add(new Location(randomY, temp, $"Random location #{index}", "Grass", ""));
                            index++;
                        }
                    }
                }
            }
            while (locationLists.Count < 10);
        }
    }
}
