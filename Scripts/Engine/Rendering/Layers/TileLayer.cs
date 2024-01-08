using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using OpenTK.Graphics.OpenGL4;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers
{
    class TileLayer : RenderLayerBase
    {
        public TileLayer(string shaderPath) : base(shaderPath) {
            textureSlotsUsed = 1;
        }
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
            CameraManager.Instance.SetCameraUniform(shader.ProgramId);
            CameraManager.Instance.SetCameraScaleUniform(shader.ProgramId);
        }

        protected override void LoadTextures()
        {
            var textureSampleUniformLocation = shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[1] { 0};
            GL.Uniform1(textureSampleUniformLocation, 1, samplers);

            ResourceManager.Instance.LoadTexture("Assets/Textures/Platformer/tilemap_packed.png", TextureUnit.Texture0);
        }

        protected override void UpdateArrayBuffer(float[] verticies, bool indexUpdate)
        {
            GL.BufferData(BufferTarget.ArrayBuffer, verticies.Length * sizeof(float), verticies, BufferUsageHint.DynamicDraw);
        }
    }
}
