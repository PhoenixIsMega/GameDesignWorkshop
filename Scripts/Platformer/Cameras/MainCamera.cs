using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects;
using OpenTK.Windowing.Desktop;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Cameras
{
    public class MainCamera : GameObjectBase
    {
        float velocityX = 0;
        float velocityY = 0;
        float speed = 1.5f;
        float airResistance = 1.0f;
        float maxVelocity = 15.0f;
        float threshold = 1.0f;

        //make it so width height are always 90 and scale is always 1

        public MainCamera() : base()
        {
        }

        public override float[] AssembleVertexData()
        {
            return null;
        }

        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
        }

        public void SetPosition(float x, float y)
        {
            transform.X = x;
            transform.Y = y;
        }
    }
}
