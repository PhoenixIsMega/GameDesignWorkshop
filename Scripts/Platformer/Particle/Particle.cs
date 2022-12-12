using GameDesignWorkshop.Scripts.Game.Utility;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameDesignWorkshop.game.objects.particles
{
    [Serializable]
    class Particle
    {
        // Lifespan of the particle, in seconds
        public float Lifespan { get; set; }

        // Position, velocity, and acceleration of the particle
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        // Starting and ending colors of the particle
        public RGBAColor StartColor { get; set; }
        public RGBAColor EndColor { get; set; }

        // Starting and ending sizes of the particle
        public float StartSize { get; set; }
        public float EndSize { get; set; }

        // Rotation speed of the particle, in degrees per second
        public float RotationSpeed { get; set; }
        public float Rotation { get; set; }

        // Starting and ending opacities of the particle
        //public float StartOpacity { get; set; }
        //public float EndOpacity { get; set; }

        // Update the position and velocity of the particle based on its acceleration and elapsed time
        public void Update(float elapsedTime)
        {
            // Update the velocity of the particle
            Velocity += Acceleration * elapsedTime;

            // Update the position of the particle
            Position += Velocity * elapsedTime + 0.5f * Acceleration * elapsedTime * elapsedTime;

            // Update the color, size, and opacity of the particle
            // based on its lifespan and the starting and ending values
            float age = Lifespan - elapsedTime;
            float lifespan = Lifespan;
            RGBAColor color = MathUtilities.Lerp(EndColor, StartColor, age / lifespan);
            //float Size = (float)MathUtilities.Lerp(EndSize,StartSize, age / lifespan);
            //Opacity = Lerp(EndOpacity, StartOpacity, age / lifespan);

            // Update the rotation of the particle
            Rotation += RotationSpeed * elapsedTime;
        }
    }
}
