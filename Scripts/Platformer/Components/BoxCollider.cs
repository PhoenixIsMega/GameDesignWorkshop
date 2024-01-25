using System;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Components
{
    public class BoxCollider : ComponentBase
    {
        private float scaleX = 0;
        private float scaleY = 0;
        Transform transform;
        Quad quad;

        public void AssignVariables(Transform transform, Quad quad)
        {
            this.quad = quad;
            this.transform = transform;
        }

        public float[] getLines()
        {
            float minX = transform.X;
            float minY = transform.Y;
            float maxY = transform.Y + (quad.Height * transform.ScaleY);
            float maxX = transform.X + (quad.Width * transform.ScaleX);
            //float centreY = ((2*transform.Y) + (quad.Height * transform.ScaleY)/2);
            //float centreX = ((2*transform.X) + (quad.Width * transform.ScaleX)/2);
            //float minX = centreX - (quad.Width * transform.ScaleX * scaleX) / 2;
            //float minY = centreY - (quad.Height * transform.ScaleY * scaleY) / 2;
            //float maxX = centreX + (quad.Width * transform.ScaleX * scaleX) / 2;
            //float maxY = centreY + (quad.Height * transform.ScaleY * scaleY) / 2;
            float[] lines = new float[24] 
            {minX, minY, 0f,
            minX, maxY, 0f,
            minX, maxY, 0f,
            maxX, maxY, 0f,
            maxX, maxY, 0f,
            maxX, minY, 0f,
            maxX, minY, 0f,
            minX, minY, 0f};

            return lines;
        }
            //add method to calculate lines for rendering

            //add collision bool method
        }
}
