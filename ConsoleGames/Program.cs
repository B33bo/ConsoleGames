using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ConsoleGames.Games;

using TextEngine.Demos;
using TextEngine.Colors;

namespace ConsoleGames
{
    class Program
    {
        public static Demo GameDemo = new();

        private static void Main(string[] args)
        {
            Clipboard.Copy("GH\r\nd");
            Demo.AddDemoType("userinput", typeof(UserInputDemoType));
            Demo.AddDemoType("compinput", typeof(ComputerInputDemoType));

            ConsoleColourManager.Enable();
            for (int i = 1; i < Games.Length; i++)
            {
                Console.WriteLine($"{i}. " + Games[i]);
            }

            if (!int.TryParse(Console.ReadLine(), out int game) || game >= Games.Length)
            {
                game = 0;
                Console.WriteLine("Game not found");
            }

            Console.Clear();

            Games[game].Play.Invoke();
        }

        public static GameData[] Games = new GameData[]
        {
            new GameData("Error", ErrorGame.Play, 0),
            new GameData("Higher Or Lower", HigherOrLower.Play, 1),
            new GameData("AZ", AZGame.Play, 1),
            new GameData("Snake", Snake.Play, 1),
            new GameData("TicTacToe", TicTacToe.Play, 2),
            new GameData("PubGame", Pub_Game.Play, 1),
            new GameData("Space Invaders", SpaceInvaders.Play, 1),
            new GameData("Rock Paper Scissors", RockPaperScissors.Play, 1, 2),
            new GameData("Fizzbuzz", FizzBuzz.Play, 1),
            new GameData("Wordle", Wordle.Play, 1),
            new GameData("NagARam", NagARam.Play, 1),
            new GameData("Minsweeper", Minesweeper.Play, 1),
            new GameData("Crossy Road", CrossyRoad.Play, 1),
            new GameData("Maths", Maths.Play, 1),
            new GameData("Tower Of Hanoi", TowerOfHanoi.Play, 1),
        };
    }
}
