using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Sources;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers
{
    public class ShaderProgramManager
    {
        public int ProgramId { get; private set; }
        private ShaderProgramSource ShaderProgramSource { get; }
        public bool Compiled { get; private set; }

        private readonly IDictionary<string, int> uniforms = new Dictionary<string, int>();

        public ShaderProgramManager(ShaderProgramSource shaderProgramSource, bool compiled = false)
        {
            ShaderProgramSource = shaderProgramSource;
            if (compiled)
            {
                CompileShader();
            }
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(ProgramId);

                disposedValue = true;
            }
        }

        ~ShaderProgramManager()
        {
            if (disposedValue == false)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool CompileShader()
        {
            if (ShaderProgramSource == null)
            {
                throw new ArgumentNullException(nameof(ShaderProgramSource), "Shader Program Source is null");
            }

            if (Compiled)
            {
                throw new InvalidOperationException("Shader is already compiled");
            }

            // Create and compile vertex and fragment shaders
            int vertexShaderId = CreateAndCompileShader(ShaderType.VertexShader, ShaderProgramSource.VertexShaderSource);
            int fragmentShaderId = CreateAndCompileShader(ShaderType.FragmentShader, ShaderProgramSource.FragmentShaderSource);

            // Link shaders into a program and detach/delete shaders
            ProgramId = LinkDetachAndDeleteProgram(vertexShaderId, fragmentShaderId);

            // Retrieve active uniforms of the program
            RetrieveUniforms(ProgramId);

            Compiled = true;
            return true;
        }

        private int CreateAndCompileShader(ShaderType type, string shaderSource)
        {
            // Create a shader of the specified type
            int shaderId = GL.CreateShader(type);

            // Set the source code for the shader
            GL.ShaderSource(shaderId, shaderSource);

            // Compile the shader
            GL.CompileShader(shaderId);

            // Check the compilation status
            GL.GetShader(shaderId, ShaderParameter.CompileStatus, out int compilationStatus);

            if (compilationStatus != (int)All.True)
            {
                // Compilation failed, throw an exception with the error log
                throw new InvalidOperationException($"Shader compilation failed: {GL.GetShaderInfoLog(shaderId)}");
            }

            return shaderId;
        }

        private int LinkDetachAndDeleteProgram(int vertexShaderId, int fragmentShaderId)
        {
            // Create a program
            int programId = GL.CreateProgram();

            // Attach the shaders to the program
            GL.AttachShader(programId, vertexShaderId);
            GL.AttachShader(programId, fragmentShaderId);

            // Link the program
            GL.LinkProgram(programId);

            // Detach and delete the shaders
            GL.DetachShader(programId, vertexShaderId);
            GL.DetachShader(programId, fragmentShaderId);
            GL.DeleteShader(vertexShaderId);
            GL.DeleteShader(fragmentShaderId);

            return programId;
        }

        private void RetrieveUniforms(int programId)
        {
            // Get the total number of active uniforms in the program
            GL.GetProgram(programId, GetProgramParameterName.ActiveUniforms, out int totalUniforms);

            // Retrieve information about each uniform and store it in the dictionary
            for (int i = 0; i < totalUniforms; i++)
            {
                string key = GL.GetActiveUniform(programId, i, out _, out _);
                int location = GL.GetUniformLocation(programId, key);
                uniforms.Add(key, location);
            }
        }

        public int GetUniformLocation(string uniformName)
        {
            if (uniforms.TryGetValue(uniformName, out int location))
            {
                // Return the location of the uniform
                return location;
            }

            // Uniform not found, throw an exception
            throw new KeyNotFoundException($"Uniform '{uniformName}' not found");
        }

        public void Use()
        {
            if (!Compiled)
            {
                throw new InvalidOperationException("Shader has not been compiled");
            }

            // Use the shader program
            GL.UseProgram(ProgramId);
        }

        public static ShaderProgramSource ParseShader(string filePath)
        {
            string[] shaderSource = new string[2];
            Enums.ShaderType shaderType = Enums.ShaderType.NONE;
            var allLines = File.ReadAllLines(filePath);
            foreach (string line in allLines)
            {
                if (line.ToLower().Contains("#shader"))
                {
                    if (line.ToLower().Contains("fragment"))
                    {
                        shaderType = Enums.ShaderType.FRAGMENT;
                    }
                    else if (line.ToLower().Contains("vertex"))
                    {
                        shaderType = Enums.ShaderType.VERTEX;
                    }
                    else
                    {
                        // No shader type specified, throw an exception
                        throw new InvalidOperationException("No shader type has been supplied");
                    }
                }
                else
                {
                    // Append the shader source code based on the shader type
                    shaderSource[(int)shaderType] += (line + Environment.NewLine);
                }
            }
            return new ShaderProgramSource(shaderSource[(int)Enums.ShaderType.VERTEX], shaderSource[(int)Enums.ShaderType.FRAGMENT]);
        }
    }
}
