namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Sources
{
    public class ShaderProgramSource
    {
        public string VertexShaderSource;
        public string FragmentShaderSource;

        public ShaderProgramSource(string vertexShaderSource, string fragmentShaderSource)
        {
            this.VertexShaderSource = vertexShaderSource;
            this.FragmentShaderSource = fragmentShaderSource;
        }
    }
}
