using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGames
{
    public static class WordLoader
    {
        public static string[] GetWords()
        {
            Console.WriteLine("[R]andom word, [C]ustom word, or [G]roup of words");
            char choice = char.ToLower(Console.ReadKey().KeyChar);

            switch (choice)
            {
                default:
                    if (File.Exists("words.txt"))
                        return File.ReadAllLines("words.txt");
                    return new string[] { "wordsfilenotfound" };
                case 'c':
                    Console.WriteLine("Enter a word:");
                    return new string[] { Console.ReadLine().ToLower() };
                case 'g':
                    Console.WriteLine("Enter file path:");
                    string filePath = Console.ReadLine();

                    if (File.Exists(filePath))
                        return File.ReadAllLines(filePath);

                    return new string[] { "invalidfilefpath" };
            }
        }
    }
}
