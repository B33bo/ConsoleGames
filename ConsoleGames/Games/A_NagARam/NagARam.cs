using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TextEngine.Colors;

namespace ConsoleGames.Games
{
    public static class NagARam
    {
        private static string[] Words;
        private static string CurrentWord;

        public static void Play()
        {
            Words = WordLoader.GetWords();
            CurrentWord = TextEngine.RandomNG.Choice(Words).ToLower().Trim();
            Console.Clear();

            Console.WriteLine("Type '?' for another scramble");

            Console.WriteLine(Anagram(CurrentWord).Colourize(Color.Yellow));
            Console.WriteLine("Make a guess: (or type '?' for another scramble)");

            string guess = "";
            int i = 0;

            while (guess != CurrentWord)
            {
                i++;
                guess = Console.ReadLine().ToLower().Trim();
                Console.CursorTop--;
                Console.WriteLine(ShowCorrectChars(guess));

                if (guess == "?")
                    Console.WriteLine(Anagram(CurrentWord).Colourize(Color.Yellow));
            }

            Console.WriteLine("Correct! You got it in " + i.ToString().Colourize(Color.Yellow));
        }
        
        private static string ShowCorrectChars(string guess)
        {
            string prettyString = "";
            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == CurrentWord[i])
                    prettyString += guess[i].ToString().Colourize(Color.Green);
                else
                    prettyString += guess[i];
            }

            return prettyString;
        }

        private static string Anagram(string word)
        {
            string anagram = "";
            List<int> UsedIndexes = new();

            while (UsedIndexes.Count < word.Length)
            {
                int newIndex = TextEngine.RandomNG.Int(0, word.Length);

                if (UsedIndexes.Contains(newIndex))
                    continue;

                UsedIndexes.Add(newIndex);
                anagram += word[newIndex];
            }

            return anagram;
        }
    }
}
