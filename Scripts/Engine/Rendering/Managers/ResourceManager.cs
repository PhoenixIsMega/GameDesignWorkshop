using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes.Factories;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers
{
    public sealed class ResourceManager
    {
        private static ResourceManager instance = null; // Singleton instance
        private static readonly object loc = new object(); // Lock object for thread safety
        private IDictionary<string, Texture> textureCache = new Dictionary<string, Texture>(); // Cache to store loaded textures

        public static ResourceManager Instance
        {
            get
            {
                lock (loc) // Lock the access to the instance creation for thread safety
                {
                    if (instance is null) // If the instance is not yet created
                    {
                        instance = new ResourceManager(); // Create a new instance of ResourceManager
                    }
                }
                return instance; // Return the instance
            }
        }

        public Texture LoadTexture(string textureName, TextureUnit textureUnit)
        {
            textureCache.TryGetValue(textureName, out var value); // Try to get the texture from the cache
            if (value != null) // If the texture is found in the cache
            {
                value.Use(textureUnit); // Use the cached texture
                return value; // Return the cached texture
            }
            if (value == null)
            {
                value = TextureFactory.Load(textureName); // Load the texture using TextureFactory
                textureCache.Add(textureName, value); // Add the loaded texture to the cache
            }
            return value; // Return the loaded texture
        }

        public Texture LoadTexture(string textureName)
        {
            return LoadTexture(textureName, TextureUnit.Texture0);
        }

        public void DisposeTextures()
        {
            TextureFactory.UnloadTextures();
            foreach (Texture texture in textureCache.Values)
            {
                texture.Dispose();
            }
            textureCache.Clear();
        }
    }

}
