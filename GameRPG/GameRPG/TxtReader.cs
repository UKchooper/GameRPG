using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        private Character ReadStatCharactersFromTxtLine(string txtLine)
        {
            string[] parts = txtLine.Split(',');

            string name = parts[0];
            string type = parts[1];
            string description = parts[2];
            int level = int.Parse(parts[3]);
            int hp = int.Parse(parts[4]);
            int xp = int.Parse(parts[5]);
            int strength = int.Parse(parts[6]);
            int agility = int.Parse(parts[7]);
            int intelligence = int.Parse(parts[8]);
            string image = parts[9];

             return new Character(name, type, description, level, hp, xp, strength, agility, intelligence, image);
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

        private Enemy ReadEnemiesFromTxtLine(string txtLine)
        {
            string[] parts = txtLine.Split(',');

            string name = parts[0];
            string type = parts[1];
            string description = parts[2];
            int level = int.Parse(parts[3]);
            int hp = int.Parse(parts[4]);
            int strength = int.Parse(parts[5]);
            int agility = int.Parse(parts[6]);
            int intelligence = int.Parse(parts[7]);

            return new Enemy(name, type, description, level, hp, strength, agility, intelligence);
        }

        public List<Location> ReadLocations()
        {
            List<Location> locations = new List<Location>();

            using StreamReader sr = new StreamReader(TxtFilePath);
            for (int i = 0; i < File.ReadLines(TxtFilePath).Count(); i++)
            {
                string txtLine = sr.ReadLine();
                locations.Add(ReadLocationsFromTxtLine(txtLine));
            }

            return locations;
        }

        private Location ReadLocationsFromTxtLine(string txtLine)
        {
            string[] parts = txtLine.Split(',');

            int coordinateX = int.Parse(parts[0]);
            int coordinateY = int.Parse(parts[1]);
            string description = parts[2];
            string type = parts[3];
            string locationEvent = parts[4];
            bool hidden = bool.Parse(parts[5]);

            return new Location(coordinateX, coordinateY, description, type, locationEvent, hidden);
        }

        public List<Quest> ReadQuests()
        {
            List<Quest> quests = new List<Quest>();

            using StreamReader sr = new StreamReader(TxtFilePath);
            for (int i = 0; i < File.ReadLines(TxtFilePath).Count(); i++)
            {
                string txtLine = sr.ReadLine();
                quests.Add(ReadQuestsFromTxtLine(txtLine));
            }

            return quests;
        }

        private Quest ReadQuestsFromTxtLine(string txtLine)
        {
            string[] parts = txtLine.Split(',');

            string title = parts[0];
            string description = parts[1];
            int difficulty = int.Parse(parts[2]);
            string reward = parts[3];
            bool isActive = bool.Parse(parts[4]);
            bool isComplete = bool.Parse(parts[5]);

            return new Quest(title, description, difficulty, reward, isActive, isComplete);
        }
    }
}