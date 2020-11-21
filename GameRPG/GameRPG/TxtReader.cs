using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameRPG
{
    public class TxtReader
    {
        private readonly string TxtFilePath;
        public TxtReader(string txtFilePath)
        {
            this.TxtFilePath = txtFilePath;
        }
        //public List<StatsClass> ReadClasses()
        //{
        //    List<StatsClass> stats = new List<StatsClass>();

        //    using StreamReader sr = new StreamReader(TxtFilePath);
        //    for (int i = 0; i < File.ReadLines(TxtFilePath).Count(); i++)
        //    {
        //        string txtLine = sr.ReadLine();
        //        stats.Add(ReadStatClassesFromTxtLine(txtLine));
        //    }

        //    return stats;
        //}
        //public StatsClass ReadStatClassesFromTxtLine(string txtLine)
        //{
        //    string[] parts = txtLine.Split(',');

        //    string statName = parts[0];
        //    int statNumber = int.Parse(parts[1]);
        //    string statDescription = parts[2];

        //    return new StatsClass(statName, statNumber, statDescription);
        //}

        public List<Character> ReadCharacters()
        {
            List<Character> stats = new List<Character>();

            using StreamReader sr = new StreamReader(TxtFilePath);
            for (int i = 0; i < File.ReadLines(TxtFilePath).Count(); i++)
            {
                string txtLine = sr.ReadLine();
                stats.Add(ReadStatCharactersFromTxtLine(txtLine));
            }

            return stats;
        }
        public Character ReadStatCharactersFromTxtLine(string txtLine)
        {
            string[] parts = txtLine.Split(',');

            string name = parts[0];
            string type = parts[1];
            string description = parts[2];
            int level = int.Parse(parts[3]);
            int hp = int.Parse(parts[4]);
            int attack = int.Parse(parts[5]);
            int defence = int.Parse(parts[6]);
            string image = parts[7];

             return new Character(name, type, description, level, hp, attack, defence, image);
        }

        public List<Enemy> ReadEnemies()
        {
            List<Enemy> stats = new List<Enemy>();

            using StreamReader sr = new StreamReader(TxtFilePath);
            for (int i = 0; i < File.ReadLines(TxtFilePath).Count(); i++)
            {
                string txtLine = sr.ReadLine();
                stats.Add(ReadEnemiesFromTxtLine(txtLine));
            }

            return stats;
        }

        public Enemy ReadEnemiesFromTxtLine(string txtLine)
        {
            string[] parts = txtLine.Split(',');

            string name = parts[0];
            string type = parts[1];
            string description = parts[2];
            int level = int.Parse(parts[3]);
            int hp = int.Parse(parts[4]);
            int attack = int.Parse(parts[5]);
            int defence = int.Parse(parts[6]);

            return new Enemy(name, type, description, level, hp, attack, defence);
        }
    }
}