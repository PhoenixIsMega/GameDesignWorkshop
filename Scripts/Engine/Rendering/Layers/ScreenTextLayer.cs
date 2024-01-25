using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers
{
    internal class ScreenTextLayer : RenderLayerBase
    {
        public ScreenTextLayer(string shaderPath) : base(shaderPath)
        {
        }



        protected override BufferLayout LoadBufferLayout()
        {
            throw new NotImplementedException();
            //GL.Enable(EnableCap.CullFace);
            //GL.Enable(EnableCap.Blend);
            //GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

    }
}
