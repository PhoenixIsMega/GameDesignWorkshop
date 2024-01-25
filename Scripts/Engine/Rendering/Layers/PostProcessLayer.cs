using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers
{
    internal class PostProcessLayer : RenderLayerBase
    {
        protected FrameBuffer frameBuffer;

        private readonly float[] screenQuadVerticies = { // vertex attributes for a quad that fills the entire screen in Normalized Device Coordinates.
        // positions   // texCoords
        -1.0f,  1.0f,  0.0f, 1.0f,
        -1.0f, -1.0f,  0.0f, 0.0f,
         1.0f, -1.0f,  1.0f, 0.0f,

        -1.0f,  1.0f,  0.0f, 1.0f,
         1.0f, -1.0f,  1.0f, 0.0f,
         1.0f,  1.0f,  1.0f, 1.0f
    };
        public PostProcessLayer(string shaderPath) : base(shaderPath)
        {
            textureSlotsUsed = 0;
        }

        public override void LoadContent(float[] verticies)
        {
            shader = new ShaderProgramManager(ShaderProgramManager.ParseShader(shaderPath));
            if (!shader.CompileShader())
            {
                Console.WriteLine("Failed to compile shader");
                return;
            }
            vertexArray = new VertexArray();
            vertexBuffer = new VertexBuffer(screenQuadVerticies, BufferUsageHint.DynamicDraw); //ake modifieable for framebuffer

            frameBuffer = new FrameBuffer();

            BufferLayout bufferLayout = LoadBufferLayout();

            vertexArray.AddBuffer(vertexBuffer, bufferLayout);

            this.isLoaded = true;
        }



        protected override void LoadUniforms()
        {
            var textureSampleUniformLocation = shader.GetUniformLocation("screenTexture");
            GL.Uniform1(textureSampleUniformLocation, 0);
        }

        protected override BufferLayout LoadBufferLayout()
        {
            BufferLayout bufferLayout = new BufferLayout();
            bufferLayout.Add<float>(2); //xy
            bufferLayout.Add<float>(2); //texcoord
            return bufferLayout;
        }

        public override void Render(float[] verticies)
        {

            if (!isLoaded)
            {
                LoadContent(screenQuadVerticies);
            }

            shader.Use(); // Use the shader program

            LoadUniforms();

            vertexArray.Bind(); // Bind the vertex array
            vertexBuffer.Bind();


            GL.BindTexture(TextureTarget.Texture2D, frameBuffer.textureID);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);


            ErrorCode errorCode = GL.GetError();
            if (errorCode != ErrorCode.NoError)
            {
                Console.WriteLine($"OpenGL Error Drawing Elements FB: {errorCode}");
            }
        }

        public void UnbindFrameBuffer()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.Disable(EnableCap.DepthTest);
            GL.ClearColor(1.0f, 1.0f, 0.0f, 1.0f); //yellow
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            ErrorCode errorCode = GL.GetError();
            if (errorCode != ErrorCode.NoError)
            {
                Console.WriteLine($"OpenGL Error UnBindingFB: {errorCode}");
            }
        }

        public void BindFrameBuffer()
        {
            if (!isLoaded)
            {
                LoadContent(screenQuadVerticies);
            }
            frameBuffer.Bind();
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.0f, 0.0f, 1.0f, 1.0f); //blue
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            ErrorCode errorCode = GL.GetError();
            if (errorCode != ErrorCode.NoError)
            {
                Console.WriteLine($"OpenGL Error BindingFB: {errorCode}");
            }
        }
    }
}
