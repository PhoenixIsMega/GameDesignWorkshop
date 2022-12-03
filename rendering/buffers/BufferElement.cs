using OpenTK.Graphics.OpenGL4;

namespace GameDesignWorkshop.rendering.buffers
{
    public struct BufferElement
    {
        public VertexAttribPointerType type;
        public int count;
        public bool normalised;
    }
}
