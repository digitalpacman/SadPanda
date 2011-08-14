using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SadPanda.Tools.Models
{
    class BasicModel
    {
        public Model model { get; protected set; }
        protected Matrix world = Matrix.Identity;
        Vector3 EmissiveColor = new Vector3(0.6f, 0.3f, 1.0f);
        public BasicModel(Model m)
        {
            model = m;
        }

        public virtual void Update()
        {

        }

        public void Draw(Camera camera)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];

            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect be in mesh.Effects)
                {

                    be.EnableDefaultLighting();
                    be.EmissiveColor = EmissiveColor;
                    be.SpecularColor = EmissiveColor;
                    be.SpecularPower = 100.0f;

                    be.LightingEnabled = true;
                    be.DirectionalLight0.Enabled = true;
                    be.DirectionalLight0.DiffuseColor = new Vector3(0.0f, 0.4f, 0.0f);
                    be.DirectionalLight0.SpecularColor = new Vector3(0.2f, 0.0f, 0.0f);
                    be.DirectionalLight0.Direction = new Vector3(1.0f, 1.0f, 1.0f);
                    be.PreferPerPixelLighting = true;
                    be.AmbientLightColor = EmissiveColor;


                    //be.VertexColorEnabled = true;
                    be.Projection = camera.projection;
                    be.View = camera.view;
                    be.World = GetWorld() * mesh.ParentBone.Transform;
                }

                mesh.Draw();
            }
        }

        public virtual Matrix GetWorld()
        {
            return world;
        }

    }
}
