using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes;
using OpenTK.Graphics.OpenGL4;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Utility
{
    public static class MathUtilities
    {
        // Linearly interpolate between two values
        private static float Lerp(float a, float b, float t) //could change to start, end and amount
        {
            return (1 - t) * a + t * b; //t must be between 0-1
        }

        public static Color ColorLerp(Color colorA, Color colorB, float t)
        {
            return new Color((int)Lerp(colorA.R, colorB.R, t), (int)Lerp(colorA.G, colorB.G, t), (int)Lerp(colorA.B, colorB.B, t), (int)Lerp(colorA.A, colorB.A, t));
        }

        public static float SmoothStep(float a, float b, float t)
        {
            return (float)(t * t * (3.0 - 2.0 * t)); //t must be between 0-1
        }

        public static float Normalise(float startValue, float endValue, float currentValue)
        {
            //start value must be lower than end value
            float normalisedValue = currentValue / (endValue - startValue);
            if (normalisedValue < 0f || normalisedValue > 1f) {
                Console.WriteLine("Normalisation error - output: " + normalisedValue);
                return 0;
            }
            return normalisedValue;
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
