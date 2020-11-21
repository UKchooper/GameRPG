using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace GameRPG.ViewModel
{
    class FightViewModel : BindableBase
    {
        const string enemyFile = @"C:\Users\carl.hooper\Desktop\GameoStuffo\TextFiles\Enemy.txt";

        private string name;
        private string type;
        private int level;
        private int hp;
        private int attack;
        private string description;
        private int defence;

        private int randomEnemy;

        private List<Enemy> enemies;
        public FightViewModel()
        {
            TxtReader enemyReader = new TxtReader(enemyFile);
            enemies = enemyReader.ReadEnemies();

            RandomEnemyGenerator();
        }

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

        private void RandomEnemyGenerator()
        {
            Random rn = new Random(); 

            randomEnemy = rn.Next(0, enemies.Count);

            Name = enemies[randomEnemy].Name;
            Type = enemies[randomEnemy].Type;
            Level = enemies[randomEnemy].Level;
            HP = enemies[randomEnemy].Hp;
            Attack = enemies[randomEnemy].Attack;
            Defence = enemies[randomEnemy].Defence;
        }
    }
}
