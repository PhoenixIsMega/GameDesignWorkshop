using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameDesignWorkshop.game.objects.tiles
{
    [Serializable]
    class Tile
    {
        // The position of the tile on the grid, in grid units
        public Vector2 Position { get; set; }

        // The sprite sheet that the tile should use to display its sprite
        //public Texture2D SpriteSheet { get; set; }

        // The index of the sprite on the sprite sheet that the tile should display
        public int SpriteIndex { get; set; }

        // The width and height of each tile, in pixels
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        // The spacing and margins on the sprite sheet, in pixels
        public int HorizontalSpacing { get; set; }
        public int VerticalSpacing { get; set; }
        public int MarginLeft { get; set; }
        public int MarginTop { get; set; }

        // Draw the tile on the screen
        public void Draw()
        {
            // Calculate the dimensions of the sprite sheet
            //int spriteSheetWidth = SpriteSheet.Width;
            //int spriteSheetHeight = SpriteSheet.Height;

            // Calculate the dimensions of each sprite on the sprite sheet
            int spriteWidth = TileWidth;
            int spriteHeight = TileHeight;

            // Calculate the coordinates of the sprite on the sprite sheet
            //Vector2 spriteCoords = GetSpriteCoords(spriteSheetWidth, spriteSheetHeight, spriteWidth, spriteHeight, HorizontalSpacing, VerticalSpacing, MarginLeft, MarginTop, SpriteIndex);

            // Convert the sprite coordinates to normalised UV coordinates
            //float u = spriteCoords.X / spriteSheetWidth;
            //float v = spriteCoords.Y / spriteSheetHeight;

            // Bind the sprite sheet texture
            //glBindTexture(GL_TEXTURE_2D, SpriteSheet.Id);

            // Set the UV coordinates of the vertices in the mesh
            //glTexCoord2f(u, v);

            // Draw the mesh using the texture
            //glDrawElements(GL_TRIANGLES, numIndices, GL_UNSIGNED_INT, indices);
        }

        // Calculate the coordinates of a sprite on a sprite sheet
        public Vector2 GetSpriteCoords(int spriteSheetWidth, int spriteSheetHeight, int spriteWidth, int spriteHeight, int horizontalSpacing, int verticalSpacing, int marginLeft, int marginTop, int spriteIndex)
        {
            // Calculate the number of columns and rows on the sprite sheet
            int numColumns = (spriteSheetWidth - marginLeft) / (spriteWidth + horizontalSpacing);
            int numRows = (spriteSheetHeight - marginTop) / (spriteHeight + verticalSpacing);

            // Calculate
            return new Vector2();
        }
    }
}
