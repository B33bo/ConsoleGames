using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games
{
    public static class TicTacToe
    {
        static int[,] Grid = new int[3, 3];
        const int Players = 2;
        static string Characters = " XO+-YVLS";
        static Color[] colors =
        {
            Color.Default,
            Color.Red,
            Color.Blue,
            Color.Yellow,
            Color.Cyan,
            Color.Green,
            Color.Magenta,
        };

        public static void Play()
        {
            Human();
        }

        private static void ReloadGrid(Vector2D pos, char Player)
        {
            string LineSeperators = "-";
            for (int i = 0; i < Grid.GetLength(0) * 2 - 1; i++)
            {
                if (i % 2 == 0)
                    LineSeperators += "+";
                else
                    LineSeperators += "-";
            }

            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                string newLine = "";
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (new Vector2D(j, i) == pos)
                    {
                        newLine += Player.ToString().Colourize(Color.Yellow) + "|";
                        continue;
                    }

                    string character = Characters[(int)Grid[i, j]].ToString();
                    if (Grid[i, j] == 0)
                        newLine += character + "|";
                    else
                        newLine += character.Colourize(colors[Grid[i, j]]) + "|";
                }
                Console.WriteLine(newLine);
                Console.WriteLine(LineSeperators);
            }
        }

        public static void Human()
        {
            Console.Clear();
            ReloadGrid(new Vector2D(-1, -1), ' ');

            int CurrentPlayer = 0;

            int winner = 0;
            while (winner == 0)
            {
                CurrentPlayer++;
                CurrentPlayer %= Players;

                Vector2D pos = Select(Characters[CurrentPlayer + 1]);
                Grid[pos.X, pos.Y] = CurrentPlayer + 1;
                ReloadGrid(new(-1, -1), ' ');

                winner = GetWinner();
            }

            if (winner == -1)
                Console.WriteLine("Nobody won");
            else
                Console.WriteLine(Characters[winner].ToString().Colourize(colors[winner]) + " won.");
        }

        private static int GetWinner()
        {
            /**Rows**/
            for (int i = 0; i < Grid.GetLength(1); i++)
            {
                int characterToTry = Grid[i, 0];

                if (characterToTry == 0)
                    continue;

                if (Grid[i, 1] != characterToTry)
                    continue;
                if (Grid[i, 2] != characterToTry)
                    continue;
                return (int)characterToTry;
            }

            /**Collums**/
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                int characterToTry = Grid[0, i];

                if (characterToTry == 0)
                    continue;

                if (Grid[1, i] != characterToTry)
                    continue;
                if (Grid[2, i] != characterToTry)
                    continue;
                return characterToTry;
            }

            /**Diagonals**/
            int DiagonalCharToTry = Grid[0, 0];
            if (DiagonalCharToTry != 0 &&
                Grid[1, 1] == DiagonalCharToTry &&
                Grid[2, 2] == DiagonalCharToTry)
                return DiagonalCharToTry;

            DiagonalCharToTry = Grid[0, 2];
            if (DiagonalCharToTry != 0 &&
                Grid[1, 1] == DiagonalCharToTry &&
                Grid[2, 0] == DiagonalCharToTry)
                return DiagonalCharToTry;

            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (Grid[i, j] == 0)
                        return 0;
                }
            }

            return -1;
        }

        public static Vector2D Select(char NewKey)
        {
            Vector2D position = Vector2D.Zero;
            ConsoleKey key = 0;

            while (key != ConsoleKey.Spacebar)
            {
                key = Console.ReadKey().Key;

                switch (key)
                {
                    default:
                        break;
                    case ConsoleKey.W:
                        position += Vector2D.Up;
                        break;
                    case ConsoleKey.A:
                        position += Vector2D.Left;
                        break;
                    case ConsoleKey.S:
                        position += Vector2D.Down;
                        break;
                    case ConsoleKey.D:
                        position += Vector2D.Right;
                        break;
                }

                position.X %= Grid.GetLength(0);
                position.Y %= Grid.GetLength(1);

                if (position.X < 0)
                    position.X += Grid.GetLength(0);

                if (position.Y < 0)
                    position.Y += Grid.GetLength(1);

                ReloadGrid(position, NewKey);

                if (Grid[position.Y, position.X] > 0)
                    key = 0;
            }
            return new Vector2D(position.Y, position.X);
        }
    }
}
