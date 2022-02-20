using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games
{
    public static class Maths
    {
        private static int digits;
        private static int MaxNumber = 0;
        private static int score = 0;

        private static Operation choice;

        public static void Play()
        {
            Console.WriteLine("Difficulty (digits)");
            if (!int.TryParse(Console.ReadLine(), out digits))
                digits = 3;

            for (int i = 0; i < digits; i++)
            {
                MaxNumber = MaxNumber * 10 + 9;
            }

            Console.WriteLine("Pick the operations you would like to play");
            Console.WriteLine("Type 'Done' to play");

            string Tick = "✓".Colourize(Color.Green);
            string Cross = "X".Colourize(Color.Red);
            choice = Operation.all;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Type \"Start\" to continue");
                string AllEnabled = choice == Operation.all ? Tick : Cross;
                Console.WriteLine($"[{AllEnabled}] {Operation.all}");

                for (byte i = 1; i <= 128 && i > 0; i *= 2)
                {
                    bool CurrentIsEnabled = ((byte)choice & i) == i;
                    string EnabledString = CurrentIsEnabled ? Tick : Cross;

                    Console.WriteLine($"[{EnabledString}] {(Operation)i}");
                }

                string answer = Console.ReadLine().ToLower();
                Operation newChoice;

                if (answer == "start")
                    break;

                if (answer == "all" || answer == "1")
                {
                    if (choice == Operation.all)
                        choice = Operation.none;
                    else
                        choice = Operation.all;
                    continue;
                }

                if (byte.TryParse(answer, out byte index))
                {
                    //index of 4 means times which is 4 or 2^2
                    //2^index-2
                    //index must be between 2-9

                    if (index < 2 || index > 9)
                    {
                        Console.WriteLine("Must be between 1 and 7");
                        continue;
                    }

                    newChoice = (Operation)Math.Pow(2, index - 2);
                }
                else if (!Enum.TryParse(answer, out newChoice))
                {
                    Console.WriteLine("That is not a valid input!");
                }

                bool isAlreadyTicked = (choice & newChoice) == newChoice;

                if (isAlreadyTicked)
                    choice = (Operation)((byte)choice - (byte)newChoice);
                else
                    choice = (Operation)((byte)choice + (byte)newChoice);
            }

            Console.Clear();
            while (true)
            {
                Game();
            }
        }

        private static void Game()
        {
            string correct = "Correct!".Colourize(Color.Green);
            string incorrect = "incorrect!".Colourize(Color.Red);

            for (byte i = 1; i <= 128 && i > 0; i *= 2)
            {
                bool CurrentIsEnabled = ((byte)choice & i) == i;

                if (!CurrentIsEnabled)
                    continue;

                int a = RandomNG.Int(0, MaxNumber);
                int b = RandomNG.Int(0, MaxNumber);
                Operation currentOperation = (Operation)i;

                if (currentOperation == Operation.factorial)
                    Console.Write($"{a}{currentOperation.ToOperatorString().Colourize(Color.Yellow)} = ");
                else
                    Console.Write($"{a} {currentOperation.ToOperatorString().Colourize(Color.Yellow)} {b} = ");

                bool guessedCorrectly = AskQuestion(currentOperation, a, b);
                Console.Clear();

                if (guessedCorrectly)
                {
                    Console.WriteLine(correct);
                    score++;
                }
                else
                {
                    Console.WriteLine(incorrect);
                    score--;
                }

                Console.WriteLine("Score: " + score.ToString().Colourize(Color.Yellow));
            }
        }

        private static bool AskQuestion(Operation operation, int a, int b)
        {
            //Rounds down but so should all the others
            string userAnswerSTR = Console.ReadLine().Split('.')[0];
            if (!double.TryParse(userAnswerSTR, out double guess))
                return false;

            if ((operation == Operation.mod ||
                operation == Operation.divide ||
                operation == Operation.root) && b == 0)
                return true;

            if (operation == Operation.powers && a == 0 && b == 0)
                return true;

            return operation switch
            {
                Operation.none => true,
                Operation.plus => a + b == guess,
                Operation.minus => a - b == guess,
                Operation.times => a * b == guess,
                Operation.divide => a / b == guess,
                Operation.mod => a % b == guess,
                Operation.powers => ((int)Math.Pow(a, b) + double.Epsilon) == guess,
                Operation.root => (int)(Math.Pow(a, 1 / b) + double.Epsilon) == guess,
                Operation.factorial => Factorial(a) == guess,
                Operation.all => true,
                _ => false,
            };
        }

        private static string ToOperatorString(this Operation operation)
        {
            return operation switch
            {
                Operation.none => " ",
                Operation.plus => "+",
                Operation.minus => "-",
                Operation.times => "*",
                Operation.divide => "/",
                Operation.mod => "%",
                Operation.powers => "^",
                Operation.root => "✓",
                Operation.factorial => "!",
                Operation.all => "all",
                _ => operation.ToString(),
            };
        }

        private static int Factorial(int num)
        {
            if (num <= 0)
                return 1;
            
            return Factorial(num - 1) * num;
        }

        private enum Operation : byte
        {
            none = 0,
            plus = 1,
            minus = 2,
            times = 4,
            divide = 8,
            mod = 16,
            powers = 32,
            root = 64,
            factorial = 128,
            all = byte.MaxValue,
        }
    }
}
