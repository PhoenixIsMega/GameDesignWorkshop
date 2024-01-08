using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Utility
{
    public static class VertexUtilities
    {
        public static void RotateQuad(float rotationAngle, float quadWidth, float quadHeight, float x, float y, ref float[] _verticies, int stride) //stride should be temp
        {
            // Update the vertex positions, UVs, colors, and textureslot
            
            // Calculate the center point of the quad
            float centerX = x + quadWidth / 2;
            float centerY = y + quadHeight / 2;

            // Apply the rotation transformation
            float rotationRadians = rotationAngle * (float)Math.PI / 180f;  // Convert the angle to radians
            float cosTheta = (float)Math.Cos(rotationRadians);
            float sinTheta = (float)Math.Sin(rotationRadians);
            for (int i = 0; i < _verticies.Length; i += stride)
            {
                float vertexX = _verticies[i] - centerX;
                float vertexY = _verticies[i + 1] - centerY;

                // Apply the rotation transformation
                _verticies[i] = vertexX * cosTheta - vertexY * sinTheta + centerX;
                _verticies[i + 1] = vertexX * sinTheta + vertexY * cosTheta + centerY;
            }

            // Update the _verticies array with the translated vertices
            //Array.Copy(translatedVertices, _verticies, translatedVertices.Length);

            //return _verticies;
        }

        public static float[] ScaleQuad(float scaleX, float scaleY, float quadWidth, float quadHeight, float x, float y, float[] _vertices, int stride)
        {
            // Calculate the center point of the quad
            float centerX = x + quadWidth / 2;
            float centerY = y + quadHeight / 2;

            // Apply the scaling transformation
            for (int i = 0; i < _vertices.Length; i += stride)
            {
                // Scale the x-coordinate
                float scaledX = (_vertices[i] - centerX) * scaleX + centerX;
                _vertices[i] = scaledX;

                // Scale the y-coordinate
                float scaledY = (_vertices[i + 1] - centerY) * scaleY + centerY;
                _vertices[i + 1] = scaledY;
            }

            return _vertices;
        }

    }
}
