using Prism.Commands;
using Prism.Mvvm;
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
        }

        public ICommand NewGameCommand { get; }

        private void StartNewGame()
        {
            this.main.SwitchToCharacterSelection();
        }
    }
}
