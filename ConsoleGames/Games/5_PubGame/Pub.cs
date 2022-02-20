using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games.PubGame
{
    public class Pub : GameObject
    {
        public Pub()
        {
            Position = new(Game.Screen.width - 1, 0);
            Character = 'O';
            Color = Color.Cyan;
        }

        public override void OnCollision(GameObject type, Vector2D Displacement)
        {
            if (type == Man.instance)
                Game.Stop();
        }
    }
}
