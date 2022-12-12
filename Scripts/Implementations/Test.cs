using GameDesignWorkshop.me.phoenix.rendering.shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop
{
    internal class Test : Game
    {
        public Test(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
        {
        }

        private readonly float[] _verticies = {
            //positions x, y, z
            -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, //bottom left - red
            0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, //bottom right - green
            0.5f, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f //top - blue
        };

        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private Shader shader;
        protected override void Initialise()
        {
        }

        protected override void LoadContent()
        {
            //ShaderProgramSource src = Shader.ParseShader("me.phoenix/resources/Default.glsl");
            shader = new Shader(Shader.ParseShader("resources/shaders/Default.glsl"), true);
            //shader.CompileShader();

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _verticies.Length * sizeof(float), _verticies, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
        }


        protected override void Update(GameTime gameTime)
        {
            //Console.WriteLine("U:" + gameTime.ElapsedGameTime.TotalMilliseconds);
        }
        protected override void Render(GameTime gameTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(new Color4(0.8f, 0.4f, 0.5f, 1f));
            shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            //Console.WriteLine("R:" + gameTime.ElapsedGameTime.TotalMilliseconds);
            //Console.WriteLine("R:" + gameTime.TotalGameTime.TotalMilliseconds);
        }
    }
}
