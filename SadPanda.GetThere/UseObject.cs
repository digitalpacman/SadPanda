using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace SadPanda.GetThere
{
    public class UseObject
    {
        private ContentManager content;
        private Timer stateTimer;
        private bool isLocked = true;

        public UseObject(ContentManager contentManager)
        {
            content = contentManager;
        }

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

        public bool TurnOn()
        {
            if (!isLocked)
                return false;

            LockState();


            UseObjectTexture = content.Load<Texture2D>("useComputerOn");
            Active = true;

            BeginReleaseState();

            return true;
        }

        private void LockState()
        {
            isLocked = false;
        }

        private void BeginReleaseState()
        {
            stateTimer = new Timer(ReleaseState, null, 1000, 1000);
        }

        private void ReleaseState(Object stateInfo)
        {
            stateTimer.Dispose();
            isLocked = true;
        }

        public bool TurnOff()
        {
            if (!isLocked)
                return false;

            LockState();


            UseObjectTexture = content.Load<Texture2D>("useComputeroff");
            Active = false;

            BeginReleaseState();

            return true;
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

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UseObjectTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
