using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GameRPG.ViewModel
{
    public class CharacterSelectionViewModel : BindableBase
    {
        const string characterFile = @"C:\Users\carl.hooper\Desktop\GameoStuffo\TextFiles\Characters.txt";

        private MainWindowViewModel main;

        private string name;
        private string type;
        private int level;
        private int hp;
        private int attack;
        private string description;
        private int defence;
        private string image;

        public static int selectedCharacterIndex;

        public static List<Character> characters;
        
        public CharacterSelectionViewModel(MainWindowViewModel main)
        {
            this.main = main;

            this.ConfirmCommand = new DelegateCommand(this.PressConfirm);

            TxtReader charReader = new TxtReader(characterFile);
            characters = charReader.ReadCharacters();

            UpdateCharacter();

            foreach (var character in characters)
            {
                CharacterNames.Add(character.Name);
            }
        }

        public ICommand ConfirmCommand { get; set; }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
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
                RaisePropertyChanged(nameof(Type));
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
                RaisePropertyChanged(nameof(Level));
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
                RaisePropertyChanged(nameof(HP));
            }
        }

        public int Attack
        {
            get
            {
                return attack;
            }
            set
            {
                attack = value;
                RaisePropertyChanged(nameof(Attack));
            }
        }

        public int Defence
        {
            get
            {
                return defence;
            }
            set
            {
                defence = value;
                RaisePropertyChanged(nameof(Defence));
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public string Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                RaisePropertyChanged(nameof(Image));
            }
        }

        public ObservableCollection<string> CharacterNames { get; } = new ObservableCollection<string>();

        public int SelectedCharacterIndex
        {
            get
            {
                return selectedCharacterIndex;
            }
            set
            {
                selectedCharacterIndex = value;
                RaisePropertyChanged(nameof(SelectedCharacterIndex));
                UpdateCharacter();
            }
        }

        public void UpdateCharacter()
        {
            Name = characters[SelectedCharacterIndex].Name;
            Type = characters[SelectedCharacterIndex].Type;
            Level = characters[SelectedCharacterIndex].Level;
            HP = characters[SelectedCharacterIndex].Hp;
            Attack = characters[SelectedCharacterIndex].Attack;
            Description = characters[SelectedCharacterIndex].Description;
            Defence = characters[SelectedCharacterIndex].Defence;
            Image = $"pack://application:,,,/{characters[selectedCharacterIndex].Image}";
         }

        private void PressConfirm()
        {
            this.main.SwitchToGameView();
        }
    }
}
