using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace GameRPG.ViewModel
{
    public class StartScreenViewModel : BindableBase
    {
        private MainWindowViewModel main;
        public StartScreenViewModel(MainWindowViewModel main)
        {
            this.main = main;

            this.NewGameCommand = new DelegateCommand(this.StartNewGame);
            this.LoadGameCommand = new DelegateCommand(this.LoadGame);
            this.SettingsCommand = new DelegateCommand(this.Settings);
        }

        public ICommand NewGameCommand { get; }
        public ICommand LoadGameCommand { get; }
        public ICommand SettingsCommand { get; }

        private void StartNewGame()
        {
            this.main.SwitchToCharacterSelection();
        }

        private void LoadGame()
        {
            this.main.SwitchToGameView();
        }

        private void Settings()
        {
            MessageBox.Show($"Under construction, sorry!");
        }
    }
}
