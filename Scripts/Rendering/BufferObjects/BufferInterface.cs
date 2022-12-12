using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.rendering.buffers
{
    public interface BufferInterface
    {
        int bufferID { get; }
        void Bind();
        void Unbind();
    }
}
