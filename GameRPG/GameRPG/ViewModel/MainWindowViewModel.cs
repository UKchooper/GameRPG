using Prism.Mvvm;

namespace GameRPG.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public int Index;

        private readonly StartScreenViewModel startScreenViewModel;
        private readonly CharacterSelectionViewModel characterSelectionViewModel;
        private readonly CharacterCreationViewModel characterCreationViewModel;
        private readonly FightViewModel fightViewModel;
        private readonly GameViewModel gameViewModel;

        private BindableBase currentViewModel;

        public MainWindowViewModel()
        {
            this.startScreenViewModel = new StartScreenViewModel(this);
            this.characterSelectionViewModel = new CharacterSelectionViewModel(this);
            this.gameViewModel = new GameViewModel(this);
            this.characterCreationViewModel = new CharacterCreationViewModel();
            this.fightViewModel = new FightViewModel();

            this.CurrentViewModel = this.startScreenViewModel;
        }
        public BindableBase CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }
            set
            {
                this.SetProperty(ref this.currentViewModel, value);
            }
        }

        public void SwitchToCharacterSelection()
        {
            this.CurrentViewModel = this.characterSelectionViewModel;
        }

        public void SwitchToGameView()
        {
            this.CurrentViewModel = this.gameViewModel;
        }

        public void LoadCharacterFromFile()
        {
            this.gameViewModel.AddCharacterFromFile();
        }
    }
}
