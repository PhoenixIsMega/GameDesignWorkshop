namespace GameDesignLearningAppPrototype.Scripts.Platformer.Components
{
    public class Transform : ComponentBase
    {
        private float x = 0.0f;
        private float y = 0.0f;

        private float scaleX = 1.0f;
        private float scaleY = 1.0f;
        public Transform() : base()
        {
        }

        public Transform(float x, float y, float scaleX, float slaceY) : this()
        {
            this.x = x;
            this.y = y;
            this.scaleX = scaleX;
            this.scaleY = slaceY;
        }

        public float X
        {
            get { return x; }
            set { x = value; 
            }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float ScaleX
        {
            get { return scaleX; }
            set { scaleX = value; }
        }

        public float ScaleY
        {
            get { return scaleY; }
            set { scaleY = value; }
        }
    }
}
