using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine;

namespace ConsoleGames.Games.CrossyRoadGame
{
    public class Player : GameObject
    {
        public Player Instance { get; private set; }
        public static int Score;

        public Player()
        {
            Instance = this;
            Position = new(Vector2D.GameMiddleCenter.X, Game.Screen.height - 1);
            Character = 'O';
            RenderOrder = 1;
            HasCollision = true;
        }

        public override void KeyPress(ConsoleKey key)
        {
            Vector2D movement;
            movement = key switch
            {
                ConsoleKey.W or ConsoleKey.Spacebar => Vector2D.Up,
                ConsoleKey.A => Vector2D.Left,
                ConsoleKey.S => Vector2D.Down,
                ConsoleKey.D => Vector2D.Right,
                _ => Vector2D.Zero,
            };

            if (movement == Vector2D.Zero)
                return;

            if (Position.Y >= Game.Screen.height - 1 && movement == Vector2D.Down)
                return;

            if (Position.X + movement.X < 0)
                return;

            if (Position.X + movement.X > Game.Screen.width - 1)
                return;

            Sound.PlayNote(new(RandomNG.Int(500, 1000), 200));

            Move(movement);

            int cameraLagBehindPosition = Position.Y - Camera.Instance.Position.Y;

            if (movement == Vector2D.Up && Game.Screen.height - 5 > cameraLagBehindPosition)
                Camera.Instance.Position += movement;

            int topRenderPos = Terrain.LastPlacedTerrain.Position.Y;

            if (topRenderPos > Camera.Instance.Position.Y)
                Game.AddObject(new Terrain((TerrainType)RandomNG.Int(0, 2), topRenderPos));

            Score = -Camera.Instance.Position.Y;
            Game.ToolBar = Score.ToString();
        }

        public override void OnCollision(GameObject collision, Vector2D displacement)
        {
            if (collision is Terrain)
                Highlight = collision.Highlight;
        }
    }
}
