using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects
{
    public class IndexBuffer : IBuffer
    {
        public int BufferID { get; } // The ID of the index buffer

        public IndexBuffer(uint[] indices)
        {
            BufferID = GL.GenBuffer(); // Generate a buffer ID for the index buffer
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, BufferID); // Bind the index buffer

            // Fill the index buffer with data
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, BufferID); // Bind the index buffer
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0); // Unbind the index buffer by binding ID 0 (default)
        }
    }

}
