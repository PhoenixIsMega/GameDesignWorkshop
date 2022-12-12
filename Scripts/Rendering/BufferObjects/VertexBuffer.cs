using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.rendering.buffers
{
    public class VertexBuffer : BufferInterface
    {
        public int bufferID { get; }

        public VertexBuffer(float[] verticies)
        {
            bufferID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, bufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, verticies.Length * sizeof(float), verticies, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, bufferID);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //could be put in abstract class
        }
    }
}
