using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes;
using GameDesignLearningAppPrototype.Scripts.Engine.Utility;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects;
using OpenTK.Windowing.Desktop;
using System;
using System.Reflection.Metadata;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Particles
{
    public class Particle : GameObjectBase, IDisposable
    {
        private Quad quad;
        private PhysicsComponent physicsComponent;
        private ColorComponent colorComponent;
        public int lifetime = 0;
        public float lifetimeMax = 75;
        public float rotationspeed = 0;
        public Color startColor = new Color(255, 0, 0, 255);
        public Color endColor = new Color(255, 0, 0, 0);
        public float startSize = 0.0f;
        public float endSize = 0.0f;
        public bool gravity = true;

        public Particle() : base()
        {
            quad = AddComponent<Quad>();
            physicsComponent = AddComponent<PhysicsComponent>(this.transform);
            colorComponent = AddComponent<ColorComponent>(this.startColor);

            quad.Width = startSize;
            quad.Height = startSize;
            physicsComponent.VelocityY = 0;
            physicsComponent.ignoreAirResistance = true;
        }

        public Particle(Particle particle) : this()
        {
            this.lifetimeMax = particle.lifetimeMax;
            this.rotationspeed = particle.rotationspeed;
            this.startColor = particle.startColor;
            this.endColor = particle.endColor;
            this.startSize = particle.startSize;
            this.endSize = particle.endSize;
            this.physicsComponent.gravity = particle.physicsComponent.gravity;
        }
        

        public void Move(float x, float y)
        {
            transform.X = x;
            transform.Y = y;
        }

        public void SetSize(float size)
        {
            quad.Width = size;
            quad.Height = size;
        }

        private void SetColor(Color color)
        {
            colorComponent.SetColor(color);
        }

        public (float, float) GetCoords()
        {
            return (transform.X, transform.Y);
        }

        public override float[] AssembleVertexData()
        {
            if (disposed) { return null; }

            Color currentColor = colorComponent.GetColor();

            float R = MathUtilities.Normalise(0, 255, currentColor.R);
            float G = MathUtilities.Normalise(0, 255, currentColor.G);
            float B = MathUtilities.Normalise(0, 255, currentColor.B);
            float A = MathUtilities.Normalise(0, 255, currentColor.A);

            float[] verticies = new float[] {
            //Positions         //UV /Textureslot (should all be the same)
             transform.X + (quad.Width*transform.ScaleX),  transform.Y + (quad.Height*transform.ScaleY), 0.0f  ,R,G, B, A,//top right
             transform.X + (quad.Width*transform.ScaleX),  transform.Y, 0.0f ,R,G, B, A,  //bottom right
             transform.X,  transform.Y, 0.0f ,R,G, B, A,                                               //bottom left
             transform.X,  transform.Y + (quad.Height*transform.ScaleY), 0.0f ,R,G, B, A //top left
            }; ;
            VertexUtilities.RotateQuad(quad.RotationAngle, quad.Width, quad.Height, transform.X, transform.Y, ref verticies, 7);

            
            return verticies;
        }

        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            physicsComponent.Update(gameWindow, gameTime);
            quad.RotationAngle = ((float)gameTime.TotalGameTime.TotalMilliseconds * rotationspeed) % 360;
            float normalisedLifetime = MathUtilities.Normalise(0, lifetimeMax, lifetime);
            SetSize(MathUtilities.Lerp(startSize, endSize, normalisedLifetime));
            SetColor(MathUtilities.ColorLerp(startColor, endColor, normalisedLifetime));

            lifetime++;
        }

        private bool disposed; // Flag to track if the object has been disposed

        ~Particle()
        {
            Dispose(false); // Destructor that calls Dispose method with disposing set to false
        }

        public void Dispose(bool disposing)
        {
            if (!disposed) // Check if the object has already been disposed
            {
                //any extra code here
                disposed = true; // Set the disposed flag to true
            }
        }

        public void Dispose()
        {
            Dispose(true); // Public Dispose method that calls Dispose method with disposing set to true
            GC.SuppressFinalize(this); // Request the garbage collector not to call the finalizer
        }
    }
}
