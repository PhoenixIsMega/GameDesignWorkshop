using GameDesignWorkshop.rendering.buffers;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.rendering
{
    public class IndexBuffer : BufferInterface
    {
        public int bufferID { get; } //handle

        public IndexBuffer(uint[] indices)
        {
            bufferID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, bufferID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, bufferID);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0); //could be put in abstract class
        }
    }
}
