using GameDesignWorkshop.management.Textures;
using GameDesignWorkshop.rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.management
{
    public sealed class ResourceManager
    {
        private static ResourceManager instance = null;
        private static readonly object loc = new object();
        private IDictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

        public static ResourceManager Instance
        {
            get
            {
                lock(loc)
                {
                    if (instance == null)
                    {
                        instance = new ResourceManager();
                    }
                }
                return instance;
            }
        }

        public Texture2D LoadTexture(string textureName)
        {
            textureCache.TryGetValue(textureName, out var value);
            if(value != null)
            {
                return value;
            }
            value = TextureFactory.Load(textureName);
            textureCache.Add(textureName, value);
            return value;
        }
    }
}
