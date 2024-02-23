using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using OpenTK.Graphics.OpenGL4;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers
{
    public class PlayerLayer : RenderLayerBase
    {
        private readonly ClassManager classManager;
        public PlayerLayer(ClassManager classManager, string shaderPath) : base(shaderPath) {
            this.classManager = classManager;
            textureSlotsUsed = 1;
        }
        protected override BufferLayout LoadBufferLayout()
        {
            BufferLayout bufferLayout = new BufferLayout();
            bufferLayout.Add<float>(3); //xyz
            bufferLayout.Add<float>(2); //uv
            bufferLayout.Add<float>(3); //color
            bufferLayout.Add<float>(1); //textureslot
            return bufferLayout;
        }

        protected override void LoadUniforms()
        {
            GL.Uniform1(GL.GetUniformLocation(shader.ProgramId, "blackness"), 1.0f); // Set the "blackness" uniform value in the shader
            classManager.CameraManager.SetCameraUniform(shader.ProgramId);
            classManager.CameraManager.SetCameraScaleUniform(shader.ProgramId, 1.0f);
        }

        protected override void LoadTextures()
        {
            var textureSampleUniformLocation = shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[1] { 0};
            GL.Uniform1(textureSampleUniformLocation, 1, samplers);

            ResourceManager.Instance.LoadTexture("Assets/Textures/Platformer/characters_packed.png");
        }

        protected override void UpdateArrayBuffer(float[] verticies, bool indexUpdate)
        {
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, verticies.Length * sizeof(float), verticies); // Update the vertex buffer data
        }
    }
}
