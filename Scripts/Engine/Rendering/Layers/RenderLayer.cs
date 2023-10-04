using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers
{
    public abstract class RenderLayer
    {
        protected string shaderPath;
        protected ShaderProgramManager shader;
        protected VertexArray vertexArray;
        protected VertexBuffer vertexBuffer;
        protected IndexBuffer indexBuffer;
        protected bool isLoaded = false;
        protected uint[] _indices =
        {
            0, 1, 3, //tri 1
            1, 2, 3, //tri 2
        };

        protected bool indexUpdate = false;
        protected bool vertexUpdate = false;

        public RenderLayer(string shaderPath)
        {
            this.shaderPath = shaderPath;
        }

        protected abstract BufferLayout LoadBufferLayout();
        //protected abstract void LoadContent();
        public virtual void LoadContent(float[] verticies)
        {
            shader = new ShaderProgramManager(ShaderProgramManager.ParseShader(shaderPath));
            if (!shader.CompileShader())
            {
                Console.WriteLine("Failed to compile shader");
                return;
            }
            vertexArray = new VertexArray();
            vertexBuffer = new VertexBuffer(verticies, BufferUsageHint.DynamicDraw);
            indexBuffer = new IndexBuffer(_indices);

            BufferLayout bufferLayout = LoadBufferLayout();

            vertexArray.AddBuffer(vertexBuffer, bufferLayout);

            this.isLoaded = true;
        }

        protected virtual void LoadUniforms() { }
        protected virtual void LoadTextures() { }
        protected virtual void UpdateArrayBuffer(float[] verticies) { }

        public virtual void UpdateIndexBuffer(int numQuads)
        {
            //Console.WriteLine("indecies before: " + _indices.Length);
            //int numVerts = numQuads * 4;
            uint[] indices = new uint[numQuads * 6];

            for (int i = 0; i < numQuads; i++)
            {
                int baseIndex = i * 4;
                int baseOffset = i * 6;

                indices[baseOffset] = (uint)baseIndex;
                indices[baseOffset + 1] = (uint)(baseIndex + 1);
                indices[baseOffset + 2] = (uint)(baseIndex + 3);
                indices[baseOffset + 3] = (uint)(baseIndex + 1);
                indices[baseOffset + 4] = (uint)(baseIndex + 2);
                indices[baseOffset + 5] = (uint)(baseIndex + 3);
            }

            _indices = indices;
            indexUpdate = true;

            //Console.WriteLine("indecies after: " + _indices.Length);
        }

        public virtual void Render(float[] verticies)
        {
            if (!isLoaded)
            {
                LoadContent(verticies);
            }

            shader.Use(); // Use the shader program

            LoadTextures();

            vertexArray.Bind(); // Bind the vertex array
            vertexBuffer.Bind();

            UpdateArrayBuffer(verticies);
            indexBuffer.Bind(); // Bind the index buffer

            if (indexUpdate)
            {
                GL.Flush();
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(int), _indices, BufferUsageHint.DynamicDraw);
            }

            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport); // Get the viewport dimensions
            GL.Uniform2(GL.GetUniformLocation(shader.ProgramId, "ViewportSize"), (float)viewport[2], (float)viewport[3]); // Set the "ViewportSize" uniform in the shader
            LoadUniforms();
            //Console.WriteLine("indecies after2: " + shaderPath + " " + _indices.Length);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0); // Draw the elements using triangles

            

            ErrorCode errorCode = GL.GetError();
            if (errorCode != ErrorCode.NoError)
            {
                Console.WriteLine($"OpenGL Error DE: {errorCode}");
                // You can handle the error here, log it, or take other appropriate actions.
            }

            ResourceManager.Instance.UnloadTextures();
        }
    }
}
