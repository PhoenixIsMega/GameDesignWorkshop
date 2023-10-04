using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects
{
    public struct BufferElement
    {
        public VertexAttribPointerType type;
        public int count;
        public bool normalized;
    }

    public class BufferLayout
    {
        private List<BufferElement> elements = new List<BufferElement>(); // List to store the buffer elements
        private int stride; // The stride of the buffer layout

        public BufferLayout()
        {
            stride = 0; // Initialize the stride to 0
        }

        public List<BufferElement> GetBufferElements() => elements; // Returns the list of buffer elements
        public int GetStride() => stride; // Returns the stride

        public void Add<T>(int count, bool normalized = false) where T : struct
        {
            VertexAttribPointerType type;

            // Determine the data type based on the generic type parameter T
            if (typeof(float) == typeof(T))
            {
                type = VertexAttribPointerType.Float;
                stride += sizeof(float) * count; // Increment the stride by the size of the float multiplied by the count
            }
            else if (typeof(uint) == typeof(T))
            {
                type = VertexAttribPointerType.UnsignedByte;
                stride += sizeof(uint) * count; // Increment the stride by the size of the uint multiplied by the count
            }
            else if (typeof(byte) == typeof(T))
            {
                type = VertexAttribPointerType.UnsignedByte;
                stride += sizeof(byte) * count; // Increment the stride by the size of the byte multiplied by the count
            }
            else
            {
                throw new ArgumentException($"{typeof(T)} is not a valid type"); // Throw an exception for unsupported types
            }

            // Create a new BufferElement and add it to the list of elements
            elements.Add(new BufferElement { type = type, count = count, normalized = normalized });
        }
    }

}
