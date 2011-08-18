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

namespace SadPanda.GetThere
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //Represents the player
        Player player;

        //represents the useable object
        UseObject testUseObject; 

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        float playerMoveSpeed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //Initialize player class
            player = new Player();

            //Set Speed of player
            playerMoveSpeed = 8.0f;

            //initialize a useable object
            testUseObject = new UseObject(this.Content);

            base.Initialize();
           
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //Load Player resources
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(Content.Load<Texture2D>("player"), playerPosition);

            //load useable object resources
            testUseObject.TurnOff();
            testUseObject.Position = new Vector2(125, 245);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {   
            // Save the previous state of the keyboard and game pad so we can determinesingle key/button presses
            //previousKeyboardState = currentKeyboardState;

            // Read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState(); 

            //Update the player
            UpdatePlayer(gameTime);

            // Update the useable object
            UpdateUseObject();

            previousKeyboardState = currentKeyboardState;

            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            //basic movement
            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                player.Position.X -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                player.Position.X += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.W))
            {
                player.Position.Y -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                player.Position.Y += playerMoveSpeed;
            }


            // Allows the game to exit
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // Make sure that the player does not go out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);

        }

        private void UpdateUseObject()
        {
            // Use the Rectangle's built-in intersect function to 
            // determine if two objects are overlapping
            Rectangle rectangle1;
            Rectangle rectangle2;

            // Only create the rectangle once for the player
            rectangle1 = new Rectangle((int)player.Position.X,
            (int)player.Position.Y,
            player.Width,
            player.Height);

            // Do the collision between the player and the enemies
            rectangle2 = new Rectangle((int)testUseObject.Position.X,
            (int)testUseObject.Position.Y,
            testUseObject.Width,
            testUseObject.Height);

            // Determine if the two objects collided with each
            // other
            if (rectangle1.Intersects(rectangle2))
            {
                player.Initialize(Content.Load<Texture2D>("playerQuestion"), player.Position);
                //use ability
                if (currentKeyboardState.IsKeyDown(Keys.E) && !previousKeyboardState.IsKeyDown(Keys.E))
                {
                    if (testUseObject.Active)
                        testUseObject.TurnOff();
                    else
                        testUseObject.TurnOn();
                }
            }
            else
            {
                player.Initialize(Content.Load<Texture2D>("player"), player.Position);
            }

         }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            //Start Drawing
            spriteBatch.Begin();

            //Draw the player
            player.Draw(spriteBatch);

            testUseObject.Draw(spriteBatch);

            //Stop drawing
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

