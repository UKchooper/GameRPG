using System;
using System.Collections.Generic;
using System.Text;

namespace GameRPG
{
    public class Quest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public string Reward { get; set; }

        public Quest(string title, string description, int difficulty, string reward)
        {
            this.Title = title;
            this.Description = description;
            this.Difficulty = difficulty;
            this.Reward = reward;
        }
    }
}
