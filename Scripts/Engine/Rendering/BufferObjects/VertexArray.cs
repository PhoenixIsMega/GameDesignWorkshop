using GameDesignLearningAppPrototype.Scripts.Engine.Utility;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects
{
    public class VertexArray : IBuffer
    {
        public int BufferID { get; } // The ID of the vertex array

        public VertexArray()
        {
            BufferID = GL.GenVertexArray(); // Generate a vertex array ID for the vertex array
        }

        ~VertexArray()
        {
            GL.DeleteVertexArray(BufferID); // Delete the vertex array when the object is garbage collected
        }

        public void AddBuffer(VertexBuffer vertexBuffer, BufferLayout bufferLayout)
        {
            Bind(); // Bind the vertex array
            vertexBuffer.Bind(); // Bind the vertex buffer
            var elements = bufferLayout.GetBufferElements(); // Get the buffer elements from the buffer layout
            int offset = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                var currentElement = elements[i];
                GL.EnableVertexAttribArray(i); // Enable the vertex attribute at the current index
                GL.VertexAttribPointer(i, currentElement.count, currentElement.type, currentElement.normalized, bufferLayout.GetStride(), offset);
                // Specify the vertex attribute pointer settings
                // Parameters: attribute index, number of values per vertex attribute, data type, normalization, stride, offset
                offset += currentElement.count * MathUtilities.GetSizeOfVertexAttribPointerType(currentElement.type);
                // Calculate the offset for the next vertex attribute based on the current element's properties
            }
        }

        public void Bind()
        {
            GL.BindVertexArray(BufferID); // Bind the vertex array
        }

        public void Unbind()
        {
            GL.BindVertexArray(0); // Unbind the vertex array by binding ID 0 (default)
        }
    }

}
