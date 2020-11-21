using System.Windows;
using GameRPG.ViewModel;

namespace GameRPG.View
{
    /// <summary>
    /// Interaction logic for StartScreenView.xaml
    /// </summary>
    /// 
    public partial class StartScreenView
    {
        public StartScreenView()
        {
            InitializeComponent();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"in construction, Sorry!");
        }

        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"in construction, Sorry!");
        }
    }
}
