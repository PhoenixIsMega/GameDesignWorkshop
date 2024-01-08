using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using OpenTK.Graphics.OpenGL4;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers
{
    class CursorLayer : RenderLayerBase
    {
        public CursorLayer(string shaderPath) : base(shaderPath) {
            textureSlotsUsed = 2;
        }
        protected override BufferLayout LoadBufferLayout()
        {
            BufferLayout bufferLayout = new BufferLayout();
            bufferLayout.Add<float>(3); //xyz
            bufferLayout.Add<float>(2); //uv
            bufferLayout.Add<float>(1); //textureslot
            return bufferLayout;
        }

        protected override void LoadTextures()
        {
            var textureSampleUniformLocation = shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[2] { 0, 1 };
            GL.Uniform1(textureSampleUniformLocation, 2, samplers);

            ResourceManager.Instance.LoadTexture("Assets/Textures/Menu/tilemap_packed.png", TextureUnit.Texture0);
            ResourceManager.Instance.LoadTexture("Assets/Textures/Platformer/tilemap_packed.png", TextureUnit.Texture1);
        }

        protected override void UpdateArrayBuffer(float[] verticies, bool indexUpdate)
        {
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, verticies.Length * sizeof(float), verticies); // Update the vertex buffer data
        }
    }
}
