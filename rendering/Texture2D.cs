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
        public Texture2D(int handle)
        {
            this.Handle = handle;
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
