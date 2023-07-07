using GameDesignWorkshop.management;
using GameDesignWorkshop.me.phoenix.rendering.shaders;
using GameDesignWorkshop.rendering;
using GameDesignWorkshop.rendering.buffers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Media;
using GameDesignWorkshop.resources.sounds;
using NAudio.Wave;

namespace GameDesignWorkshop
{
    internal class TextureWithColors : Game
    {
        public TextureWithColors(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
        {
        }

        private readonly float[] _verticies = {
            //Positions         //Colors
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, //top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, //bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,//bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f //top left
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
        private Texture2D texture;
        //private WaveOut waveOut;
        protected override void Initialise()
        {
            /*
            Console.WriteLine("Starting...");
            if (waveOut == null)
            {
                WaveFileReader reader = new WaveFileReader("resources/sounds/ACFF.wav");
                LoopStream loop = new LoopStream(reader);
                waveOut = new WaveOut();
                waveOut.Init(loop);
                waveOut.Play();
            }
            else
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }*/
        }

        protected override void LoadContent()
        {
            shader = new Shader(Shader.ParseShader("resources/shaders/Fader.glsl"));
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


            vertexArray.AddBuffer(vertexBuffer, bufferLayout);

            indexBuffer = new IndexBuffer(_indices);

            texture = ResourceManager.Instance.LoadTexture("resources/textures/sample.png");
            texture.Use();
        }
        //bool played = false;

        protected override void Update(GameTime gameTime)
        {
            //Console.WriteLine(Math.Floor(gameTime.TotalGameTime.TotalMilliseconds));
            /*if (Math.Floor(gameTime.TotalGameTime.TotalMilliseconds) % 3 == 0 && !played)
            {
                var exit = new CachedSound("resources/sounds/Exit.wav");
                //AudioPlaybackEngine.Instance.PlaySound(exit);
                played = true;
            }
            if (Math.Floor(gameTime.TotalGameTime.TotalMilliseconds) % 3 == 2 && played)
            {
                played = false;
            }*/
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

        protected override void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
