using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private string description;
        private int strength;
        private int agility;
        private int intelligence;
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

        public int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
                RaisePropertyChanged(nameof(Strength));
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
                RaisePropertyChanged(nameof(Agility));
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
                RaisePropertyChanged(nameof(Intelligence));
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
            Strength = characters[SelectedCharacterIndex].Strength;
            Description = characters[SelectedCharacterIndex].Description;
            Agility = characters[SelectedCharacterIndex].Agility;
            Intelligence = characters[selectedCharacterIndex].Intelligence;
            Image = $"pack://application:,,,/{characters[selectedCharacterIndex].Image}";
         }

        private void PressConfirm()
        {
            this.main.SwitchToGameView();

            string combineCharacterStats = $"{Name},{Type},{Description},{Level},{HP},{Strength},{Agility},{Intelligence},{Image}";

            TxtWriter characterWriter = new TxtWriter(combineCharacterStats);

            characterWriter.WriteCharacters();
        }
    }
}
