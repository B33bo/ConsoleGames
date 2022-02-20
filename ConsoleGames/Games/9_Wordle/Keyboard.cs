using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;

namespace ConsoleGames.Games.WordleGame
{
    public static class Keyboard
    {
        private const int left = 10;
        public static Dictionary<char, Wordle.CharacterType> Types = new();

        private const string KeyboardOrganised = "qwertyuiop/asdfghjkl/zxcvbnm";

        public static void Init()
        {
            for (int i = 0; i < KeyboardOrganised.Length; i++)
            {
                if (KeyboardOrganised[i] == '/')
                    continue;
                Types.Add(KeyboardOrganised[i], Wordle.CharacterType.Unknown);
            }

            Redraw();
        }

        private static void Redraw()
        {
            int OldLeft = Console.CursorLeft;
            int OldTop = Console.CursorTop;

            Console.SetCursorPosition(left, 2);
            Console.Write(GetKeyboardRow(0));

            Console.SetCursorPosition(left + 1, 3);
            Console.Write(GetKeyboardRow(1));

            Console.SetCursorPosition(left + 2, 4);
            Console.Write(GetKeyboardRow(2));

            Console.SetCursorPosition(OldLeft, OldTop);
        }

        private static string GetKeyboardRow(int layer)
        {
            string Row = "";

            string Keyboard = KeyboardOrganised.Split('/')[layer];
            Wordle.CharacterType[] colours = new Wordle.CharacterType[Keyboard.Length];

            for (int i = 0; i < Keyboard.Length; i++)
            {
                Wordle.CharacterType currentColour = Types[Keyboard[i]];
                Row += currentColour.ToColourfulString(Keyboard[i].ToString()) + " ";
            }

            return Row;
        }

        public static void Reload(string guess, Wordle.CharacterType[] characterTypes)
        {
            for (int i = 0; i < guess.Length; i++)
            {
                char character = guess[i];

                if (!Types.ContainsKey(character))
                    continue;

                //Correct, Unknown, Present, Absent
                if (Types[character] == Wordle.CharacterType.Correct)
                    continue;

                //Unknown, Present, Absent
                if (Types[character] == Wordle.CharacterType.Unknown)
                {
                    Types[character] = characterTypes[i];
                    continue;
                }

                //Absent, Present
                if (Types[character] == Wordle.CharacterType.Present)
                {
                    if (characterTypes[i] == Wordle.CharacterType.Correct)
                        Types[character] = characterTypes[i];
                    continue;
                }

                //Absent
                Types[character] = characterTypes[i];
            }

            Redraw();
        }
    }
}
