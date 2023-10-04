using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects
{
    public interface IBuffer
    {
        int BufferID { get; }
        void Bind();
        void Unbind();
    }
}
