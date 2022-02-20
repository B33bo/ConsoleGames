using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using ConsoleGames.Games.CrossyRoadGame;

namespace ConsoleGames.Games
{
    public static class CrossyRoad
    {
        public static void Play()
        {
            Game.Screen = new(40, 20);
            Game.AddObject(new Player());
            Game.Start();

            int pos = Game.Screen.height;

            Terrain grass = new(TerrainType.Grass, pos);
            Game.AddObject(grass);
            pos -= grass.Scale.height;

            while (pos > 0)
            {
                Terrain terrain = new((TerrainType)RandomNG.Int(0, 2), pos);
                pos -= terrain.Scale.height;
                Game.AddObject(terrain);
            }
        }
    }
}
