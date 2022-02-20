using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TextEngine.Colors;

namespace ConsoleGames.Games
{
    public static class TowerOfHanoi
    {
        private static int Rings = 0;
        private static int Prongs = 3;
        private static Color[] colors;

        private static int[,] ProngsAndRings;

        private static int ProngLength;

        public static void Play()
        {
            Console.WriteLine("How many rings?");

            if (!int.TryParse(Console.ReadLine(), out Rings))
                Rings = 5;

            Console.WriteLine("How many prongs?");
            if (!int.TryParse(Console.ReadLine(), out Prongs))
                Prongs = 3;

            colors = new Color[Rings];

            for (int i = 0; i < colors.Length; i++)
            {
                float percentage = i / (float)Rings;
                colors[i] = Color.ToRGB((360f * percentage, 1, 1));
            }

            if (Prongs < 0 || Rings < 0)
                return;

            ProngsAndRings = new int[Prongs, Rings];

            ProngLength = Rings;

            for (int i = 0; i < Rings; i++)
            {
                ProngsAndRings[0, i] = i + 1;
            }

            Console.Clear();

            PrintAllProngs();

            Console.SetCursorPosition(0, 5);

            while (true)
            {
                string[] UserChoice = Console.ReadLine().Split('-');

                if (UserChoice.Length < 2)
                    continue;

                if (!int.TryParse(UserChoice[0], out int ProngA))
                    continue;
                if (!int.TryParse(UserChoice[1], out int ProngB))
                    continue;

                Console.Clear();
                MakeMove(ProngA - 1, ProngB - 1);
                PrintAllProngs();

                Console.SetCursorPosition(0, Rings + 2);

                if (HasWon())
                {
                    Console.WriteLine("You win");
                    return;

                }
            }
        }

        private static void PrintAllProngs()
        {
            for (int i = 0; i < Prongs; i++)
            {
                Print(i, (i + 1) * Rings * 2);
            }
        }

        private static void Print(int prong, int CursorLeft)
        {
            Console.CursorTop = 0;

            for (int i = 0; i < ProngLength; i++)
            {
                if (CursorLeft > Console.WindowWidth)
                    continue;

                Console.CursorLeft = CursorLeft;

                Console.WriteLine("|".Colourize(new("666666")));
            }

            for (int i = 0; i < ProngLength; i++)
            {
                int ring = ProngsAndRings[prong, i];

                if (ring == 0)
                    continue;

                string ringToPrint = GetRing(prong, i);

                if (CursorLeft - ProngsAndRings[prong, i] + 1 < 0)
                    continue;

                Console.SetCursorPosition(CursorLeft - ProngsAndRings[prong, i] + 1, i);
                Console.Write(ringToPrint);
            }
        }

        private static bool HasWon()
        {
            int LastProng = Prongs - 1;

            for (int i = 0; i < Rings; i++)
            {
                if (ProngsAndRings[LastProng, i] == 0)
                    return false;
            }
            return true;
        }

        private static string GetRing(int prong, int ring)
        {
            string ringString = "";
            for (int i = 0; i < ProngsAndRings[prong, ring] * 2 - 1; i++)
            {
                ringString += "#";
            }

            return ringString.Colourize(colors[ProngsAndRings[prong, ring] - 1]);
        }

        private static void MakeMove(int prong1, int prong2)
        {
            if (prong1 == prong2)
                return;

            if (prong1 >= Prongs || prong1 < 0 || prong2 >= Prongs || prong2 < 0)
                return;

            int topOfA = GetTopIndexOf(prong1);
            int topOfB = GetTopIndexOf(prong2);

            if (topOfA == -1)
                return;

            if (topOfB == -1)
            {
                ProngsAndRings[prong2, Rings - 1] = ProngsAndRings[prong1, topOfA];
                ProngsAndRings[prong1, topOfA] = 0;
                return;
            }

            if (ProngsAndRings[prong1, topOfA] > ProngsAndRings[prong2, topOfB])
                return;

            ProngsAndRings[prong2, topOfB - 1] = ProngsAndRings[prong1, topOfA];

            if (topOfA < 0)
                return;
            ProngsAndRings[prong1, topOfA] = 0;
        }

        private static int GetTopIndexOf(int prong)
        {
            int length = Rings;

            for (int i = 0; i < length; i++)
            {
                if (ProngsAndRings[prong, i] == 0)
                    continue;

                return i;
            }
            return -1;
        }
    }
}
