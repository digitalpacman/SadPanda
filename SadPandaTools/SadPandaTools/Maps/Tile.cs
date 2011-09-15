using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SadPanda.Tools.Maps
{
    public class Tile
    {
        public TileType Type { get; set; }
        public Texture2D Texture { get; set; }
        public int Width
        {
            get
            {
                return this.Texture.Width;
            }
        }
        public int Height
        {
            get
            {
                return this.Texture.Height;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(this.Texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
