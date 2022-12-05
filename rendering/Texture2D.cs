using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.rendering
{
    public class Texture2D : IDisposable
    {
        private bool disposed;
        public int Handle { get; private set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public TextureUnit TextureUnit { get; set; } = TextureUnit.Texture0;
        public Texture2D(int handle)
        {
            this.Handle = handle;
        }

        public Texture2D(int handle, int width, int height) : this(handle)
        {
            Width = width;
            Height = height;
        }

        public Texture2D(int handle, int width, int height, TextureUnit textureSlot) : this(handle, width, height)
        {
            TextureUnit = textureSlot; //change names to slot
        }

        ~Texture2D() {
            Dispose(false);
        }

        public void Use()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                GL.DeleteTexture(Handle);
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
