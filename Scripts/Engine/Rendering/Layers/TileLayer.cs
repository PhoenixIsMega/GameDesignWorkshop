using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers
{
    class TileLayer : RenderLayer
    {
        public TileLayer(string shaderPath) : base(shaderPath) { }
        protected override BufferLayout LoadBufferLayout()
        {
            BufferLayout bufferLayout = new BufferLayout();
            bufferLayout.Add<float>(3); //xyz
            bufferLayout.Add<float>(2); //uv
            bufferLayout.Add<float>(1); //textureslot
            return bufferLayout;
        }

        protected override void LoadUniforms()
        {
            CameraManager.Instance.setCameraUniform(shader.ProgramId);
            CameraManager.Instance.setCameraScaleUniform(shader.ProgramId);
        }

        protected override void LoadTextures()
        {
            var textureSampleUniformLocation = shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[2] { 0, 1 };
            GL.Uniform1(textureSampleUniformLocation, 2, samplers);

            ResourceManager.Instance.LoadTexture("Assets/Textures/Platformer/tiles_packed.png");
        }

        protected override void UpdateArrayBuffer(float[] verticies)
        {
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, verticies.Length * sizeof(float), verticies); // Update the vertex buffer data
        }
    }
}
