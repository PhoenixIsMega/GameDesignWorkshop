using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Utility;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Components
{
    public class PhysicsComponent : ComponentBase
    {
        private readonly Transform transform;

        private float mass = 1.0f;
        private float velocityX = 0.0f;
        private float velocityY = 0.0f;
        private float maxVelocityX = 8.5f;
        private float maxVelocityY = 9.81f;
        private float airResistance = 0.2f;
        public bool ignoreAirResistance = false;
        public float velocityThreshold = 0.16f;
        public bool gravity = false;

        public PhysicsComponent() : base()
        {
        }
        public PhysicsComponent(Transform transform) : this()
        {
            this.transform = transform;
        }

        public PhysicsComponent(Transform transform, float mass, float velocityX, float velocityY) : this(transform)
        {
            this.mass = mass;
            this.velocityX = velocityX;
            this.velocityY = velocityY;
        }

        public float VelocityX
        {
            get { return velocityX; }
            set { velocityX = value; }
        }

        public float VelocityY
        {
            get { return velocityY; }
            set { velocityY = value; }
        }

        public void ApplyForce(float forceX, float forceY)
        {
            velocityX = MathUtilities.Clamp(velocityX + forceX, -maxVelocityX, maxVelocityX);
            velocityY = MathUtilities.Clamp(velocityY + forceY, -maxVelocityY, maxVelocityY);
        }

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds * 100;
            transform.X += velocityX * deltaTime;
            transform.Y += velocityY * deltaTime;

            if (!ignoreAirResistance)
            {
                ApplyAirResistance(deltaTime);
            }

            if (gravity) //move to own function?
            {
                ApplyForce(0.0f, UniversalConstants.gravity * deltaTime);
            }
        }

        public void ApplyAirResistance(float deltaTime)
        {
            // Apply air resistance to velocity
            if (Math.Abs(velocityX) < velocityThreshold)
            {
                velocityX = 0.0f;
            }
            else if (velocityX > 0.0f)
            {
                velocityX -= airResistance * deltaTime;
            }
            else if (velocityX < 0.0f)
            {
                velocityX += airResistance * deltaTime;
            }

            if (Math.Abs(velocityY) < velocityThreshold)
            {
                velocityY = 0.0f;
            }
            else if (velocityY > 0.0f)
            {
                velocityY -= airResistance * deltaTime;
            }
            else if (velocityY < 0.0f)
            {
                velocityY += airResistance * deltaTime;
            }
        }
    }
}