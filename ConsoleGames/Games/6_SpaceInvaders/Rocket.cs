using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;

namespace ConsoleGames.Games.SpaceInvadersGame
{
    public class Rocket : GameObject
    {
        public static Rocket instance;
        public Rocket()
        {
            Character = 'U';
            Position = new(15, 13);
            instance = this;
        }

        public override void KeyPress(ConsoleKey key)
        {
            if (key == ConsoleKey.A)
                Move(new(-1, 0));
            else if (key == ConsoleKey.D)
                Move(new(1, 0));
            else if (key == ConsoleKey.Spacebar)
                Shoot();
        }

        private void Shoot()
        {
            Bullet bullet = new(Position - new Vector2D(0, 1));
            Game.AddObject(bullet);
        }

        public override void OnCollision(GameObject collision, Vector2D displacement)
        { }

        public override void Update()
        { }
    }
}
