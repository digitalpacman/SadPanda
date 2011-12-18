using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SadPanda.GetThere
{
    public class BradsGame : Microsoft.Xna.Framework.Game
    {
        private GameBoard GameBoard { get; set; }
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public BradsGame()
        {
            GameBoard = new GameBoard();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Unit player = new Doctor(Content);
            player.Angle = Movement.West;
            GameBoard.Pieces.Add(player);

            Unit player2 = new Enforcer(Content);
            player2.Angle = Movement.East;
            player2.Position = new Vector2(400, 400);
            GameBoard.Pieces.Add(player2);

            base.LoadContent();
        }

        private Unit player;
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keybState = Keyboard.GetState();
            if (keybState.IsKeyDown(Keys.D2))
                player = (Unit)GameBoard.Pieces.Skip(1).First();
            if (player == null || keybState.IsKeyDown(Keys.D1))
                player = (Unit)GameBoard.Pieces.First();


            if (keybState.IsKeyDown(Keys.Left))
                player.Move(Movement.West);
            if (keybState.IsKeyDown(Keys.Right))
                player.Move(Movement.East);
            if (keybState.IsKeyDown(Keys.Up))
                player.Move(Movement.North);
            if (keybState.IsKeyDown(Keys.Down))
                player.Move(Movement.South);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);
            spriteBatch.Begin();
            foreach (GamePiece2D piece in GameBoard.Pieces)
            {
                piece.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
