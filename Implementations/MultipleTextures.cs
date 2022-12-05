using GameDesignWorkshop.management;
using GameDesignWorkshop.me.phoenix.rendering.shaders;
using GameDesignWorkshop.rendering;
using GameDesignWorkshop.rendering.buffers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop
{
    internal class MultipleTextures : Application
    {
        public MultipleTextures (string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
        {
        }

        private readonly float[] _verticies = {
            //Positions         //UV        //Colors
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, //top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, //bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f,//bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f//top left
        };

        private uint[] _indices =
        {
            0, 1, 3, //tri 1
            1, 2, 3 //tri 2
        };

        private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer;
        private VertexArray vertexArray;

        private Shader shader;
        protected override void Initialise()
        {
        }

        protected override void LoadContent()
        {
            shader = new Shader(Shader.ParseShader("resources/shaders/TextureWithColorAndTextureSlot.glsl"));
            if (!shader.CompileShader())
            {
                Console.WriteLine("Failed to compile shader");
                return;
            }
            vertexArray = new VertexArray();
            vertexBuffer = new VertexBuffer(_verticies);

            BufferLayout bufferLayout = new BufferLayout();
            bufferLayout.Add<float>(3); //xyz
            bufferLayout.Add<float>(2); //uv
            bufferLayout.Add<float>(3); //color
            bufferLayout.Add<float>(1); //textureslot


            vertexArray.AddBuffer(vertexBuffer, bufferLayout);

            shader.Use();

            indexBuffer = new IndexBuffer(_indices);
            var textureSamepleUniformLocation = shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[2] { 0, 1 };
            GL.Uniform1(textureSamepleUniformLocation, 2, samplers);

            ResourceManager.Instance.LoadTexture("resources/textures/tiles_spritesheet.png");
            ResourceManager.Instance.LoadTexture("resources/textures/sample.png");

            //texture = ResourceManager.Instance.LoadTexture("resources/textures/sample.png");
            //texture.Use();
        }


        protected override void Update(GameTime gameTime)
        {
        }
        protected override void Render(GameTime gameTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); //swap?
            GL.ClearColor(new Color4(0.8f, 0.4f, 0.5f, 1f));
            shader.Use();
            vertexArray.Bind();
            indexBuffer.Bind();

            GL.Uniform1(GL.GetUniformLocation(shader.ProgramId, "blackness"), (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds) / (2.0f) + 0.5f);

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
