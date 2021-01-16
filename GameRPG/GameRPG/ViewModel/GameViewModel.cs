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

        private int selectedCharacterIndex;
        private List<Character> characters = CharacterSelectionViewModel.characters;

        int mapListIndex;
        int previousMapListIndex;
        int selectedEventIndex;

        public List<Location> locationLists = new List<Location>();
        public List<Character> characterInformation = new List<Character> ();
        public List<Quest> questList;

        // Unused atm
        //List<Enemy> enemyList = new List<Enemy>();

        public GameViewModel(MainWindowViewModel main)
        {
            this.NorthButtonCommand = new DelegateCommand(this.NorthButton);
            this.EastButtonCommand = new DelegateCommand(this.EastButton);
            this.SouthButtonCommand = new DelegateCommand(this.SouthButton);
            this.WestButtonCommand = new DelegateCommand(this.WestButton);
            this.EventLocatorCommand = new DelegateCommand(this.EventLocatorButton);

            // this adds specific locations
            //AddLocationsFromFile();

            SetupRandomLocations();
            AddCharacterFromFile();
            UpdateDirectionalButtons();
            UpdateMapLocation();
            AddQuests();
            AddEvents();
            //FakePerson();
            UpdateBlockedMapLocations();
            UpdateMap();
        }

        public ICommand NorthButtonCommand { get; set; }

        public ICommand EastButtonCommand { get; set; }

        public ICommand SouthButtonCommand { get; set; }

        public ICommand WestButtonCommand { get; set; }

        public ICommand EventLocatorCommand { get; set; }

        public int SelectedCharacterIndex
        {
            get
            {
                return selectedCharacterIndex;
            }
            set
            {
                selectedCharacterIndex = value;
                RaisePropertyChanged(nameof(this.SelectedCharacterIndex));
            }
        }

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

        public void AddLocationsFromFile()
        {
            const string locationFile = @"C:\Users\carl.hooper\Desktop\GameoStuffo\TextFiles\Locations.txt";

            TxtReader locationReader = new TxtReader(locationFile);
            locationLists = locationReader.ReadLocations();
        }

        public void AddCharacterFromFile()
        {
            const string characterFile = @"C:\Users\carl.hooper\Desktop\GameoStuffo\TextFiles\SaveCharacter.txt";

            TxtReader characterReader = new TxtReader(characterFile);
            characterInformation = characterReader.ReadCharacters();

            FakePerson();
        }

        public void UpdateMapLocation()
        {
            mapListIndex = locationLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth && i.CoordinateX == CurrentEastWest);

            //previousMapListIndex;

            MapImage = $"pack://application:,,,/Images/locations/{locationLists[mapListIndex].Description.ToLower()}.png";
            MapTitle = locationLists[mapListIndex].Title;
            MapEvent = locationLists[mapListIndex].LocationEvent;
            MapDescription = locationLists[mapListIndex].Description;
        }

        public void UpdateBlockedMapLocations()
        {
            mapListIndex = locationLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth && i.CoordinateX == CurrentEastWest);

            int NorthIndex = locationLists.FindIndex(i => i.CoordinateY == locationLists[mapListIndex].CoordinateY + 1 && i.CoordinateX == locationLists[mapListIndex].CoordinateX);

            if (NorthIndex != -1 && locationLists[NorthIndex].Description == "Hidden")
            {
                locationLists[NorthIndex].Description = "blockedtop";
            }
            else if (NorthIndex != -1 && locationLists[NorthIndex].Description.Contains("block"))
            {
                int NorthEast = locationLists.FindIndex(i => i.CoordinateY == locationLists[NorthIndex].CoordinateY && i.CoordinateX == locationLists[NorthIndex].CoordinateX + 1);
                int NorthWest = locationLists.FindIndex(i => i.CoordinateY == locationLists[NorthIndex].CoordinateY && i.CoordinateX == locationLists[NorthIndex].CoordinateX - 1);

                if (locationLists[NorthIndex].Description == "blockedtop")
                {
                    locationLists[NorthIndex].Description = "blockedtop";
                }
                else if (locationLists[NorthIndex].Description == "blockedright")
                {
                    locationLists[NorthIndex].Description = "blockedtopright";
                }
                else if (locationLists[NorthIndex].Description == "blockedleft")
                {
                    locationLists[NorthIndex].Description = "blockedtopleft";
                }

                // working I think, just need to test it properly
                //int NorthNorthIndex = locationLists.FindIndex(i => i.CoordinateY == locationLists[mapListIndex].CoordinateY + 2 && i.CoordinateX == locationLists[mapListIndex].CoordinateX);

                //if (NorthIndex == -1 || (locationLists[NorthIndex].Description == "blockedlefttop"))
                //{
                //    if (NorthNorthIndex == -1 || !locationLists[NorthNorthIndex].Description.Contains("blocked") && !locationLists[NorthNorthIndex].Description.Contains("Hidden"))
                //    {
                //        locationLists[NorthIndex].Description = "blockedtopleftright";
                //    }
                //}
                //else if (NorthIndex == -1 || (locationLists[NorthIndex].Description == "blockedtopright"))
                //{
                //    if (NorthNorthIndex == -1 || !locationLists[NorthNorthIndex].Description.Contains("blocked") && !locationLists[NorthNorthIndex].Description.Contains("Hidden"))
                //    {
                //        locationLists[NorthIndex].Description = "blockedtopleftright";
                //    }
                //}
            }

            int EastIndex = locationLists.FindIndex(i => i.CoordinateY == locationLists[mapListIndex].CoordinateY && i.CoordinateX == locationLists[mapListIndex].CoordinateX + 1);

            if (EastIndex != -1 && locationLists[EastIndex].Description == "Hidden")
            {
                locationLists[EastIndex].Description = "blockedright";
            }
            else if (EastIndex != -1 && locationLists[EastIndex].Description.Contains("block"))
            {
                int EastNorth = locationLists.FindIndex(i => i.CoordinateY == locationLists[EastIndex].CoordinateY + 1 && i.CoordinateX == locationLists[EastIndex].CoordinateX);
                int EastSouth = locationLists.FindIndex(i => i.CoordinateY == locationLists[EastIndex].CoordinateY - 1 && i.CoordinateX == locationLists[EastIndex].CoordinateX);

                if(locationLists[EastIndex].Description == "blockedright")
                {
                    locationLists[EastIndex].Description = "blockedright";
                }
                else if (locationLists[EastIndex].Description == "blockedtop")
                {
                    locationLists[EastIndex].Description = "blockedtopright";
                }
                else if (locationLists[EastIndex].Description == "blockedbottom")
                {
                    locationLists[EastIndex].Description = "blockedbottomright";
                }

                //int EastEastIndex = locationLists.FindIndex(i => i.CoordinateY == locationLists[mapListIndex].CoordinateY && i.CoordinateX == locationLists[mapListIndex].CoordinateX + 2);

                //if (EastIndex == -1 || (locationLists[EastIndex].Description == "blockedtopright"))
                //{
                //    if (EastEastIndex == -1 || !locationLists[EastEastIndex].Description.Contains("blocked") && !locationLists[EastEastIndex].Description.Contains("Hidden"))
                //    {
                //        locationLists[EastIndex].Description = "blockedlefttopbottom";
                //    }
                //}
                //else if (EastIndex == -1 || (locationLists[EastIndex].Description == "blockedbottomright"))
                //{
                //    if (EastEastIndex == -1 || !locationLists[EastEastIndex].Description.Contains("blocked") && !locationLists[EastEastIndex].Description.Contains("Hidden"))
                //    {
                //        locationLists[EastIndex].Description = "blockedlefttopbottom";
                //    }
                //}
            }

            int SouthIndex = locationLists.FindIndex(i => i.CoordinateY == locationLists[mapListIndex].CoordinateY - 1 && i.CoordinateX == locationLists[mapListIndex].CoordinateX);

            if (SouthIndex != -1 && locationLists[SouthIndex].Description == "Hidden")
            {
                locationLists[SouthIndex].Description = "blockedbottom";
            }
            else if (SouthIndex != -1 && locationLists[SouthIndex].Description.Contains("block"))
            {
                //int SouthEast = locationLists.FindIndex(i => i.CoordinateY == locationLists[SouthIndex].CoordinateY && i.CoordinateX == locationLists[SouthIndex].CoordinateX + 1);
                //int SouthWest = locationLists.FindIndex(i => i.CoordinateY == locationLists[SouthIndex].CoordinateY && i.CoordinateX == locationLists[SouthIndex].CoordinateX - 1);

                switch (locationLists[SouthIndex].Description)
                {
                    case "blockedbottom":
                        locationLists[SouthIndex].Description = "blockedbottom";
                        break;
                    case "blockedright":
                        locationLists[SouthIndex].Description = "blockedbottomright";
                        break;
                    case "blockedleft":
                        locationLists[SouthIndex].Description = "blockedbottomleft";
                        break;

                    default:
                        break;
                }

                //int SouthSouthIndex = locationLists.FindIndex(i => i.CoordinateY == locationLists[mapListIndex].CoordinateY - 2 && i.CoordinateX == locationLists[mapListIndex].CoordinateX);

                //if (SouthIndex == -1 || (locationLists[SouthIndex].Description == "blockedbottomleft"))
                //{
                //    if (SouthSouthIndex == -1 || !locationLists[SouthSouthIndex].Description.Contains("blocked") && !locationLists[SouthSouthIndex].Description.Contains("Hidden"))
                //    {
                //        locationLists[SouthIndex].Description = "blockedbottomleftright";
                //    }
                //}
                //else if (SouthIndex == -1 || (locationLists[SouthIndex].Description == "blockedbottomright"))
                //{
                //    if (SouthSouthIndex == -1 || !locationLists[SouthSouthIndex].Description.Contains("blocked") && !locationLists[SouthSouthIndex].Description.Contains("Hidden"))
                //    {
                //        locationLists[SouthIndex].Description = "blockedbottomleftright";
                //    }
                //}
            }

            int WestIndex = locationLists.FindIndex(i => i.CoordinateY == locationLists[mapListIndex].CoordinateY && i.CoordinateX == locationLists[mapListIndex].CoordinateX - 1);

            if (WestIndex != -1 && locationLists[WestIndex].Description == "Hidden")
            {
                locationLists[WestIndex].Description = "blockedleft";
            }
            else if (WestIndex != -1 && locationLists[WestIndex].Description.Contains("block"))
            {
                int WestNorth = locationLists.FindIndex(i => i.CoordinateY == locationLists[WestIndex].CoordinateY + 1 && i.CoordinateX == locationLists[WestIndex].CoordinateX);
                int WestSouth = locationLists.FindIndex(i => i.CoordinateY == locationLists[WestIndex].CoordinateY - 1 && i.CoordinateX == locationLists[WestIndex].CoordinateX);

                if (locationLists[WestIndex].Description == "blockedleft")
                {
                    locationLists[WestIndex].Description = "blockedleft";
                }
                else if (locationLists[WestIndex].Description == "blockedtop")
                {
                    locationLists[WestIndex].Description = "blockedtopleft";
                }
                else if (locationLists[WestIndex].Description == "blockedbottom")
                {
                    locationLists[WestIndex].Description = "blockedbottomleft";
                }

                //int WestWestIndex = locationLists.FindIndex(i => i.CoordinateY == locationLists[mapListIndex].CoordinateY && i.CoordinateX == locationLists[mapListIndex].CoordinateX - 1);

                //if (WestIndex == -1 || (locationLists[WestIndex].Description == "blockedtopleft"))
                //{
                //    if (WestWestIndex == -1 || !locationLists[WestWestIndex].Description.Contains("blocked") && !locationLists[WestWestIndex].Description.Contains("Hidden"))
                //    {
                //        locationLists[WestIndex].Description = "blockedrighttopbottom";
                //    }
                //}
                //else if (WestIndex == -1 || (locationLists[WestIndex].Description == "blockedbottomleft"))
                //{
                //    if (WestWestIndex == -1 || !locationLists[WestWestIndex].Description.Contains("blocked") && !locationLists[WestWestIndex].Description.Contains("Hidden"))
                //    {
                //        locationLists[WestIndex].Description = "blockedrighttopbottom";
                //    }
                //}
            }
        }

        private void NorthButton()
        {
            CurrentNorthSouth++;

            UpdateMapLocation();
            UpdateDirectionalButtons();
            TestingFight();
            TestingActivatingSecondQuest();
            UpdateBlockedMapLocations();
            UpdateMap();
        }

        private void EastButton()
        {
            CurrentEastWest++;

            UpdateMapLocation();
            UpdateDirectionalButtons();
            TestingFight();
            TestingActivatingSecondQuest();
            UpdateBlockedMapLocations();
            UpdateMap();
        }

        private void SouthButton()
        {
            CurrentNorthSouth--;

            UpdateMapLocation();
            UpdateDirectionalButtons();
            TestingFight();
            TestingActivatingSecondQuest();
            UpdateBlockedMapLocations();
            UpdateMap();
        }

        private void WestButton()
        {
            CurrentEastWest--;

            UpdateMapLocation();
            UpdateDirectionalButtons();
            TestingFight();
            TestingActivatingSecondQuest();
            UpdateBlockedMapLocations();
            UpdateMap();
        }

        public void UpdateDirectionalButtons()
        {
            int mapListIndexNorthMax = locationLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth + 1 && i.CoordinateX == CurrentEastWest);
            int mapListIndexEastMax = locationLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth && i.CoordinateX == CurrentEastWest + 1);
            int mapListIndexSouthMax = locationLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth - 1 && i.CoordinateX == CurrentEastWest);
            int mapListIndexWestMax = locationLists.FindIndex(i => i.CoordinateY == CurrentNorthSouth && i.CoordinateX == CurrentEastWest - 1);

            if (mapListIndexNorthMax == -1 || locationLists[mapListIndexNorthMax].Description == "Hidden" || locationLists[mapListIndexNorthMax].Description.Contains("block"))
            {
                NorthButtonEnabled = false;
            }
            else
            {
                NorthButtonEnabled = true;
            }

            if (mapListIndexEastMax == -1 || locationLists[mapListIndexEastMax].Description == "Hidden" || locationLists[mapListIndexEastMax].Description.Contains("block"))
            {
                EastButtonEnabled = false;
            }
            else
            {
                EastButtonEnabled = true;
            }

            if (mapListIndexSouthMax == -1 || locationLists[mapListIndexSouthMax].Description == "Hidden" || locationLists[mapListIndexSouthMax].Description.Contains("block"))
            {
                SouthButtonEnabled = false;
            }
            else
            {
                SouthButtonEnabled = true;
            }

            if (mapListIndexWestMax == -1 || locationLists[mapListIndexWestMax].Description == "Hidden" || locationLists[mapListIndexWestMax].Description.Contains("block"))
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

        // Redo FakePerson, TestingFight and TestingActivatingSecondQuest, Enemies, Random locations

        public void FakePerson()
        {
            Name = characterInformation[selectedCharacterIndex].Name;
            Type = characterInformation[selectedCharacterIndex].Type;
            Level = characterInformation[selectedCharacterIndex].Level;
            XP = 0;
            HP = characterInformation[selectedCharacterIndex].Hp;
            Strength = 3;
            Intelligence = 4;
            Agility = 5;
        }

        public void TestingFight()
        {
            if (locationLists[mapListIndex].LocationEvent.Contains("Fight"))
            {
                HP--;
                XP += 5;
            }
        }

        public void TestingActivatingSecondQuest()
        {
            if (mapListIndex == locationLists.FindIndex(ml => ml.LocationEvent.Contains("Quest #1")))
            {
               // Quest[Where(x => x.Title == "1. Search the map")].
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

        List<string> locationImages = new List<string> { "Grass", "Ocean", "Sand" };

        List<string> locationEvents = new List<string> { "Fight #1", "Fight #2", "Fight #3", "Shop", "Quest #1", "Quest #2", string.Empty, string.Empty, string.Empty };

        public void SetupRandomLocations()
        {
          
            locationLists.Add(new Location(0, 0, "Starting location - Grassland #1", "Grass", "Home", false));

            //All locations
            //locationLists.Add(new Location(2, -2, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(2, -1, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(2, 0, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(2, 1, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(2, 2, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(1, -2, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(1, -1, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(1, 0, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(1, 1, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(1, 2, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(0, -2, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(0, -1, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(0, 1, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(0, 2, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(-1, -2, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(-1, -1, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(-1, 0, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(-1, 1, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(-1, 2, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(-2, -2, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(-2, -1, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(-2, 0, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(-2, 1, "Starting location - Grassland #1", "Grass", "Home", false));
            //locationLists.Add(new Location(-2, 2, "Starting location - Grassland #1", "Grass", "Home", false));

            Random random = new Random();

            List<int> coordYList = new List<int>();
            List<int> coordXList = new List<int>();

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

                int randomCoordinateY = random.Next(coordYList.Min() - 1, coordYList.Max() + 1);
                int randomCoordinateX = random.Next(coordXList.Min() - 1, coordXList.Max() + 1);

                int randomImage = random.Next(0, locationImages.Count());
                int randomQuest = random.Next(0, locationEvents.Count());

                List<string> directions = new List<string> { "North", "East", "South", "West" };

                string pickTempCoordinate = directions[random.Next(0, 4)];

                int tempY = 0;
                int tempX = 0;

                switch (pickTempCoordinate)
                {
                    case "North":
                        tempY = randomCoordinateY + 1;
                        tempX = randomCoordinateX;
                        break;
                    case "East":
                        tempY = randomCoordinateY;
                        tempX = randomCoordinateX + 1;
                        break;
                    case "South":
                        tempY = randomCoordinateY - 1;
                        tempX = randomCoordinateX;
                        break;
                    case "West":
                        tempY = randomCoordinateY;
                        tempX = randomCoordinateX - 1;
                        break;
                    default:
                        break;
                }

                AddRandomLocation(randomCoordinateY, randomCoordinateX, tempY, tempX, randomImage, randomQuest);
            }
            while (locationLists.Count < 10);

            List<int> totalColumns = new List<int> { 2, 1, 0, -1, -2 };
            List<int> totalRows = new List<int> { 2, 1, 0, -1, -2 };
            
            foreach (var item in locationLists.ToList())
            {
                foreach (var column in totalColumns)
                {
                    foreach (var row in totalRows)
                    {
                        if (locationLists.FindIndex(i => i.CoordinateY == column && i.CoordinateX == row) == -1)
                        {
                            locationLists.Add(new Location(column, row, "", "Hidden", "", false));
                        }
                    }
                        
                }
            }
        }
        public void AddRandomLocation(int randomY, int randomX, int tempY, int tempX, int randomImage, int randomQuest)
        {
            if (locationLists.FindIndex(i => i.CoordinateY == randomY && i.CoordinateX == randomX) != -1)
            {
                if (locationLists.FindIndex(i => i.CoordinateY == tempY && i.CoordinateX == tempX) == -1 && (tempY <= 2 && tempY >= -2) && (tempX <= 2 && tempX >= -2))
                {
                    locationLists.Add(new Location(tempY, tempX, $"Random { locationImages[randomImage] } location #{locationLists.Count + 1}", $"{locationImages[randomImage]}", $"{locationEvents[randomQuest]}", true));
                    locationEvents.RemoveAt(randomQuest);
                }
            }
        }

        //Testo

        // hidden by default (black image)
        static string hiddenImage = $"pack://application:,,,/Images/locations/hidden.png";

        private string fullMapImage1 = hiddenImage;
        private string fullMapImage2 = hiddenImage;
        private string fullMapImage3 = hiddenImage;
        private string fullMapImage4 = hiddenImage;
        private string fullMapImage5 = hiddenImage;
        private string fullMapImage6 = hiddenImage;
        private string fullMapImage7 = hiddenImage;
        private string fullMapImage8 = hiddenImage;
        private string fullMapImage9 = hiddenImage;
        private string fullMapImage10 = hiddenImage;
        private string fullMapImage11 = hiddenImage;
        private string fullMapImage12 = hiddenImage;
        private string fullMapImage13 = hiddenImage;
        private string fullMapImage14 = hiddenImage;
        private string fullMapImage15 = hiddenImage;
        private string fullMapImage16 = hiddenImage;
        private string fullMapImage17 = hiddenImage;
        private string fullMapImage18 = hiddenImage;
        private string fullMapImage19 = hiddenImage;
        private string fullMapImage20 = hiddenImage;
        private string fullMapImage21 = hiddenImage;
        private string fullMapImage22 = hiddenImage;
        private string fullMapImage23 = hiddenImage;
        private string fullMapImage24 = hiddenImage;
        private string fullMapImage25 = hiddenImage;

        public string FullMapImage1
        {
            get
            {
                return fullMapImage1;
            }
            set
            {
                fullMapImage1 = value;
                RaisePropertyChanged(nameof(this.FullMapImage1));
            }
        }
        public string FullMapImage2
        {
            get
            {
                return fullMapImage2;
            }
            set
            {
                fullMapImage2 = value;
                RaisePropertyChanged(nameof(this.FullMapImage2));
            }
        }
        public string FullMapImage3
        {
            get
            {
                return fullMapImage3;
            }
            set
            {
                fullMapImage3 = value;
                RaisePropertyChanged(nameof(this.FullMapImage3));
            }
        }
        public string FullMapImage4
        {
            get
            {
                return fullMapImage4;
            }
            set
            {
                fullMapImage4 = value;
                RaisePropertyChanged(nameof(this.FullMapImage4));
            }
        }
        public string FullMapImage5
        {
            get
            {
                return fullMapImage5;
            }
            set
            {
                fullMapImage5 = value;
                RaisePropertyChanged(nameof(this.FullMapImage5));
            }
        }
        public string FullMapImage6
        {
            get
            {
                return fullMapImage6;
            }
            set
            {
                fullMapImage6 = value;
                RaisePropertyChanged(nameof(this.FullMapImage6));
            }
        }
        public string FullMapImage7
        {
            get
            {
                return fullMapImage7;
            }
            set
            {
                fullMapImage7 = value;
                RaisePropertyChanged(nameof(this.FullMapImage7));
            }
        }
        public string FullMapImage8
        {
            get
            {
                return fullMapImage8;
            }
            set
            {
                fullMapImage8 = value;
                RaisePropertyChanged(nameof(this.FullMapImage8));
            }
        }
        public string FullMapImage9
        {
            get
            {
                return fullMapImage9;
            }
            set
            {
                fullMapImage9 = value;
                RaisePropertyChanged(nameof(this.FullMapImage9));
            }
        }
        public string FullMapImage10
        {
            get
            {
                return fullMapImage10;
            }
            set
            {
                fullMapImage10 = value;
                RaisePropertyChanged(nameof(this.FullMapImage10));
            }
        }
        public string FullMapImage11
        {
            get
            {
                return fullMapImage11;
            }
            set
            {
                fullMapImage11 = value;
                RaisePropertyChanged(nameof(this.FullMapImage11));
            }
        }
        public string FullMapImage12
        {
            get
            {
                return fullMapImage12;
            }
            set
            {
                fullMapImage12 = value;
                RaisePropertyChanged(nameof(this.FullMapImage12));            }
        }
        public string FullMapImage13
        {
            get
            {
                return fullMapImage13;
            }
            set
            {
                fullMapImage13 = value;
                RaisePropertyChanged(nameof(this.FullMapImage13));
            }
        }
        public string FullMapImage14
        {
            get
            {
                return fullMapImage14;
            }
            set
            {
                fullMapImage14 = value;
                RaisePropertyChanged(nameof(this.FullMapImage14));
            }
        }
        public string FullMapImage15
        {
            get
            {
                return fullMapImage15;
            }
            set
            {
                fullMapImage15 = value;
                RaisePropertyChanged(nameof(this.FullMapImage15));
            }
        }
        public string FullMapImage16
        {
            get
            {
                return fullMapImage16;
            }
            set
            {
                fullMapImage16 = value;
                RaisePropertyChanged(nameof(this.FullMapImage16));
            }
        }
        public string FullMapImage17
        {
            get
            {
                return fullMapImage17;
            }
            set
            {
                fullMapImage17 = value;
                RaisePropertyChanged(nameof(this.FullMapImage17));
            }
        }
        public string FullMapImage18
        {
            get
            {
                return fullMapImage18;
            }
            set
            {
                fullMapImage18 = value;
                RaisePropertyChanged(nameof(this.FullMapImage18));
            }
        }
        public string FullMapImage19
        {
            get
            {
                return fullMapImage19;
            }
            set
            {
                fullMapImage19 = value;
                RaisePropertyChanged(nameof(this.FullMapImage19));
            }
        }
        public string FullMapImage20
        {
            get
            {
                return fullMapImage20;
            }
            set
            {
                fullMapImage20 = value;
                RaisePropertyChanged(nameof(this.FullMapImage20));
            }
        }
        public string FullMapImage21
        {
            get
            {
                return fullMapImage21;
            }
            set
            {
                fullMapImage21 = value;
                RaisePropertyChanged(nameof(this.FullMapImage21));
            }
        }
        public string FullMapImage22
        {
            get
            {
                return fullMapImage22;
            }
            set
            {
                fullMapImage22 = value;
                RaisePropertyChanged(nameof(this.FullMapImage22));
            }
        }
        public string FullMapImage23
        {
            get
            {
                return fullMapImage23;
            }
            set
            {
                fullMapImage23 = value;
                RaisePropertyChanged(nameof(this.FullMapImage23));
            }
        }
        public string FullMapImage24
        {
            get
            {
                return fullMapImage24;
            }
            set
            {
                fullMapImage24 = value;
                RaisePropertyChanged(nameof(this.FullMapImage24));
            }
        }
        public string FullMapImage25
        {
            get
            {
                return fullMapImage25;
            }
            set
            {
                fullMapImage25 = value;
                RaisePropertyChanged(nameof(this.FullMapImage25));
            }
        }

        public void UpdateMap()
        {
            if (locationLists[mapListIndex].Hidden == true)
            {
                locationLists[mapListIndex].Hidden = false;
            }

            string currentLocationImages = $"pack://application:,,,/Images/locations/{locationLists[mapListIndex].Description}.png";
           
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == -2)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == -2)] == locationLists[mapListIndex])
                {
                    FullMapImage1 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == -2)].Description}.png";
                }
                else
                {
                    FullMapImage1 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == -2)].Description}.png";
                }

                //FullMapImage1 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == -2)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == -1)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == -1)] == locationLists[mapListIndex])
                {
                    FullMapImage2 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == -1)].Description}.png";
                }
                else
                {
                    FullMapImage2 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == -1)].Description}.png";
                }
                
                //FullMapImage2 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == -1)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 0)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 0)] == locationLists[mapListIndex])
                {
                    FullMapImage3 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 0)].Description}.png";
                }
                else
                {
                    FullMapImage3 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 0)].Description}.png";
                }

                //FullMapImage3 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 0)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 1)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 1)] == locationLists[mapListIndex])
                {
                    FullMapImage4 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 1)].Description}.png";
                }
                else
                {
                    FullMapImage4 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 1)].Description}.png";
                }

                //FullMapImage4 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 1)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 2)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 2)] == locationLists[mapListIndex])
                {
                    FullMapImage5 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 2)].Description}.png";
                }
                else
                {
                    FullMapImage5 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 2)].Description}.png";
                }
                
                //FullMapImage5 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 2 && i.CoordinateX == 2)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == -2)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == -2)] == locationLists[mapListIndex])
                {
                    FullMapImage6 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == -2)].Description}.png";
                }
                else
                {
                    FullMapImage6 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == -2)].Description}.png";
                }

                //FullMapImage6 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == -2)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == -1)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == -1)] == locationLists[mapListIndex])
                {
                    FullMapImage7 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == -1)].Description}.png";
                }
                else
                {
                    FullMapImage7 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == -1)].Description}.png";
                }

                //FullMapImage7 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == -1)].Description}.png";
            }

            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 0)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 0)] == locationLists[mapListIndex])
                {
                    FullMapImage8 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 0)].Description}.png";
                }
                else
                {
                    FullMapImage8 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 0)].Description}.png";
                }

                //FullMapImage8 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 0)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 1)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 1)] == locationLists[mapListIndex])
                {
                    FullMapImage9 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 1)].Description}.png";
                }
                else
                {
                    FullMapImage9 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 1)].Description}.png";
                }

                //FullMapImage9 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 1)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 2)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 2)] == locationLists[mapListIndex])
                {
                    FullMapImage10 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 2)].Description}.png";
                }
                else
                {
                    FullMapImage10 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 2)].Description}.png";
                }

                //FullMapImage10 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 1 && i.CoordinateX == 2)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == -2)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == -2)] == locationLists[mapListIndex])
                {
                    FullMapImage11 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == -2)].Description}.png";
                }
                else
                {
                    FullMapImage11 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == -2)].Description}.png";
                }

                //FullMapImage11 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == -2)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == -1)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == -1)] == locationLists[mapListIndex])
                {
                    FullMapImage12 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == -1)].Description}.png";
                }
                else
                {
                    FullMapImage12 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == -1)].Description}.png";
                }

                //FullMapImage12 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == -1)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 0)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 0)] == locationLists[mapListIndex])
                {
                    FullMapImage13 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 0)].Description}.png";
                }
                else
                {
                    FullMapImage13 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 0)].Description}.png";
                }
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 1)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 1)] == locationLists[mapListIndex])
                {
                    FullMapImage14 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 1)].Description}.png";
                }
                else
                {
                    FullMapImage14 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 1)].Description}.png";
                }

                //FullMapImage14 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 1)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 2)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 2)] == locationLists[mapListIndex])
                {
                    FullMapImage15 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 2)].Description}.png";
                }
                else
                {
                    FullMapImage15 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 2)].Description}.png";
                }

                //FullMapImage15 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == 0 && i.CoordinateX == 2)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == -2)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == -2)] == locationLists[mapListIndex])
                {
                    FullMapImage16 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == -2)].Description}.png";
                }
                else
                {
                    FullMapImage16 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == -2)].Description}.png";
                }

                //FullMapImage16 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == -2)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == -1)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == -1)] == locationLists[mapListIndex])
                {
                    FullMapImage17 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == -1)].Description}.png";
                }
                else
                {
                    FullMapImage17 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == -1)].Description}.png";
                }

                //FullMapImage17 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == -1)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 0)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 0)] == locationLists[mapListIndex])
                {
                    FullMapImage18 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 0)].Description}.png";
                }
                else
                {
                    FullMapImage18 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 0)].Description}.png";
                }

                //FullMapImage18 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 0)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 1)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 1)] == locationLists[mapListIndex])
                {
                    FullMapImage19 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 1)].Description}.png";
                }
                else
                {
                    FullMapImage19 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 1)].Description}.png";
                }

                //FullMapImage19 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 1)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 2)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 2)] == locationLists[mapListIndex])
                {
                    FullMapImage20 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 2)].Description}.png";
                }
                else
                {
                    FullMapImage20 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 2)].Description}.png";
                }

                //FullMapImage20 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -1 && i.CoordinateX == 2)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == -2)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == -2)] == locationLists[mapListIndex])
                {
                    FullMapImage21 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == -2)].Description}.png";
                }
                else
                {
                    FullMapImage21 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == -2)].Description}.png";
                }

                //FullMapImage21 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == -2)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == -1)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == -1)] == locationLists[mapListIndex])
                {
                    FullMapImage22 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == -1)].Description}.png";
                }
                else
                {
                    FullMapImage22 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == -1)].Description}.png";
                }

                //FullMapImage22 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == -1)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 0)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 0)] == locationLists[mapListIndex])
                {
                    FullMapImage23 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 0)].Description}.png";
                }
                else
                {
                    FullMapImage23 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 0)].Description}.png";
                }

                //FullMapImage23 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 0)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 1)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 1)] == locationLists[mapListIndex])
                {
                    FullMapImage24 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 1)].Description}.png";
                }
                else
                {
                    FullMapImage24 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 1)].Description}.png";
                }

                //FullMapImage24 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 1)].Description}.png";
            }
            
            if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 2)].Hidden == false)
            {
                if (locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 2)] == locationLists[mapListIndex])
                {
                    FullMapImage25 = $"pack://application:,,,/Images/locations/current{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 2)].Description}.png";
                }
                else
                {
                    FullMapImage25 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 2)].Description}.png";
                }

                //FullMapImage25 = $"pack://application:,,,/Images/locations/{locationLists[locationLists.FindIndex(i => i.CoordinateY == -2 && i.CoordinateX == 2)].Description}.png";
            }
        }
    }
}
