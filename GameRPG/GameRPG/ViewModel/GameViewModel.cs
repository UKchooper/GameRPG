using Prism.Mvvm;
using System.Collections.Generic;

namespace GameRPG.ViewModel
{
    public class GameViewModel : BindableBase
    {
        List<Character> charList = new List<Character>();
        public GameViewModel()
        {
            //charList.Add(new Character("Bob", 95));
            //Update();
        }

        private string name;
        private int hp;

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

        public void LoseHP()
        {
            hp--;
            //UpdateHP();
        }

        public void Update()
        {
            
            //HP = charList[0].HP;
            //Name = charList[0].Name;
        }
    }
}
