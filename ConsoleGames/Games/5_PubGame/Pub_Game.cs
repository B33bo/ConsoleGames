using System;
using TextEngine;
using ConsoleGames.Games.PubGame;

namespace ConsoleGames.Games
{
    public class Pub_Game
    {
        public static int score = 600;

        public static void Play()
        {
            Man man = new();
            Game.Screen = new(40, 20);

            Game.AddObject(man);

            Pub pub = new();
            Game.AddObject(pub);

            Game.Start();

            for (int i = 0; i < 40; i++)
            {
                Tree tree = new();
                Game.AddObject(tree);
            }

            for (int i = 12; i < 25; i++)
            {
                if (i % 4 == 0 || i % 3 == 0)
                    continue;
                Car newCar = new(i, i % 3 * 50);
                Game.AddObject(newCar);
            }

            Game.OnQuitGame += EndOfGame;
        }

        static void EndOfGame()
        {
            Console.WriteLine(GetRating(score));
        }

        static string GetRating(int score)
        {
            if (score < 0)
                return "Would have done better getting run over!!";

            if (score < 100)
                return "Seen better, but at least you didn't die";

            if (score < 200)
                return "Need to do better";

            if (score < 300)
                return "hmm, not too bad";

            if (score < 600)
                return "average - good, keep trying...";

            if (score == 600)
                return "Perfect, need to try with more obstacles!!";

            return "You're literally hacking";
        }
    }
}
