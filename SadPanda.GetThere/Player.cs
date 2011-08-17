using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SadPanda.GetThere
{
    class Player
    {
        //Players image
        public Texture2D PlayerTexture;

        //player position
        public Vector2 Position;

        //State of character: selected or not
        public bool Active;

        //players remaining health
        public int Health;

        //players size in width
        public int Width
        {
            get { return PlayerTexture.Width; }
        }


        //players size in height
        public int Height
        {
            get { return PlayerTexture.Height; }
        }
        

        public void Initialize(Texture2D texture, Vector2 position)
        {
            //set texture
            PlayerTexture = texture;

            //starting position of character
            Position = position;

            //set state
            Active = true;

            //set health
            Health = 100;

        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}