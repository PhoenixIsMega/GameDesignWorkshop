using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects
{
    public class VertexBuffer : IBuffer
    {
        public int BufferID { get; } // The ID of the vertex buffer

        public VertexBuffer(float[] vertices, BufferUsageHint usageHint)
        {
            BufferID = GL.GenBuffer(); // Generate a buffer ID for the vertex buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, BufferID); // Bind the vertex buffer

            // Fill the vertex buffer with data
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, usageHint);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, BufferID); // Bind the vertex buffer
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // Unbind the vertex buffer by binding ID 0 (default)
        }
    }

}
