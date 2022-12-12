using GameDesignWorkshop.game.objects.particles;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.Scripts.Game.Utility
{
    public static class MathUtilities
    {
        // Linearly interpolate between two RGBA colors
        public static RGBAColor Lerp(RGBAColor a, RGBAColor b, float t) //start end amount
        {
            RGBAColor result = new RGBAColor();
            result.R = (byte)Lerp(a.R, b.R, t);
            result.G = (byte)Lerp(a.G, b.G, t);
            result.B = (byte)Lerp(a.B, b.B, t);
            result.A = (byte)Lerp(a.A, b.A, t);
            return result;
        }

        // Linearly interpolate between two values
        private static float Lerp(float a, float b, float t) //could change to start, end and amount
        {
            return (1 - t) * a + t * b; //t must be between 0-1
        }

        public static int GetSizeOfVertexAttribPointerType(VertexAttribPointerType attribPointerType)
        {
            switch (attribPointerType)
            {
                case VertexAttribPointerType.UnsignedByte:
                    return 1;
                //break;
                case VertexAttribPointerType.UnsignedInt:
                    return 4;
                //break;
                case VertexAttribPointerType.Float:
                    return 4;
                //break;
                default:
                    return 0;
                //break;
            }
        }
    }
}
