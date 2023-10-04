using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Components
{
    class Quad : Component
    {
        private float width = 0.0f;
        private float height = 0.0f;

        private float rotationAngle = 0.0f;

        public float Width {
            get { return width; }
            set { width = value; }
        }

        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        public float RotationAngle
        {
            get { return rotationAngle; }
            set { rotationAngle = value; }
        }
    }
}
