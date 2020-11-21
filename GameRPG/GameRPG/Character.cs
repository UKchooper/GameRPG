using System;
using System.Collections.Generic;
using System.Text;

namespace GameRPG
{
    public class Character
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public string Image { get; set; }
        public Character(string name, string type, string description, int level, int hp, int attack, int defence, string image)
        {
            Name = name;
            Type = type;
            Description = description;
            Level = level;
            Hp = hp;
            Attack = attack;
            Defence = defence;
            Image = image;
        }
    }
}
