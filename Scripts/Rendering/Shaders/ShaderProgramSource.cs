using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.me.phoenix.rendering.shaders
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
