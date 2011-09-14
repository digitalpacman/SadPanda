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

        GraphicsDevice device;

        public BasicSprite(GraphicsDevice _device, Quad _quad, BasicEffect _effect, Texture2D _texture)
        {
            quad = _quad;
            effect = _effect;
            texture = _texture;
            device = _device;
        }

        public void Initialize()
        {
            //quad = new Quad(new Vector3(0, 50, 0), Vector3.Backward, Vector3.Up, 100, 100);
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

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.BlendState = BlendState.AlphaBlend;
                device.RasterizerState = RasterizerState.CullNone;
                device.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList,
                    quad.Vertices, 0, 4,
                    quad.Indexes, 0, 2);
                device.BlendState = BlendState.Opaque;
            }
        }

        public virtual Matrix GetWorld()
        {
            return world;
        }
    }
}
