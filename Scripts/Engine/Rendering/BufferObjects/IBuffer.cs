namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects
{
    public interface IBuffer
    {
        int BufferID { get; }
        void Bind();
        void Unbind();
    }
}
