using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games
{
    public static class HigherOrLower
    {
        public static void Play()
        {
            Console.WriteLine("Maximum number allowed?");

            Console.WriteLine("Who guesses? (C)omputer, (P)layer");
            char UserKey = char.ToLower(Console.ReadKey().KeyChar);
            Console.Clear();

            switch (UserKey)
            {
                default:
                    break;
                case 'c':
                    ComputerGuess();
                    break;
                case 'b':
                    BinarySearchGuess();
                    break;
                case 'p':
                    UserGuess();
                    break;
            }
        }

        public static void ComputerGuess()
        {
            Console.WriteLine("Think of a random number");
            int minBound = 0;
            int maxBound = int.MaxValue;

            for (int i = 0; ; i++)
            {
                int number = RandomNG.Int(minBound, maxBound);

                string minBoundStr = minBound.ToString().Colourize(Color.Yellow);
                string maxBoundStr = maxBound.ToString().Colourize(Color.Yellow);

                string MiddleStr = ((maxBound - minBound) / 2 + minBound).ToString().Colourize(Color.Yellow);
                string numberStr = number.ToString().Colourize(Color.Cyan);

                Console.WriteLine($"Bounds: {minBoundStr}-{maxBoundStr} (Middle = {MiddleStr})");
                Console.WriteLine($"(H)igher/(L)ower/(E)qual to {numberStr}?");

                char input = char.ToLower(Console.ReadKey().KeyChar);

                if (input == 'h' || input == '>')
                    minBound = number + 1;
                else if (input == 'l' || input == '<')
                    maxBound = number - 1;
                else if (input == 'e' || input == '=')
                {
                    Console.WriteLine($"The number was {numberStr}. I won in {i} guesses");
                    return;
                }
                Console.Clear();

                if (minBound > maxBound)
                {
                    Console.WriteLine("MinBound > MaxBound. You cheated. I'm restarting");
                    maxBound = int.MaxValue;
                    minBound = 0;
                }
            }
        }

        public static void BinarySearchGuess()
        {
            Console.WriteLine("Think of a random number");
            int minBound = 0;
            int maxBound = int.MaxValue;

            for (int i = 0; ; i++)
            {
                int number = (maxBound + minBound) / 2;

                string minBoundStr = minBound.ToString().Colourize(Color.Yellow);
                string maxBoundStr = maxBound.ToString().Colourize(Color.Yellow);

                string MiddleStr = ((maxBound - minBound) / 2 + minBound).ToString().Colourize(Color.Yellow);
                string numberStr = number.ToString().Colourize(Color.Cyan);

                Console.WriteLine($"Bounds: {minBoundStr}-{maxBoundStr} (middle = {MiddleStr})");
                Console.WriteLine($"(H)igher/(L)ower/(E)qual to {numberStr}?");

                char input = char.ToLower(Console.ReadKey().KeyChar);

                if (input == 'h' || input == '>')
                    minBound = number + 1;
                else if (input == 'l' || input == '<')
                    maxBound = number - 1;
                else if (input == 'e' || input == '=')
                {
                    Console.WriteLine($"The number was {numberStr}. I won in {i} guesses");
                    return;
                }
                Console.Clear();

                if (minBound > maxBound)
                {
                    Console.WriteLine("MinBound > MaxBound. You cheated. I'm restarting");
                    maxBound = int.MaxValue;
                    minBound = 0;
                }
            }
        }

        public static void UserGuess()
        {
            Console.WriteLine("Enter max number:");
            if (!int.TryParse(Console.ReadLine(), out int MaxNumber))
                MaxNumber = int.MaxValue;

            int RandomNumber = RandomNG.Int(0, MaxNumber);

            int minBound = 0, maxBound = MaxNumber;

            for (int i = 1; ; i++)
            {
                string minBoundStr = minBound.ToString().Colourize(Color.Yellow);
                string maxBoundStr = maxBound.ToString().Colourize(Color.Yellow);
                string MiddleStr = ((maxBound - minBound) / 2 + minBound).ToString().Colourize(Color.Yellow);

                Console.WriteLine($"Bounds: {minBoundStr}-{maxBoundStr} (middle = {MiddleStr})");
                Console.WriteLine("Enter number:");

                if (!int.TryParse(Console.ReadLine(), out int guess))
                {
                    Console.Clear();
                    Console.WriteLine("That's not a number");
                    continue;
                }

                if (guess > RandomNumber)
                {
                    Console.Clear();
                    Console.WriteLine("Too high!");

                    if (maxBound > guess)
                        maxBound = guess;

                    continue;
                }

                if (guess < RandomNumber)
                {
                    Console.Clear();
                    Console.WriteLine("Too low!");

                    if (minBound < guess)
                        minBound = guess;
                    continue;
                }

                if (guess == RandomNumber)
                {
                    Console.Clear();
                    Console.WriteLine("You win! it was: " + RandomNumber);
                    Console.WriteLine($"You won in {i.ToString().Colourize(Color.Yellow)} guesses");
                    return;
                }
            }
        }
    }
}
