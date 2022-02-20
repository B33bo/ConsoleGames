using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGames
{    
    public class GameData
    {
        public string Name;
        public Action Play;
        public int MinPlayers;
        public int MaxPlayers;

        public GameData(string Name, Action Play, int Players)
        {
            this.Name = Name;
            this.Play = Play;

            MinPlayers = Players;
            MaxPlayers = Players;
        }

        public GameData(string Name, Action Play, int MinPlayers, int MaxPlayers)
        {
            this.Name = Name;
            this.Play = Play;

            this.MinPlayers = MinPlayers;
            this.MaxPlayers = MaxPlayers;
        }

        public override string ToString()
        {
            if (MinPlayers == MaxPlayers)
                return $"{Name} ({MinPlayers}p)";

            return $"{Name} ({MinPlayers}-{MaxPlayers}p)";
        }
    }
}
