using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;
using TextEngine.Colors;

namespace ConsoleGames.Games.CrossyRoadGame
{
    public class Car : GameObject
    {
        private bool GoingRight;
        private long TimeBetweenMoves;
        private long TimeSinceLastMove;

        public Car(bool GoingRight, Vector2D position, long timeBetweenMoves)
        {
            RenderOrder = 2;
            TimeBetweenMoves = timeBetweenMoves;
            this.GoingRight = GoingRight;
            this.Position = position;

            texture = GoingRight ? new("o=o>") : new("<o=o");
            Scale = new(4, 1);

            Color = new Color("FF0000");
            Highlight = Color.Black;
        }

        public override void Update()
        {
            if (Game.Timer.ElapsedMilliseconds - TimeSinceLastMove < TimeBetweenMoves)
                return;
            TimeSinceLastMove = Game.Timer.ElapsedMilliseconds;

            if (GoingRight)
            {
                if (Position.X >= Game.Screen.width)
                    Position.X = -5;

                Move(Vector2D.Right);
            }
            else
            {
                if (Position.X <= -5)
                    Position.X = Game.Screen.width;

                Move(Vector2D.Left);
            }
        }

        public override void OnCollision(GameObject collision, Vector2D displacement)
        {
            if (collision is Player)
                Game.Stop();
        }
    }
}
