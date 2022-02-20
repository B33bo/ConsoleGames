using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games.CrossyRoadGame
{
    public class Terrain : GameObject
    {
        private static Texture roadTexture = new(new string[] { "--   " });
        public static Terrain LastPlacedTerrain { get; private set; }
        private GameObject[] children;

        public Terrain(TerrainType type, int position)
        {
            LastPlacedTerrain = this;

            if (type == TerrainType.Grass)
                Scale = new(Game.Screen.width, RandomNG.Int(1, 10));
            else
                Scale = new(Game.Screen.width, RandomNG.Int(1, 4));

            Vector2D spawnPos = new(0, position);
            spawnPos.Y -= Scale.height;

            Position = spawnPos;

            switch (type)
            {
                case TerrainType.Grass:
                    Color = new Color("00AA00");
                    Highlight = new Color("00AA00");
                    SpawnTrees();
                    break;
                case TerrainType.Road:
                    texture = roadTexture;
                    Color = Color.Yellow;
                    Highlight = Color.Black;
                    SpawnCars();
                    break;
                default:
                    Color = Color.Red;
                    Highlight = Color.Black;
                    Character = '!';
                    break;
            }
        }

        private void SpawnCars()
        {
            children = new GameObject[Scale.height * 4];

            int currentChild = 0;
            for (int i = 0; i < Scale.height; i++)
            {
                int CarSpawnStartPos = RandomNG.Int(0, Game.Screen.width);
                int Ypos = Position.Y + i;

                bool GoingRight = RandomNG.Bool();

                int CarUpdateDelay = RandomNG.Int(100, 150);

                for (int j = 0; j < 4; j++)
                {
                    Vector2D pos = new(CarSpawnStartPos, Ypos);
                    Car newCar = new(GoingRight, pos, CarUpdateDelay);

                    CarSpawnStartPos += RandomNG.Int(10, 15);
                    CarSpawnStartPos %= Game.Screen.width;
                    children[currentChild] = newCar;
                    Game.AddObject(newCar);

                    currentChild++;
                }
            }
        }

        private void SpawnTrees()
        {
            children = new GameObject[Scale.height * 4];

            int currentChild = 0;
            for (int i = 0; i < Scale.height; i++)
            {
                int Ypos = Position.Y + i;

                for (int j = 0; j < 4; j++)
                {
                    GameObject newTree = new();

                    newTree.Position = new(RandomNG.Int(0, Scale.width), Ypos);
                    newTree.Highlight = Highlight;
                    newTree.Character = '^';
                    newTree.Color = Color.Green;
                    newTree.HasCollision = true;
                    newTree.RenderOrder = 2;

                    children[currentChild] = newTree;
                    Game.AddObject(newTree);

                    currentChild++;
                }
            }
        }


        public override void Update()
        {
            if (Position.Y > Camera.Instance.Position.Y + Game.Screen.height)
            {
                if (children is not null)
                {
                    for (int i = 0; i < children.Length; i++)
                        children[i].Destroy();
                }

                Destroy();
            }
        }

        public override void KeyPress(ConsoleKey key)
        {
            if (key == ConsoleKey.H)
                if (RandomNG.Bool())
                    Color = Color.Red;
        }
    }

    public enum TerrainType : int
    {
        Grass,
        Road,
    }
}
