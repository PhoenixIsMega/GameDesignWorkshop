using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace GameDesignWorkshop.me.phoenix.rendering.shaders
{
    public class Shader
    {
        public int ProgramId { get; private set; }
        private ShaderProgramSource shaderProgramSource { get; }
        public bool Compiled { get; private set; }

        private readonly IDictionary<string, int> uniforms = new Dictionary<string, int>();

        public Shader(ShaderProgramSource shaderProgramSource, bool compile = false)
        {
            this.shaderProgramSource = shaderProgramSource;
            if (compile)
            {
                CompileShader();
            }
        }

        public bool CompileShader()
        {
            if(shaderProgramSource == null)
            {
                Console.WriteLine("Shader Program Source is null");
                return false;
            }
            if (Compiled)
            {
                Console.WriteLine("shader is already compiled");//warning?
                return false;
            }
            int vertexShaderId = GL.CreateShader(OpenTK.Graphics.OpenGL4.ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderId, shaderProgramSource.VertexShaderSource);
            GL.CompileShader(vertexShaderId);
            GL.GetShader(vertexShaderId, ShaderParameter.CompileStatus, out var vertexShaderCompilationCode);
            if (vertexShaderCompilationCode != (int)All.True)
            {
                Console.WriteLine(GL.GetShaderInfoLog(vertexShaderId));
                return false;
            }

            //make into further methods
            int fragmentShaderId = GL.CreateShader(OpenTK.Graphics.OpenGL4.ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderId, shaderProgramSource.FragmentShaderSource);
            GL.CompileShader(fragmentShaderId);
            GL.GetShader(fragmentShaderId, ShaderParameter.CompileStatus, out var fragmentShaderCompilationCode);
            if (fragmentShaderCompilationCode != (int)All.True)
            {
                Console.WriteLine(GL.GetShaderInfoLog(vertexShaderId));
                return false;
            }

            ProgramId = GL.CreateProgram();
            GL.AttachShader(ProgramId, vertexShaderId);
            GL.AttachShader(ProgramId, fragmentShaderId);

            GL.LinkProgram(ProgramId);

            GL.DetachShader(ProgramId, vertexShaderId);
            GL.DetachShader(ProgramId, fragmentShaderId);

            GL.DeleteShader(vertexShaderId);
            GL.DeleteShader(fragmentShaderId);


            GL.GetProgram(ProgramId, GetProgramParameterName.ActiveUniforms, out var totalUniforms);
            for (int i = 0; i < totalUniforms; i++)
            {
                string key = GL.GetActiveUniform(ProgramId, i, out _, out _);
                int location = GL.GetUniformLocation(ProgramId, key);
                uniforms.Add(key, location);
            }

            Compiled = true;
            return true;
        }

        public int GetUniformLocation(string uniformName) => uniforms[uniformName];

        public void Use()
        {
            if (Compiled)
            {
                GL.UseProgram(ProgramId);
            } else
            {
                Console.WriteLine("Shader has not been compiled!");
            }
        }

        public static ShaderProgramSource ParseShader(string filePath)
        {
            string[] shaderSource = new string[2];
            ShaderType shaderType = ShaderType.NONE;
            var allLines = File.ReadAllLines(filePath);
            foreach (string line in allLines)
            {
                //Console.WriteLine(line);
                if (line.ToLower().Contains("#shader"))
                {
                    if (line.ToLower().Contains("fragment"))
                    {
                        shaderType = ShaderType.FRAGMENT;
                    }
                    else if (line.ToLower().Contains("vertex"))
                    {
                        shaderType = ShaderType.VERTEX;
                    }
                    else
                    {
                        Console.WriteLine("Error. no shader type has been supplied");
                    }
                }
                else
                {
                    shaderSource[(int)shaderType] += (line + Environment.NewLine);
                }
            }
            return new ShaderProgramSource(shaderSource[(int)ShaderType.VERTEX], shaderSource[(int)ShaderType.FRAGMENT]);
        }

        internal static ShaderProgramSource ParseShader()
        {
            throw new NotImplementedException();
        }
    }
}
