using Prism.Mvvm;

namespace GameRPG.ViewModel
{
    public class CharacterCreationViewModel : BindableBase
    {
        private string name;
        private string type;
        private int level;
        private int hp;
        private int attack;
        private string description;
        private int defence;
        private string image;

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
                description = requiredInfo;
                
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

        public string requiredInfo;

        public void Random()
        {
            requiredInfo = $"{Name},{Type},{Level},{HP},{Attack},{Description},{Defence}";

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\carl.hooper\Desktop\GameoStuffo\TextFiles\SaveCharacter.txt", true))
            {
                file.WriteLine(requiredInfo);
            }
        }
    }
}
