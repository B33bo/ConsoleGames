using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ConsoleGames.Games
{
    public static class FizzBuzz
    {
        public static void Play()
        {
            long i = 1;
            string userinput = "1";
            string realAnswer = "1";
            bool GoingUp = true;

            while (userinput == realAnswer)
            {
                if (i == 0)
                    continue;

                realAnswer = GetFizzbuzzAt(i);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.CursorLeft = 0;

                Console.WriteLine(i.ToString());
                Console.ResetColor();

                Console.SetCursorPosition(0, Console.CursorTop-1);
                userinput = Console.ReadLine().ToLower();

                if (userinput.StartsWith("-"))
                    userinput = userinput[1..];

                if (userinput == "")
                {
                    Console.SetCursorPosition(0, Console.CursorTop-1);
                    userinput = i.ToString();
                    Console.Write(userinput + "\n");
                }
                else
                {
                    string emptyText = GetEmpty(i);
                    Console.SetCursorPosition(userinput.Length, Console.CursorTop - 1);
                    Console.Write(emptyText + "\n");
                }

                if (i == long.MaxValue || i == long.MinValue)
                {
                    Console.WriteLine("Integer limit reached. Negatives next");
                    GoingUp = !GoingUp;
                    i = 0;
                }

                if (GoingUp)
                    i++;
                else
                    i--;
            }

            long previous = GoingUp ? i - 1 : i + 1;
            Console.WriteLine($"Incorrect! You typed: {userinput}. Correct answer: {realAnswer}. (at {previous})");
        }

        private static string GetFizzbuzzAt(long index)
        {
            string s = "";
            if (index % 3 == 0)
                s += "fizz";
            if (index % 5 == 0)
                s += "buzz";

            if (s == "")
                return index.ToString();
            return s;
        }

        private static string GetEmpty(long number)
        {
            string s = "";

            for (int i = 0; i < number.ToString().Length; i++)
                s += " ";

            return s;
        }
    }
}
