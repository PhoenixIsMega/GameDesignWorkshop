using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Utility;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects;
using OpenTK.Windowing.Desktop;
using System;
using System.Reflection.Metadata;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Particles
{
    class Particle : GameObjectBase, IDisposable
    {
        private Quad quad;
        public int lifetime = 0;
        public float rotationspeed;
        private Random random;
        //make it so width height are always 90 and scale is always 1

        public Particle() : base()
        {
            quad = AddComponent<Quad>();

            quad.Width = 10.5f;
            quad.Height = 10.5f;
            random = new Random();
            rotationspeed = 120;
        }

        public void Move(float x, float y)
        {
            transform.X = x;
            transform.Y = y;
        }

        public (float, float) GetCoords()
        {
            return (transform.X, transform.Y);
        }

        public override float[] AssembleVertexData()
        {
            if (disposed) { return null; }
            
            float[] verticies = new float[] {
            //Positions         //UV /Textureslot (should all be the same)
             transform.X + (quad.Width*transform.ScaleX),  transform.Y + (quad.Height*transform.ScaleY), 0.0f,//top right
             transform.X + (quad.Width*transform.ScaleX),  transform.Y, 0.0f,  //bottom right
             transform.X,  transform.Y, 0.0f,                                               //bottom left
             transform.X,  transform.Y + (quad.Height*transform.ScaleY), 0.0f //top left
            }; ;
            VertexUtilities.RotateQuad(quad.RotationAngle, quad.Width, quad.Height, transform.X, transform.Y, ref verticies, 3);

            
            return verticies;
        }

        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            quad.RotationAngle = ((float)gameTime.TotalGameTime.TotalMilliseconds * rotationspeed) % 360;
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
