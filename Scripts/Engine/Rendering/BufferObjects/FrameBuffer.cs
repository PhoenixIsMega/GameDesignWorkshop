using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects
{
    public class FrameBuffer : IBuffer //WIP
    {
        public int BufferID => throw new NotImplementedException();

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, BufferID);
        }

        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        //delete after done with

        public bool CheckStatus()
        {
            return (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) == FramebufferErrorCode.FramebufferComplete);
        }
    }
}
