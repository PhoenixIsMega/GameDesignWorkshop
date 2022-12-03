using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.rendering.buffers
{
    public static class Utilities
    {
        public static int GetSizeOfVertexAttribPointerType(VertexAttribPointerType attribPointerType)
        {
            switch (attribPointerType)
            {
                case VertexAttribPointerType.UnsignedByte:
                    return 1;
                    break;
                case VertexAttribPointerType.UnsignedInt:
                    return 4;
                    break;
                case VertexAttribPointerType.Float:
                    return 4;
                    break;
                default:
                    return 0;
                    break;
            }
        }
    }
}