using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.rendering.buffers
{
    public class BufferLayout
    {
        private List<BufferElement> elements = new List<BufferElement>();
        private int stride;
        public BufferLayout()
        {
            stride = 0;
        }

        public List<BufferElement> GetBufferElements() => elements;
        public int GetStride() => stride;

        public void Add<T>(int count, bool normalised = false) where T : struct
        {
            VertexAttribPointerType type;
            if(typeof(float) == typeof(T))
            {
                type = VertexAttribPointerType.Float;
                stride += sizeof(float) * count;
            } else if (typeof(uint) == typeof(T))
            {
                type = VertexAttribPointerType.UnsignedByte;
                stride += sizeof(uint) * count;
            }
            else if (typeof(byte) == typeof(T))
            {
                type = VertexAttribPointerType.UnsignedByte;
                stride += sizeof(byte) * count;
            } else
            {
                throw new ArgumentException($"{typeof(T)} is not a valid type");
            }
            elements.Add(new BufferElement { type = type, count = count, normalised = normalised });
        }
    }
}
