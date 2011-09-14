using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SadPanda.Tools.Sprites;
using SadPanda.Tools;

namespace SadPanda.GetThere
{
    class Player
    {
        GraphicsDeviceManager graphics;

        //Players image
        public Texture2D PlayerTexture;
        public BasicSprite sprite;
        BasicEffect spriteEffect;

        //player position
        public Quad Position;

        //State of character: selected or not
        public bool Active;

        //players remaining health
        public int Health;

        //players size in width
        public int Width
        {
            get { return PlayerTexture.Width; }
        }

        public Player(GraphicsDeviceManager _graphics )
        { 
            graphics = _graphics;
            spriteEffect = new BasicEffect(graphics.GraphicsDevice);

            Position = new Quad(new Vector3(0, 50, 0), Vector3.Backward, Vector3.Up, 100, 100);
        
        }

        //players size in height
        public int Height
        {
            get { return PlayerTexture.Height; }
        }
        

        public void Initialize(Texture2D texture)
        {
            //set texture
            PlayerTexture = texture;

            //set state
            Active = true;

            //set health
            Health = 100;

            sprite = new BasicSprite(graphics.GraphicsDevice, Position, spriteEffect, PlayerTexture);

        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}