using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Components
{
    public class Texture : Component
    {
        private int textureID = 0;
        public int TextureID { get; set; }
        public Texture()
        {
            TextureID = 0;
        }
    }
}
