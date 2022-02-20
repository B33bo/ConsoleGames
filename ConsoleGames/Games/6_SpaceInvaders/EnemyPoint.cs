using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;

namespace ConsoleGames.Games.SpaceInvadersGame
{
    public class EnemyPoint : GameObject
    {
        int TimeBetweenMoves = 750;
        long Ms_SinceLastMove;
        bool goingRight = true;

        bool Note_Up;

        public static EnemyPoint Instance { get; private set; }

        public EnemyPoint()
        {
            HasCollision = false;
            Invisible = true;
            Character = '!';
            Instance = this;
        }

        public override void KeyPress(ConsoleKey key)
        {
            
        }

        public override void OnCollision(GameObject collision, Vector2D displacement)
        {
            
        }

        public override void Update()
        {
            if (Game.Timer.ElapsedMilliseconds - Ms_SinceLastMove < TimeBetweenMoves)
                return;

            Ms_SinceLastMove = Game.Timer.ElapsedMilliseconds;

            Vector2D movement = new(goingRight ? 1 : -1, 0);

            if (Position.X == 5)
            {
                goingRight = false;
                movement.Y = 1;
                movement.X = -1;
            }
            else if (Position.X == -3)
            {
                goingRight = true;
                movement.Y = 1;
                movement.X = 1;
            }

            TimeBetweenMoves = (Enemy.StartEnemies - Enemy.DeadEnemies) * 50;

            Sound.PlayNote(new(Note_Up ? 500 : 250, 100));
            Note_Up = !Note_Up;
            Move(movement);
        }
    }
}
