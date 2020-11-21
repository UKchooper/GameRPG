using GameRPG.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GameRPG.View
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        int CurrentNorthSouth = 0;
        int CurrentEastWest = 0;
        int mapListIndex;

        int maxX;
        int maxY;
        int minX;
        int minY;

        List<Map> mapLists = new List<Map>();
        List<string> questList = new List<string>();
        public GameView()
        {
            InitializeComponent();

            //DataContext = new GameViewModel();

            DisableSouthWest();

            WhoCares();

            WhoCaresMarkII();

            AddToComboBox();

            AddToQuests();

            //testingViewModel tvM = new testingViewModel();

            //tvM.Update();
        }

        public void WhoCares()
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

            questList.Add("Search the map");
            questList.Add("Defeat Bob");
            questList.Add("Eat food");
            questList.Add("Speak to Malcolm");
            questList.Add("Die");

            MaxCoord();
        }

        private void ButtonNorth_Click(object sender, RoutedEventArgs e)
        {
            CurrentNorthSouth++;

            WhoCaresMarkII();

            if (btnSouth.IsEnabled == false)
            {
                btnSouth.IsEnabled = true;
            }

            if (CurrentNorthSouth == maxX)
            {
                btnNorth.IsEnabled = false;
            }
        }

        private void btnSouth_Click(object sender, RoutedEventArgs e)
        {
            CurrentNorthSouth--;

            WhoCaresMarkII();

            if (btnNorth.IsEnabled == false)
            {
                btnNorth.IsEnabled = true;
            }

            if (CurrentNorthSouth == minX)
            {
                btnSouth.IsEnabled = false;
            }
        }

        private void ButtonEast_Click(object sender, RoutedEventArgs e)
        {
            CurrentEastWest++;

            WhoCaresMarkII();

            if (btnWest.IsEnabled == false)
            {
                btnWest.IsEnabled = true;
            }

            if (CurrentEastWest == maxY)
            {
                btnEast.IsEnabled = false;
            }
        }

        private void btnWest_Click(object sender, RoutedEventArgs e)
        {
            CurrentEastWest--;

            WhoCaresMarkII();

            if (btnEast.IsEnabled == false)
            {
                btnEast.IsEnabled = true;
            }

            if (CurrentEastWest == minY)
            {
                btnWest.IsEnabled = false;
            }
        }

        public void WhoCaresMarkII()
        {
            mapListIndex = mapLists.FindIndex(i => i.CoorY == CurrentNorthSouth && i.CoorX == CurrentEastWest);

            imageDisplay.Source = new BitmapImage(new Uri($@"/Images/locations/{mapLists[mapListIndex].Description.ToLower()}.png", UriKind.Relative));

            ok.Text = mapLists[mapListIndex].Title;

            tbkDescription.Text = mapLists[mapListIndex].Description;

            if (!string.IsNullOrWhiteSpace(mapLists[mapListIndex].DoSomething))
            {
                tbkEvent.Text = mapLists[mapListIndex].DoSomething;
            }
            else
            {
                tbkEvent.Text = "None";
            }

            DoingSomethingEvent();
        }

        public void MaxCoord()
        {
            maxX = mapLists.Max(map => map.CoorX);
            maxY = mapLists.Max(map => map.CoorY);
            minX = mapLists.Min(map => map.CoorX);
            minY = mapLists.Min(map => map.CoorY);
        }

        public void DoingSomethingEvent()
        {
            switch (mapLists[mapListIndex].DoSomething)
            {
                case "Fight":
                    //MessageBox.Show("Fight begins!");
                    break;
                case "Shop":
                    //MessageBox.Show("No shop for you buddy!");
                    break;
                default:
                    break;
            }
        }

        public void DisableSouthWest()
        {
            btnSouth.IsEnabled = false;
            btnWest.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FindHome();
        }

        public void FindHome()
        {
            string homeLocation = "";

            int currentMapListIndex = mapLists.FindIndex(i => i.CoorY == CurrentNorthSouth && i.CoorX == CurrentEastWest);

            int itemIndex = mapLists.FindIndex(i => i.DoSomething.Contains(cbxHello.Text));

            if (currentMapListIndex == itemIndex)
            {
                MessageBox.Show("You're already at this location");
            }
            else
            {
                for (int i = CurrentNorthSouth; i > mapLists[itemIndex].CoorY; i--)
                {
                    homeLocation += "South\n";
                }

                for (int i = CurrentEastWest; i > mapLists[itemIndex].CoorX; i--)
                {
                    homeLocation += "West\n";
                }

                for (int i = CurrentNorthSouth; i < mapLists[itemIndex].CoorY; i++)
                {
                    homeLocation += "North\n";
                }

                for (int i = CurrentEastWest; i < mapLists[itemIndex].CoorX; i++)
                {
                    homeLocation += "East\n";
                }

                MessageBox.Show($"Navigate:\n{homeLocation}");
            }
        }

        public void AddToComboBox()
        {
            foreach (var eventos in mapLists)
            {
                if (!string.IsNullOrEmpty(eventos.DoSomething))
                {
                    cbxHello.Items.Add(eventos.DoSomething);
                }
            }
        }

        public void AddToQuests()
        {
            int number = 1;

            foreach (var quests in questList)
            {

                lbxQuest.Items.Add($"Quest {number}: {quests}");
                number++;
            }
        }
    }
}
