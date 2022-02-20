using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games
{
    public static class Minesweeper
    {
        public static Cell[,] cells;
        private static Vector2D cellCount;
        private static Vector2D Position;
        private static int bombCount;
        private static int revealedTiles = 0;
        private static bool didLose = false;

        private static Color[] ColourOfNumbers = new Color[]
        {
            new Color("666666"),
            Color.Blue,
            new Color("008000"),
            Color.Red,
            new Color("000080"),
            new Color("800000"),
            new Color("008080"),
            new Color("080808"),
            new Color("808080"),
        };

        public static void Play()
        {
            Console.WriteLine("Enter width, height, and bombs (format: width x height x bombs)");
            string[] userInput = Console.ReadLine().Replace(" ", "").Split('x');

            if (userInput.Length < 3)
            {
                if (userInput.Length == 2)
                {
                    Console.WriteLine("How many bombs?");
                    userInput = new string[] { userInput[0], userInput[1], Console.ReadLine() };
                }
                else
                    return;
            }

            int[] userInputInt = new int[userInput.Length];

            for (int i = 0; i < userInput.Length; i++)
            {
                if (!int.TryParse(userInput[i], out int res))
                    userInputInt[i] = 30;
                else
                    userInputInt[i] = res;

                userInputInt[i] = Math.Abs(userInputInt[i]);
            }

            cells = new Cell[userInputInt[0], userInputInt[1]];
            bombCount = userInputInt[2];

            cellCount = new(cells.GetLength(0), cells.GetLength(1));
            int totalCellCount = cellCount.X * cellCount.Y;

            if (bombCount > totalCellCount)
                bombCount = totalCellCount;

            //Initialise board
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                    cells[i, j] = new(new Vector2D(i, j), false, FlagType.Unflagged);
            }

            //Generate bombs
            for (int i = 0; i < bombCount;)
            {
                Vector2D position = RandomNG.Vector(Vector2D.Zero, cellCount);

                if (cells[position.X, position.Y].IsBomb)
                    continue;

                cells[position.X, position.Y].IsBomb = true;
                i++;
            }

            //Check "bombsAroundMe"

            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                    cells[i, j].SetBombsAroundMe();
            }

            Console.Clear();
            while (totalCellCount - revealedTiles > bombCount && !didLose)
            {
                DrawBoard();
                GetUserInput();
            }
            DrawBoard();

            if (didLose)
                Console.WriteLine("You hit a bomb!");
            else
                Console.WriteLine("You won!");
        }

        public static void DrawBoard()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < cells.GetLength(1); i++)
            {
                string currentRow = "";
                for (int j = 0; j < cells.GetLength(0); j++)
                {
                    if (Position == new Vector2D(j, i))
                        currentRow += cells[j, i].ToCellString(false).Colourize(Color.Black, Color.Yellow);

                    else
                        currentRow += cells[j, i].ToCellString(true);
                }
                Console.WriteLine(currentRow);
            }
        }

        public static void GetUserInput()
        {
            char key = char.ToLower(Console.ReadKey().KeyChar);

            switch (key)
            {
                default:
                    break;
                case 'w':
                    Position += Vector2D.Up;
                    break;
                case 'a':
                    Position += Vector2D.Left;
                    break;
                case 's':
                    Position += Vector2D.Down;
                    break;
                case 'd':
                    Position += Vector2D.Right;
                    break;
                case ' ':
                    Cell cell = At(Position);

                    if (cell is null)
                        break;
                    if (cell.flag == FlagType.Flagged)
                        break;

                    if (cell.flag == FlagType.Revealed)
                        cell.RevealAroundRevealedTile();
                    else
                        cell.Reveal();
                    break;
                case '?':

                    Cell questionTarget = At(Position);

                    if (questionTarget is null)
                        break;

                    questionTarget.Question();
                    break;
                case 'f':
                    Cell flagTarget = At(Position);

                    if (flagTarget is null)
                        break;

                    flagTarget.Flag();
                    break;
            }
        }

        public static Cell At(Vector2D index)
        {
            if (index.X < 0 || index.Y < 0)
                return null;

            if (index.X >= cells.GetLength(0) || index.Y >= cells.GetLength(1))
                return null;
            return cells[index.X, index.Y];
        }

        public class Cell
        {
            public Vector2D CellPos;
            public bool IsBomb;
            public FlagType flag;
            public int bombsAroundMe;

            public Cell[] CellsAroundMe
            {
                get
                {
                    Cell[] cells = new Cell[9];

                    cells[0] = At(CellPos + new Vector2D(-1, -1)); //Topleft
                    cells[1] = At(CellPos + new Vector2D(0, -1)); //Top
                    cells[2] = At(CellPos + new Vector2D(1, -1)); //TopRight
                    cells[3] = At(CellPos + new Vector2D(-1, 0)); //Left
                    cells[4] = At(CellPos + new Vector2D(1, 0)); //Right
                    cells[5] = this;
                    cells[6] = At(CellPos + new Vector2D(-1, 1)); //BottomLeft
                    cells[7] = At(CellPos + new Vector2D(0, 1)); //Bottom
                    cells[8] = At(CellPos + new Vector2D(1, 1)); //BottomRight

                    return cells;
                }
            }

            public void Reveal()
            {
                if (flag == FlagType.Revealed)
                    return;

                if (IsBomb)
                    didLose = true;

                flag = FlagType.Revealed;
                revealedTiles++;

                if (bombsAroundMe != 0)
                    return;

                RevealAround();
            }

            public void RevealAround()
            {
                Cell[] cellsAround = CellsAroundMe;

                for (int i = 0; i < cellsAround.Length; i++)
                {
                    if (cellsAround[i] is null)
                        continue;

                    cellsAround[i].Reveal();
                }
            }

            public void RevealAroundRevealedTile()
            {
                Cell[] cellsAround = CellsAroundMe;
                int flaggedBombs = 0;

                for (int i = 0; i < cellsAround.Length; i++)
                {
                    if (cellsAround[i] is null)
                        continue;

                    if (cellsAround[i].flag == FlagType.Flagged)
                        flaggedBombs++;
                }

                if (flaggedBombs != bombsAroundMe)
                    return;

                for (int i = 0; i < cellsAround.Length; i++)
                {
                    if (cellsAround[i] is null)
                        continue;

                    if (cellsAround[i].flag == FlagType.Flagged)
                        continue;
                    if (cellsAround[i].flag == FlagType.Revealed)
                        continue;

                    cellsAround[i].Reveal();
                }
            }

            public void Flag()
            {
                if (flag == FlagType.Unflagged || flag == FlagType.Question)
                    flag = FlagType.Flagged;
            }

            public void Question()
            {
                if (flag == FlagType.Unflagged || flag == FlagType.Flagged)
                    flag = FlagType.Question;
            }

            public Cell(Vector2D pos, bool isBomb, FlagType flag)
            {
                CellPos = pos;
                IsBomb = isBomb;
                this.flag = flag;
            }

            public override string ToString()
            {
                return $"|{CellPos} {IsBomb} {bombsAroundMe}|";
            }

            public string ToCellString(bool Colour)
            {
                return flag switch
                {
                    FlagType.Unflagged => "#",
                    FlagType.Flagged => Colour ? "P".Colourize(new("FF7700")) : "P",
                    FlagType.Question => Colour ? "?".Colourize(Color.Magenta) : "?",
                    _ => ToStringRevealed(Colour),
                };
            }

            private string ToStringRevealed(bool Colour)
            {
                if (IsBomb)
                    return Colour ? "!".Colourize(Color.Red) : "!";
                if (bombsAroundMe == 0)
                    return Colour ? "-".Colourize(ColourOfNumbers[0]) : "-";

                return Colour ? bombsAroundMe.ToString().Colourize(ColourOfNumbers[bombsAroundMe]) : bombsAroundMe.ToString();
            }

            public void SetBombsAroundMe()
            {
                bombsAroundMe = 0;
                Cell[] cellsAround = CellsAroundMe;
                for (int i = 0; i < cellsAround.Length; i++)
                {
                    if (cellsAround[i] is null)
                        continue;

                    if (cellsAround[i].IsBomb)
                        bombsAroundMe++;
                }
            }
        }

        public enum FlagType
        {
            Unflagged,
            Flagged,
            Question,
            Revealed,
        }
    }
}
