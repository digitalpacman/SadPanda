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


namespace SadPanda.Tools.Models
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ModelManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<BasicModel> models = new List<BasicModel>();
        Camera camera;


        public ModelManager(Game game, Camera Camera)
            : base(game)
        {
            camera = Camera;
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        protected override void LoadContent()
        {


            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            foreach (BasicModel bm in models)
            {
                bm.Draw(camera);

            }

            base.Draw(gameTime);
        }

        public void LoadModel(string modelPath)
        {
            models.Add(new BasicModel(Game.Content.Load<Model>
                (modelPath)));
        }


        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < models.Count; ++i)
            {
                models[i].Update();
            }
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}
