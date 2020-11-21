using GameRPG.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameRPG.View
{
    /// <summary>
    /// Interaction logic for CharacterCreationView.xaml
    /// </summary>
    public partial class CharacterCreationView : UserControl
    {
        public CharacterCreationView()
        {
            InitializeComponent();
            DataContext = new CharacterCreationViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
