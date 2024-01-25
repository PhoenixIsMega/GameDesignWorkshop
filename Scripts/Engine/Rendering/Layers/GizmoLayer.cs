using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers
{
    class GizmoLayer : RenderLayerBase
    {

        public GizmoLayer(string shaderPath) : base(shaderPath) {
            textureSlotsUsed = 0;
        }

        public override void LoadContent(float[] verticies)
        {
            shader = new ShaderProgramManager(ShaderProgramManager.ParseShader(shaderPath));
            if (!shader.CompileShader())
            {
                Console.WriteLine("Failed to compile shader");
                return;
            }
            vertexArray = new VertexArray();
            vertexBuffer = new VertexBuffer(verticies, BufferUsageHint.DynamicDraw);

            BufferLayout bufferLayout = LoadBufferLayout();

            vertexArray.AddBuffer(vertexBuffer, bufferLayout);

            GL.LineWidth(10f);
            GL.Enable(EnableCap.LineSmooth);

            this.isLoaded = true;
        }

        protected override BufferLayout LoadBufferLayout()
        {
            BufferLayout bufferLayout = new BufferLayout();
            bufferLayout.Add<float>(3); //xyz
            return bufferLayout;
        }

        protected override void LoadUniforms()
        {
            GL.Uniform4(GL.GetUniformLocation(shader.ProgramId, "color"), new Vector4(1f, 0.2f, 0.5f, 1));
            CameraManager.Instance.SetCameraUniform(shader.ProgramId);
            CameraManager.Instance.SetCameraScaleUniform(shader.ProgramId, 1.0f);
        }
        protected override void UpdateArrayBuffer(float[] verticies, bool indexUpdate)
        {
            GL.BufferData(BufferTarget.ArrayBuffer, verticies.Length * sizeof(float), verticies, BufferUsageHint.DynamicDraw);
        }

        public override void Render(float[] verticies)
        {
            if (!isLoaded)
            {
                LoadContent(verticies);
            }

            shader.Use(); // Use the shader program

            vertexArray.Bind(); // Bind the vertex array
            vertexBuffer.Bind();
            UpdateArrayBuffer(verticies, true);

            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport); // Get the viewport dimensions
            GL.Uniform2(GL.GetUniformLocation(shader.ProgramId, "ViewportSize"), (float)viewport[2], (float)viewport[3]); // Set the "ViewportSize" uniform in the shader
            LoadUniforms();

            GL.DrawArrays(PrimitiveType.Lines, 0, verticies.Length / 3); // Draw the elements using triangles
        }
    }
}