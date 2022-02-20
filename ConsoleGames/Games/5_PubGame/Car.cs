using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games.PubGame
{
    public class Car : GameObject
    {
        float MSbetweenMovement = 200;
        float pastMS;
        int startX;

        public Car(int StartX, float speed)
        {
            Position = new(StartX, 0);
            startX = StartX;
            Character = '\\';
            MSbetweenMovement = speed;
            Color = Color.Red;
        }

        public override void OnCollision(GameObject type, Vector2D Displacement)
        {
            if (type == Man.instance)
                Game.Stop();
            else if (type.Character == 'I')
            {
                MSbetweenMovement += 200;
                type.Destroy();
            }
        }

        public override void Update()
        {
            if (Game.Timer.ElapsedMilliseconds - pastMS < MSbetweenMovement)
                return;
            pastMS = Game.Timer.ElapsedMilliseconds;

            Move(new(1, 1));
            if (Position.X >= Game.Screen.width || Position.Y >= Game.Screen.height)
                //Hit the corner
                Position = new(startX, 0);
        }
    }
}
