using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Components
{
    public class TextureComponent : ComponentBase
    {
        private int textureID = 0;
        private int tileX = 0;
        private int tileY = 0;

        public TextureComponent() : base()
        {
            TextureID = 0;
        }

        public TextureComponent(int textureID) : this()
        {
            TextureID = textureID;
        }


        public int TextureID
        {
            get { return textureID; }
            set {
                TileX = value % 20 + 1;
                TileY = value / 20 + 1;

                textureID = value;
            }
        }

        public int TileX { get; private set; }
        public int TileY { get; private set; }


    }
}
