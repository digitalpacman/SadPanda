using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SadPanda.GetThere
{
    class UseObject
    {
        //Players image
        public Texture2D UseObjectTexture;

        //player position
        public Vector2 Position;

        //State of character: selected or not
        public bool Active;

        //players remaining health
        public int Health;

        //players size in width
        public int Width
        {
            get { return UseObjectTexture.Width; }
        }


        //players size in height
        public int Height
        {
            get { return UseObjectTexture.Height; }
        }
        

        public void Initialize(Texture2D texture, Vector2 position)
        {
            //set texture
            UseObjectTexture = texture;

            //starting position of character
            Position = position;

            //set state
            Active = false;

            //set health
            Health = 100;

        }

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            UseObjectTexture = theContentManager.Load<Texture2D>(theAssetName);
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UseObjectTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
