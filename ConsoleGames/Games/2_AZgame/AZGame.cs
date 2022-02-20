using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games
{
    public static class AZGame
    {
        static List<string> Game;
        static List<char> Characters = new();
        static string Subject;

        public static void Play()
        {
            Console.WriteLine("Choose a game from the list:");
            string[] Games = Directory.GetFiles("Games/2_AZgame/Games/");

            for (int i = 0; i < Games.Length; i++)
            {
                Games[i] = Path.GetFileNameWithoutExtension(Games[i]);
                Console.WriteLine(Games[i]);
            }

            string gameToPlay = Console.ReadLine();
            if (!File.Exists(gameToPlay))
            {
                gameToPlay = "Games/2_AZgame/Games/" + gameToPlay;
                if (!gameToPlay.EndsWith(".txt"))
                    gameToPlay += ".txt";
            }

            //Still not found
            if (!File.Exists(gameToPlay))
            {
                Console.WriteLine($"Invalid File {gameToPlay}".Colourize(Color.Red));
                return;
            }    

            Game = File.ReadAllLines(gameToPlay).ToList();
            Subject = Path.GetFileNameWithoutExtension(gameToPlay);

            for (int i = 0; i < Game.Count; i++)
            {
                Game[i] = Game[i].ToLower().Trim();

                if (Game[i].Length < 1)
                    continue;

                if (Characters.Contains(Game[i][0]))
                    continue;

                Characters.Add(Game[i][0]);
            }

            Console.Clear();
            PlayAZ();
        }

        public static void PlayAZ()
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                while (true)
                {
                    Console.WriteLine("Subject: " + Subject.Colourize(Color.Yellow));
                    Console.WriteLine("Character: " + Characters[i].ToString().Colourize(Color.Yellow));
                    string Guess = Console.ReadLine().ToLower();
                    Console.Clear();

                    if (Guess.Length < 1)
                        continue;

                    if (Game.Contains(Guess) && Guess[0] == Characters[i])
                        break;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("INVALID");
                    Console.ResetColor();
                }
            }
        }
    }
}
