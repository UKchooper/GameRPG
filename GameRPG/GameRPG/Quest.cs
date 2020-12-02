using Prism.Mvvm;

namespace GameRPG
{
    public class Quest : BindableBase
    {
        private string title;
        private string description;
        private int difficulty;
        private string reward;
        private bool isActive;
        private bool isComplete;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                RaisePropertyChanged(nameof(this.Title));
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
                RaisePropertyChanged(nameof(this.Description));
            }
        }

        public int Difficulty
        {
            get
            {
                return difficulty;
            }
            set
            {
                difficulty = value;
                RaisePropertyChanged(nameof(this.Difficulty));
            }
        }

        public string Reward
        {
            get
            {
                return reward;
            }
            set
            {
                reward = value;
                RaisePropertyChanged(nameof(this.Reward));
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                RaisePropertyChanged(nameof(this.IsActive));
            }
        }

        public bool IsComplete
        {
            get
            {
                return isComplete;
            }
            set
            {
                isComplete = value;
                RaisePropertyChanged(nameof(this.IsComplete));
            }
        }

        public Quest(string title, string description, int difficulty, string reward, bool isActive, bool isComplete)
        {
            this.Title = title;
            this.Description = description;
            this.Difficulty = difficulty;
            this.Reward = reward;
            this.IsComplete = isComplete;
            this.IsActive = isActive;
        }
    }
}
