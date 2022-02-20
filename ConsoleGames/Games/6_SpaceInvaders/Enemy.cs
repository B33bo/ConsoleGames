using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games.SpaceInvadersGame
{
    public class Enemy : GameObject
    {
        Vector2D posRelativeEnemy;
        public static int DeadEnemies;
        public static int StartEnemies;

        public Enemy(Vector2D position)
        {
            posRelativeEnemy = position;

            Color = new Color("880088");
            Character = '@';
        }

        public override void KeyPress(ConsoleKey key)
        {
            
        }

        public override void OnCollision(GameObject collision, Vector2D displacement)
        {
            if (collision is Rocket)
                Game.Stop();
        }

        public override void Update()
        {
            Position = posRelativeEnemy + EnemyPoint.Instance.Position;

            if (Position.Y > Rocket.instance.Position.Y)
                Game.Stop();
        }
    }
}
