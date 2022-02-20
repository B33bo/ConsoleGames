using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;
using ConsoleGames.Games.WordleGame;

namespace ConsoleGames.Games
{
    public static class Wordle
    {
        private static string[] Words;
        private static string CurrentWord;
        private static List<CharacterType[]> guesses = new();

        public static void Play()
        {
            Words = WordLoader.GetWords();

            Console.Clear();
            CurrentWord = RandomNG.Choice(Words);

            Keyboard.Init();

            //You have a 1/2309 chance of getting it instantly
            string NewGuess = "lucky";
            int i = 0;

            while (NewGuess != CurrentWord)
            {
                i++;
                NewGuess = ReplaceCheatCharacters(Console.ReadLine().ToLower()).Replace(" ", "");

                Console.CursorTop--;

                CharacterType[] guess = Guess(NewGuess);
                Console.WriteLine(ToPrettyString(guess, NewGuess));
                guesses.Add(guess);

                Keyboard.Reload(NewGuess, guess);
            }

            if (Console.CursorTop < 6)
                Console.CursorTop = 6;

            Console.WriteLine("You got it in " + i);

            Prettyify(guesses, out string Copy, out string Print);

            Console.Write(Print);
            Clipboard.Copy(Copy);
        }

        private static string ReplaceCheatCharacters(string guess)
        {
            string newStr = "";

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == '?')
                {
                    if (i >= CurrentWord.Length)
                        newStr += " ";
                    else
                        newStr += CurrentWord[i];
                }
                else
                    newStr += guess[i];
            }

            return newStr;
        }

        private static void Prettyify(List<CharacterType[]> words, out string Copy, out string Console)
        {
            string Green = "#".Colourize(Color.Green);
            string Yellow = "#".Colourize(Color.Yellow);
            string Grey = "#".Colourize(new("888888"));
            string Confused = "#".Colourize(Color.Magenta);

            Copy = "";
            Console = "";

            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < words[i].Length; j++)
                {
                    Copy += ToEmoji(words[i][j]);

                    Console += words[i][j] switch
                    {
                        CharacterType.Absent => Grey,
                        CharacterType.Present => Yellow,
                        CharacterType.Correct => Green,
                        _ => Confused,
                    };
                }
                Copy += "\n";
                Console += "\n";
            }
        }

        private static string ToEmoji(CharacterType type)
        {
            return type switch
            {
                CharacterType.Absent => "⬛",
                CharacterType.Present => "🟨",
                CharacterType.Correct => "🟩",
                _ => "❓",
            };
        }

        private static CharacterType[] Guess(in string s)
        {
            CharacterType[] returnValue = new CharacterType[s.Length];

            for (int i = 0; i < returnValue.Length; i++)
                returnValue[i] = CharacterType.Absent;

            Dictionary<char, int> alphabetIndexes = CharactersByFrequency(CurrentWord);

            //Check for greenies first
            for (int i = 0; i < s.Length; i++)
            {
                if (i >= CurrentWord.Length)
                    break;

                if (s[i] != CurrentWord[i])
                    continue;

                alphabetIndexes[s[i]]--;
                returnValue[i] = CharacterType.Correct;
            }

            //Look for yellowies
            for (int i = 0; i < s.Length; i++)
            {
                if (i >= CurrentWord.Length)
                    break;

                if (returnValue[i] == CharacterType.Correct)
                    continue;

                if (!alphabetIndexes.ContainsKey(s[i]))
                {
                    returnValue[i] = CharacterType.Absent;
                    continue;
                }

                if (alphabetIndexes[s[i]] > 0)
                {
                    alphabetIndexes[s[i]]--;
                    returnValue[i] = CharacterType.Present;
                    continue;
                }

                returnValue[i] = CharacterType.Absent;
            }

            return returnValue;
        }

        private static string ToPrettyString(CharacterType[] types, string s)
        {
            string prettyString = "";
            for (int i = 0; i < s.Length; i++)
            {
                prettyString += types[i].ToColourfulString(s[i].ToString());
            }

            return prettyString;
        }

        public static string ToColourfulString(this CharacterType characterType, string s)
        {
            return characterType switch
            {
                CharacterType.Absent => s.Colourize(new("888888")),
                CharacterType.Present => s.Colourize(Color.Yellow),
                CharacterType.Correct => s.Colourize(Color.Green),
                _ => s.Colourize(Color.White),
            };
        }

        private static Dictionary<char, int> CharactersByFrequency(string s)
        {
            Dictionary<char, int> returnValue = new();

            foreach (char character in s)
            {
                if (returnValue.ContainsKey(character))
                    returnValue[character]++;
                else
                    returnValue.Add(character, 1);
            }

            return returnValue;
        }

        public enum CharacterType
        {
            Unknown = -1,
            Absent,
            Present,
            Correct,
        }
    }
}
