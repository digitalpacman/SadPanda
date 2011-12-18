using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SadPanda.GetThere
{
    public class GameBoard
    {
        public GameBoard()
        {
            Pieces = new List<GamePiece2D>();
        }

        public IList<GamePiece2D> Pieces { get; set; }

        public bool TryMovement(GamePiece2D pieceToMove, float angle, float distance)
        {
            Vector2 up = new Vector2(0, -1);
            Matrix rotationMatrix = Matrix.CreateRotationZ(angle);
            Vector2 direction = Vector2.Transform(up, rotationMatrix);

            direction *= distance;

            Matrix collisionMatrix = pieceToMove.CalculateCollisionMatrix(direction, angle);

            Matrix collidingMatrix;
            Vector2 collisionPoint;
            foreach (GamePiece2D piece in Pieces)
            {
                if (piece == pieceToMove)
                    continue;

                collidingMatrix = piece.CalculateCollisionMatrix();
                collisionPoint = TexturesCollide(piece.TextureColors, collidingMatrix, pieceToMove.TextureColors, collisionMatrix);

                if (collisionPoint.X > -1)
                {
                    return true;
                }

                foreach (GamePiece2D childPiece in piece.Pieces.Values)
                {
                    collidingMatrix = childPiece.CalculateCollisionMatrix();
                    collisionPoint = TexturesCollide(childPiece.TextureColors, collidingMatrix, pieceToMove.TextureColors, collisionMatrix);

                    if (collisionPoint.X > -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static Vector2 TexturesCollide(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2)
        {
            Matrix mat1to2 = mat1 * Matrix.Invert(mat2);
            int width1 = tex1.GetLength(0);
            int height1 = tex1.GetLength(1);
            int width2 = tex2.GetLength(0);
            int height2 = tex2.GetLength(1);

            for (int x1 = 0; x1 < width1; x1++)
            {
                for (int y1 = 0; y1 < height1; y1++)
                {
                    Vector2 pos1 = new Vector2(x1, y1);
                    Vector2 pos2 = Vector2.Transform(pos1, mat1to2);

                    int x2 = (int)pos2.X;
                    int y2 = (int)pos2.Y;
                    if ((x2 >= 0) && (x2 < width2))
                    {
                        if ((y2 >= 0) && (y2 < height2))
                        {
                            if (tex1[x1, y1].A > 0)
                            {
                                if (tex2[x2, y2].A > 0)
                                {
                                    Vector2 screenPos = Vector2.Transform(pos1, mat1);
                                    return screenPos;
                                }
                            }
                        }
                    }
                }
            }

            return new Vector2(-1, -1);
        }
    }

    public abstract class Unit : GamePiece2D
    {
        protected virtual float Speed
        {
            get
            {
                return 3f;
            }
        }
        public void Die()
        {
        }

        public virtual void Move()
        {
            Vector2 movement = new Vector2(Speed * (float)Math.Cos(Angle), -(Speed * (float)Math.Sin(Angle)));
            Position += movement;
            distanceTraveled += (int)Speed;
        }

        public virtual void Move(float angle)
        {
            Face(angle);
            Move();
        }

        public virtual void Turn(float angle)
        {
            Angle += angle;
        }

        public virtual void Face(float angle)
        {
            Angle = angle;
        }

        private int currentFrame = 0;
        private int distanceTraveled = 0;
        private const int distancePerFrame = 40;
        private const int framesPerRow = 4;
        public override void Draw(SpriteBatch spriteBatch)
        {
            int row = 0;
            if (Angle == Movement.South)
                row = 0;
            else if (Angle == Movement.West)
                row = 1;
            else if (Angle == Movement.East)
                row = 2;
            else if (Angle == Movement.North)
                row = 3;

            int frameWidth = Texture.Width / framesPerRow;
            int frameHeight = Texture.Height / framesPerRow;

            int startY = row * frameHeight;
            int startX = currentFrame * frameWidth;
            spriteBatch.Draw(Texture, Position, new Rectangle(startX, startY, frameWidth, frameHeight), Color.Wheat, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);

            if (distanceTraveled > distancePerFrame)
            {
                currentFrame++;
                currentFrame = currentFrame % framesPerRow;

                distanceTraveled -= distancePerFrame;
            }
        }
    }

    public class Doctor : Unit
    {
        public Doctor(ContentManager content)
        {
            SetTexture(content.Load<Texture2D>("sharkman"));
        }
    }

    public class Enforcer : Unit
    {
        public Enforcer(ContentManager content)
        {
            SetTexture(content.Load<Texture2D>("maskedman"));
        }
    }

    public interface IUnit
    {
        void Die();
        void Move();
        void Move(float angle);
        void Turn(float angle);
        void Face(float angle);
    }

    public class Movement
    {
        public static readonly float North = (float)Math.PI / 2;
        public static readonly float East = 0f;
        public static readonly float South = (float)(3 * Math.PI / 2);
        public static readonly float West = (float)Math.PI;
    }

    public abstract class GamePiece2D
    {
        #region Auto-implementors
        public Vector2 Position { get; set; }
        public Dictionary<string, GamePiece2D> Pieces { get; set; }
        public Texture2D Texture { get; private set; }
        public float Angle { get; set; }
        public float Scale { get; set; }
        public SpriteEffects Facing { get; set; }
        #endregion

        #region Backing properties
        private Color[,] textureColors;
        public Color[,] TextureColors
        {
            get
            {
                if (textureColors == null)
                    textureColors = TextureTo2DArray(Texture);
                return textureColors;
            }
        }
        #endregion

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
        }

        public int Height
        {
            get
            {
                return Texture.Height;
            }
        }

        public int Width
        {
            get
            {
                return Texture.Width;
            }
        }

        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
            textureColors = null;
        }

        public Matrix CalculateCollisionMatrix()
        {
            return Matrix.CreateTranslation(-Width, -Height, 0)
                * Matrix.CreateRotationZ(Angle)
                * Matrix.CreateScale(Scale)
                * Matrix.CreateTranslation(Position.X, Position.Y, 0);
        }

        public Matrix CalculateCollisionMatrix(Vector2 position, float angle)
        {
            return Matrix.CreateTranslation(-Width, -Height, 0)
                * Matrix.CreateRotationZ(angle)
                * Matrix.CreateScale(Scale)
                * Matrix.CreateTranslation(position.X, position.Y, 0);
        }

        #region Statics
        private static Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);

            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }
        #endregion
    }
}
