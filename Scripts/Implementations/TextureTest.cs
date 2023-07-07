﻿using GameDesignWorkshop.management;
using GameDesignWorkshop.me.phoenix.rendering.shaders;
using GameDesignWorkshop.rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop
{
    internal class TextureTest : Game
    {
        public TextureTest(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
        {
        }

        private readonly float[] _verticies = {
            //Positions         //Colors
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, //top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, //bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, //bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f,  //top left
        };

        private uint[] _indices =
        {
            0, 1, 3, //tri 1
            1, 2, 3 //tri 2
        };

        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;

        private Shader shader;
        private Texture2D texture;
        protected override void Initialise()
        {
        }

        protected override void LoadContent()
        {
            //ShaderProgramSource src = Shader.ParseShader("me.phoenix/resources/Default.glsl");
            shader = new Shader(Shader.ParseShader("resources/shaders/Texture.glsl"), true);
            //shader.CompileShader();

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _verticies.Length * sizeof(float), _verticies, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            texture = ResourceManager.Instance.LoadTexture("resources/textures/sample.png");
            texture.Use();
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
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            //Console.WriteLine("R:" + gameTime.ElapsedGameTime.TotalMilliseconds);
            //Console.WriteLine("R:" + gameTime.TotalGameTime.TotalMilliseconds);
        }

        protected override void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
