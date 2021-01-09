using System.IO;

namespace GameRPG
{
    public class TxtWriter
    {
        private readonly string CharacterStats;
        public TxtWriter(string characterStats)
        {
            this.CharacterStats = characterStats;
        }
        public void WriteCharacters()
        {
            File.WriteAllText(@"C:\Users\carl.hooper\Desktop\GameoStuffo\TextFiles\SaveCharacter.txt", CharacterStats);
        }
    }
}
