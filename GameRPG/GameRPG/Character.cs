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
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public string Image { get; set; }
        public Character(string name, string type, string description, int level, int hp, int strength, int agility, int intelligence, string image)
        {
            Name = name;
            Type = type;
            Description = description;
            Level = level;
            Hp = hp;
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Image = image;
        }
    }
}
