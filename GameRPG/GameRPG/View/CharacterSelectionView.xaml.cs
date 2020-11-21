using System.Windows;
using System.Windows.Controls;
using GameRPG.ViewModel;

namespace GameRPG.View
{
    /// <summary>
    /// Interaction logic for CharacterSelectionView.xaml
    /// </summary>
    public partial class CharacterSelectionView : UserControl
    {
        public CharacterSelectionView()
        {
            InitializeComponent();
            //DataContext = new CharacterSelectionViewModel();
        }
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            //Window.GetWindow(this).DataContext = new FightViewModel();
        }
    }
}
