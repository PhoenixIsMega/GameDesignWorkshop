using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers
{
    public struct SpritesheetProperties
    {
        public int leftMargin;           // The left margin in pixels
        public int bottomMargin;         // The bottom margin in pixels
        public int rightMargin;          // The right margin in pixels
        public int topMargin;            // The top margin in pixels
        public int verticalSpacing;      // The vertical spacing between tiles in pixels
        public int horizontalSpacing;    // The horizontal spacing between tiles in pixels
        public int tileSizeX;            // The width of each tile in pixels
        public int tileSizeY;            // The height of each tile in pixels
        public int imageSizeX;           // The total width of the spritesheet image in pixels
        public int imageSizeY;           // The total height of the spritesheet image in pixels
    }

    public class SpritesheetManager
    {
        public float[] GetSpriteSheetUV(SpritesheetProperties spritesheetProperties, int indexX, int indexY)
        {
            // Calculate the position of the sprite based on the provided values
            int spriteY = spritesheetProperties.bottomMargin + ((spritesheetProperties.tileSizeY + spritesheetProperties.verticalSpacing) * indexY);
            int spriteX = spritesheetProperties.leftMargin + ((spritesheetProperties.tileSizeX + spritesheetProperties.horizontalSpacing) * indexX);

            Console.WriteLine(spriteX + " " + spriteY);

            // Calculate the UV coordinates of the sprite
            float[] uvCoords = new float[2];
            uvCoords[0] = (float)spriteX / (float)spritesheetProperties.imageSizeX;
            uvCoords[1] = (float)spriteY / (float)spritesheetProperties.imageSizeY;

            return uvCoords;
        }
    }

}
