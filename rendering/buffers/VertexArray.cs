using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDesignWorkshop.rendering.buffers
{
    public class VertexArray : BufferInterface
    {
        //make inherit from I Disposable
        public int bufferID { get; }

        public VertexArray()
        {
            bufferID = GL.GenVertexArray();
        }

        ~VertexArray()
        {
            GL.DeleteVertexArray(bufferID);
        }

        public void AddBuffer(VertexBuffer vertexBuffer, BufferLayout bufferLayout)
        {
            //rename
            Bind();
            vertexBuffer.Bind();
            var elements = bufferLayout.GetBufferElements();
            int offset = 0;
            for (int i = 0; i < elements.Count; i++)
            {
                var currentElement = elements[i];
                GL.EnableVertexAttribArray(i); //error on count()
                GL.VertexAttribPointer(i, currentElement.count, currentElement.type, currentElement.normalised, bufferLayout.GetStride(), offset);
                offset += currentElement.count * Utilities.GetSizeOfVertexAttribPointerType(currentElement.type);
            }
        }

        public void Bind()
        {
            GL.BindVertexArray(bufferID);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }
    }
}
