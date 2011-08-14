using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SadPanda.Tools
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {

        public Matrix view { get; protected set; }
        public Matrix projection { get; protected set; }
        public float speed = 10;

        public Vector3 cameraPosition { get; protected set; }
        Vector3 cameraDirection;
        Vector3 cameraUp;

        MouseState prevMouseState;

        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up)
            : base(game)
        {
            cameraPosition = pos;
            cameraDirection = target - pos;
            cameraDirection.Normalize();
            cameraUp = up;
            CreateLookAt();

            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)Game.Window.ClientBounds.Width
                                                                                    / (float)Game.Window.ClientBounds.Height, 1, 10000000);

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            //Set mouse postion and do initial state
            Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2);
            prevMouseState = Mouse.GetState();

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //Move forward (w) and backward(s)
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cameraPosition += cameraDirection * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                cameraPosition -= cameraDirection * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cameraPosition += cameraDirection * (speed * 2);
            }
            //Move left(a) and right(d)
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * speed;
            }

            //Yaw rotation
            cameraDirection = Vector3.Transform(cameraDirection,
                                                Matrix.CreateFromAxisAngle(cameraUp,
                                                (-MathHelper.PiOver4 / 150) * (Mouse.GetState().X - prevMouseState.X)));

            //Roll
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                cameraUp = Vector3.Transform(cameraUp,
                                             Matrix.CreateFromAxisAngle(cameraDirection, MathHelper.PiOver4 / 45));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                cameraUp = Vector3.Transform(cameraUp,
                                             Matrix.CreateFromAxisAngle(cameraDirection, -MathHelper.PiOver4 / 45));
            }

            //Pitch
            cameraDirection = Vector3.Transform(cameraDirection,
                                                Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection),
                                                (MathHelper.PiOver4 / 100) * (Mouse.GetState().Y - prevMouseState.Y)));

            cameraUp = Vector3.Transform(cameraUp,
                                        Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection),
                                        (MathHelper.PiOver4 / 100) * (Mouse.GetState().Y - prevMouseState.Y)));

            prevMouseState = Mouse.GetState();

            CreateLookAt();

            base.Update(gameTime);
        }

        private void CreateLookAt()
        {
            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);

        }
    }
}
