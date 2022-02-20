using System;
using TextEngine;

using ConsoleGames.Games.SpaceInvadersGame;

namespace ConsoleGames.Games
{
    public static class SpaceInvaders
    {
        public static void Play()
        {
            Game.Screen = new(30, 15);

            Rocket rocket = new();
            Game.AddObject(rocket);

            EnemyPoint enemyPoint = new();
            //Spawn Enemies

            Game.AddObject(enemyPoint);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 4; j < 26; j += 2)
                {
                    Enemy enemy = new(new(j, i));
                    enemy.Position = new Vector2D(j + 1,i * 2);
                    Game.AddObject(enemy);
                    Enemy.StartEnemies++;
                }
            }

            Game.Start();
        }
    }
}
