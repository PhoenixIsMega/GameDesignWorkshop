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
    class ParticleLayer : RenderLayer
    {
        public ParticleLayer(string shaderPath) : base(shaderPath) { }

        protected override BufferLayout LoadBufferLayout()
        {
            BufferLayout bufferLayout = new BufferLayout();
            bufferLayout.Add<float>(3); //xyz
            return bufferLayout;
        }

        protected override void LoadUniforms()
        {
            GL.Uniform4(GL.GetUniformLocation(shader.ProgramId, "color"), new Vector4(1, 1, 0.5f, 1));
            CameraManager.Instance.setCameraUniform(shader.ProgramId);
            CameraManager.Instance.setCameraScaleUniform(shader.ProgramId);
        }
        protected override void UpdateArrayBuffer(float[] verticies)
        {
            //Console.WriteLine("Particles vart length: " + verticies.Length + " vert length size: " + verticies.Length * sizeof(float));
            //GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, verticies.Length * sizeof(float), verticies); // Update the vertex buffer data
            GL.BufferData(BufferTarget.ArrayBuffer, verticies.Length * sizeof(float), verticies, BufferUsageHint.StreamDraw);
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
