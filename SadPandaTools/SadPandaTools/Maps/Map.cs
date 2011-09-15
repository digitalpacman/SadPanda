using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SadPanda.Tools.Maps
{
    public class Map
    {
        private Tile[][] grid;
        private Rectangle visibleArea = new Rectangle();

        public string Name { get; set; }

        public Map(string fileName, ContentManager content)
        {
            //load the map from a filename here. import xml etc.

            grid = new Tile[20][];
            for (int x = 0; x < grid.Length; x++)
            {
                grid[x] = new Tile[20];
                for (int y = 0; y < grid[x].Length; y++)
                {
                    grid[x][y] = new Tile()
                    {
                        Type = TileType.Normal,
                        Texture = content.Load<Texture2D>("tile")
                    };
                }
            }
        }

        public void DetermineVisibility()
        {
            //determine camera position

            //determine screen size

            //determine what section of the map will be visible
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //TODO: check if camera has moved from last position
            DetermineVisibility();

            //todo: get height based on a normalized setting
            int tileHeight = grid[0][0].Height;
            int tileWidth = grid[0][0].Width;

            //draw the map to the screen
            //TODO: make this based off of the visibile rectangle area instead of iterating thru the whole map grid
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    //TODO: convert map grid coordinates to pixel coordinates
                    int xPos = x * tileWidth;
                    int yPos = y * tileHeight;
                    Vector2 position = new Vector2(xPos, yPos);
                    grid[x][y].Draw(spriteBatch, position);
                }
            }
        }
    }
}
