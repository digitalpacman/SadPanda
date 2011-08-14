using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SadPanda.Tools.Sprites
{
    public class BasicSprite
    {
        private Quad quad;
        public Quad Quad{get { return quad; } set { quad = value; }}

        private BasicEffect effect;
        public BasicEffect Effect { get { return effect; } set { effect = value; } }

        private Texture2D texture;
        public Texture2D Texture { get { return texture; } set { texture = value;} }

        protected Matrix world = Matrix.Identity;


        public BasicSprite(Quad _quad, BasicEffect _effect, Texture2D _texture)
        {
            quad = _quad;
            effect = _effect;
            texture = _texture;
        }

        public void Draw(Camera camera)
        {
            effect.EnableDefaultLighting();
            effect.World = Matrix.Identity;
            effect.View = camera.view;
            effect.Projection = camera.projection;
            effect.TextureEnabled = true;
            effect.Texture = texture;
            effect.Alpha = 1;
        }

        public virtual Matrix GetWorld()
        {
            return world;
        }
    }
}
