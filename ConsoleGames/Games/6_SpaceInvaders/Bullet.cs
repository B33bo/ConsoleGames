using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;

namespace ConsoleGames.Games.SpaceInvadersGame
{
    public class Bullet : GameObject
    {
        long MS_sinceMove;
        int TimeBetweenMove = 200;

        public Bullet(Vector2D pos)
        {
            Character = '.';
            Position = pos;
        }

        public override void KeyPress(ConsoleKey key)
        {
            
        }

        public override void OnCollision(GameObject collision, Vector2D displacement)
        {
            if (collision is Enemy)
                Enemy.DeadEnemies++;

            if (Enemy.StartEnemies - Enemy.DeadEnemies < 1)
                Game.Stop();

            collision.Destroy();
            Destroy();
        }

        public override void Update()
        {
            if (Game.Timer.ElapsedMilliseconds - MS_sinceMove < TimeBetweenMove)
                return;

            MS_sinceMove = Game.Timer.ElapsedMilliseconds;
            Move(new(0, -1));
        }
    }
}
