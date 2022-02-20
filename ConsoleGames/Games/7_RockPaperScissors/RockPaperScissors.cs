using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games
{
    public static class RockPaperScissors
    {
        private static int scoreA, scoreB = 0;

        private static string Rock = "O".Colourize(new Color("666666"));
        private static string Paper = "#".Colourize(Color.White);
        private static string Scissors = "8".Colourize(Color.Red) + "<";
        private static string Nuke = "(=X=)".Colourize(Color.Green);
        private static string Unknown = "??".Colourize(Color.Blue);

        public enum Tool : int
        {
            Rock,
            Paper,
            Scissors,

            NuclearBomb,
        }

        public static string ToPrintableString(Tool t)
        {
            return t switch
            {
                Tool.Rock => Rock + " (Rock)",
                Tool.Paper => Paper + " (Paper)",
                Tool.Scissors => Scissors + " (Scissors)",
                Tool.NuclearBomb => Nuke + " (Nuke)",
                _ => Unknown + " (Unknown)",
            };
        }

        public static void Play()
        {
            Console.WriteLine("[A]I or [H]uman?");
            char answer = char.ToLower(Console.ReadKey().KeyChar);

            switch (answer)
            {
                default:
                    break;
                case 'a':
                    AI();
                    break;
                case 'h':
                    Human();
                    break;
            }
        }

        public static void AI()
        {
            Tool ComputerChoice = ToTool('?');

            Console.CursorLeft = 0;
            Console.WriteLine($"[R]ock {Rock}, [P]aper {Paper}, [S]cissors {Scissors}?");
            Tool UserChoice = ToTool(char.ToLower(Console.ReadKey().KeyChar));

            int winner = Winner(UserChoice, ComputerChoice);

            Console.CursorLeft = 0;

            string UserChoiceStr = ToPrintableString(UserChoice);
            string ComputerChoiceStr = ToPrintableString(ComputerChoice);

            if (winner == 0)
                Console.WriteLine($"{UserChoiceStr} draws {ComputerChoiceStr}. " + "You Drew".Colourize(Color.Yellow));
            if (winner == 1)
            {
                scoreA++;
                Console.WriteLine($"{UserChoiceStr} beats {ComputerChoiceStr}. " + "You Win".Colourize(Color.Green));
            }
            if (winner == 2)
            {
                scoreB++;
                Console.WriteLine($"{UserChoiceStr} lost to {ComputerChoiceStr}. " + "You Lose".Colourize(Color.Red));
            }

            Console.WriteLine("Scores: " + $"{scoreA}-{scoreB}".ToString().Colourize(Color.Yellow) + "(AI-Human)");
            Console.WriteLine("Press any key to play again");
            Console.ReadKey();
            Console.Clear();
            AI();
        }

        public static void Human()
        {
            Console.CursorLeft = 0;
            Console.WriteLine($"Player 1: [R]ock {Rock}, [P]aper {Paper}, [S]cissors {Scissors}?");
            Tool ChoiceA = ToTool(char.ToLower(Console.ReadKey().KeyChar));

            Console.Clear();
            Console.WriteLine($"Player 2: [R]ock {Rock}, [P]aper {Paper}, [S]cissors {Scissors}?");
            Tool ChoiceB = ToTool(char.ToLower(Console.ReadKey().KeyChar));

            int winner = Winner(ChoiceA, ChoiceB);

            Console.CursorLeft = 0;

            string ChoiceAStr = ToPrintableString(ChoiceA);
            string ChoiceBStr = ToPrintableString(ChoiceB);

            if (winner == 0)
                Console.WriteLine($"{ChoiceAStr} draws {ChoiceBStr}. " + "You Draw".Colourize(Color.Yellow));
            else if (winner == 1)
            {
                Console.WriteLine($"{ChoiceAStr} beats {ChoiceBStr}. Player " + "1".Colourize(Color.Yellow) + " won");
                scoreA++;
            }
            else if (winner == 2)
            {
                Console.WriteLine($"{ChoiceAStr} lost to {ChoiceBStr}. Player " + "2".Colourize(Color.Yellow) + " won");
                scoreB++;
            }

            Console.WriteLine("Scores: " + $"{scoreA}-{scoreB}".ToString().Colourize(Color.Yellow) + "(A-B)");
            Console.WriteLine("Press any key to play again");
            Console.ReadKey();
            Console.Clear();
            Human();
        }

        ///<returns>1 if A is winner, 2 if B is winner</returns>
        private static int Winner(Tool a, Tool b)
        {
            if (a == b)
                return 0;

            if (b == Tool.NuclearBomb)
                return 2;

            return a switch
            {
                Tool.Rock => b == Tool.Paper ? 2 : 1,
                Tool.Paper => b == Tool.Scissors ? 2 : 1,
                Tool.Scissors => b == Tool.Rock ? 2 : 1,
                Tool.NuclearBomb => 1,
                _ => 0,
            };
        }

        private static Tool ToTool(char choice)
        {
            return choice switch
            {
                'r' => Tool.Rock,
                'p' => Tool.Paper,
                's' => Tool.Scissors,
                'n' => Tool.NuclearBomb,
                _ => (Tool)RandomNG.Int(0, 3),
            };
        }
    }
}
