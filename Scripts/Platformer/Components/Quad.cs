namespace GameDesignLearningAppPrototype.Scripts.Platformer.Components
{
    public class Quad : ComponentBase
    {
        private float width = 0.0f;
        private float height = 0.0f;

        private float rotationAngle = 0.0f;

        public Quad() : base()
        {
        }

        public Quad(float width, float height, float rotationAngle) : this()
        {
            this.width = width;
            this.height = height;
            this.rotationAngle = rotationAngle;
        }

        public float Width
        {
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
