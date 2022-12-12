using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.game.managers
{
    public struct SpritesheetProperties
    {
        public int leftMargin; //in PIXELS
        public int bottomMargin;
        public int rightMargin;
        public int topMargin;
        public int verticalSpacing;
        public int horizontalSpacing;
        public int tileSizeX;
        public int tileSizeY;
        public int imageSizeX;
        public int imageSizeY;
    }
    public class SpritesheetManager
    {
        public float[] getSpriteSheetUV(SpritesheetProperties spritesheetProperties, int indexX, int indexY)
        {
            int spriteY = spritesheetProperties.bottomMargin + ((spritesheetProperties.tileSizeY + spritesheetProperties.verticalSpacing) * indexY);
            int spriteX = spritesheetProperties.leftMargin + ((spritesheetProperties.tileSizeX + spritesheetProperties.horizontalSpacing) * indexX);
            float[] uvCoords = new float[2];
            uvCoords[0] = (float)spriteX / (float)spritesheetProperties.imageSizeX;
            uvCoords[1] = (float)spriteY / (float)spritesheetProperties.imageSizeY;
            return uvCoords;
        }
    }
}
