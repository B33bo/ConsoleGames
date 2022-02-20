using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games.PubGame
{
    class Man : GameObject
    {
        public static Man instance;
        public Man()
        {
            Position = new(0, 0);
            Character = '#';
            HasCollision = true;
            instance = this;
            Color = Color.Cyan;
        }

        public override void KeyPress(ConsoleKey key)
        {
            switch (key)
            {
                default:
                    break;
                case ConsoleKey.W:
                    Move(new(0, -1));
                    break;
                case ConsoleKey.S:
                    Move(new(0, 1));
                    break;
                case ConsoleKey.A:
                    Move(new(-1, 0));
                    break;
                case ConsoleKey.D:
                    Move(new(1, 0));
                    break;
            }
        }

        public override void OnCollision(GameObject type, Vector2D Displacement)
        {
            if (type.Character == 'I')
                Pub_Game.score -= 2;
        }
    }
}
