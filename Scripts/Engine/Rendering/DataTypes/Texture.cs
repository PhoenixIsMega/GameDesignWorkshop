using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes
{
    public class Texture : IDisposable
    {
        private bool disposed; // Flag to track if the object has been disposed

        public int Handle { get; private set; } // Handle (ID) of the texture
        public int Width { get; set; } // Width of the texture
        public int Height { get; set; } // Height of the texture
        public TextureUnit TextureUnit { get; set; } = TextureUnit.Texture0; // Texture unit for binding the texture

        public Texture(int handle)
        {
            this.Handle = handle; // Initialize the texture with the given handle
        }

        public Texture(int handle, int width, int height) : this(handle)
        {
            Width = width; // Initialize the texture with the given handle, width, and height
            Height = height;
        }

        public Texture(int handle, int width, int height, TextureUnit textureSlot) : this(handle, width, height)
        {
            TextureUnit = textureSlot; // Initialize the texture with the given handle, width, height, and texture slot
        }

        ~Texture()
        {
            Dispose(false); // Destructor that calls Dispose method with disposing set to false
        }

        public void Use()
        {
            GL.ActiveTexture(TextureUnit.Texture0); // Activate the specified texture unit
            GL.BindTexture(TextureTarget.Texture2D, Handle); // Bind the texture to the active texture unit
        }

        public void Dispose(bool disposing)
        {
            if (!disposed) // Check if the object has already been disposed
            {
                GL.DeleteTexture(Handle); // Delete the texture from OpenGL
                disposed = true; // Set the disposed flag to true
            }
        }

        public void Dispose()
        {
            Dispose(true); // Public Dispose method that calls Dispose method with disposing set to true
            GC.SuppressFinalize(this); // Request the garbage collector not to call the finalizer
        }
    }

}
