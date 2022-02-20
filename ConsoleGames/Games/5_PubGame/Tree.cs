using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games.PubGame
{
    class Tree : GameObject
    {
        public Tree()
        {
            Character = 'I';
            Position = RandomNG.Vector(Vector2D.Zero, Camera.BottomRight);
            HasCollision = true;
            Color = Color.Green;
        }
    }
}
