using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers
{
    class ParticleLayer : RenderLayerBase
    {
        public ParticleLayer(string shaderPath) : base(shaderPath) {
            textureSlotsUsed = 0;
        }

        protected override BufferLayout LoadBufferLayout()
        {
            BufferLayout bufferLayout = new BufferLayout();
            bufferLayout.Add<float>(3); //xyz
            return bufferLayout;
        }

        protected override void LoadUniforms()
        {
            GL.Uniform4(GL.GetUniformLocation(shader.ProgramId, "color"), new Vector4(1, 1, 1.0f, 1));
            CameraManager.Instance.SetCameraUniform(shader.ProgramId);
            CameraManager.Instance.SetCameraScaleUniform(shader.ProgramId);
        }
        protected override void UpdateArrayBuffer(float[] verticies, bool indexUpdate)
        {
            if (indexUpdate) //if amount of verticies changed then use buffer data instead of sub data (need to implement this for tile layer)
            {
                GL.BufferData(BufferTarget.ArrayBuffer, verticies.Length * sizeof(float), verticies, BufferUsageHint.StreamDraw);
            } else
            {
                GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, verticies.Length * sizeof(float), verticies); // Update the vertex buffer data
            }
            // After an OpenGL function call, check for errors
            ErrorCode errorCode = GL.GetError();
            if (errorCode != ErrorCode.NoError)
            {
                Console.WriteLine($"OpenGL Error: {errorCode}");
                // You can handle the error here, log it, or take other appropriate actions.
            }
        }
    }
}
