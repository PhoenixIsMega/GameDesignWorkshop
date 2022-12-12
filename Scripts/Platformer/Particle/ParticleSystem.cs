using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GameDesignWorkshop.game.objects.particles
{
    [Serializable]
    class ParticleSystem
    {
        // List of particles in the system
        List<Particle> particles;

        // Emission rate, in particles per second
        public float EmissionRate { get; set; }

        // Emission area, represented as a rectangle
        public Rectangle EmissionArea { get; set; }

        // Initial velocity of each particle, in pixels per second
        public Vector2 Velocity { get; set; }

        // Acceleration of each particle, in pixels per second squared
        public Vector2 Acceleration { get; set; }

        // Starting color of each particle
        public RGBAColor StartColor { get; set; }

        // Ending color of each particle
        public RGBAColor EndColor { get; set; }

        // Starting size of each particle, in pixels
        public float StartSize { get; set; }

        // Ending size of each particle, in pixels
        public float EndSize { get; set; }

        // Rotational speed of each particle, in radians per second
        public float RotationSpeed { get; set; }

        // Starting opacity of each particle, from 0 (transparent) to 1 (opaque)
        public float StartOpacity { get; set; }

        // Ending opacity of each particle, from 0 (transparent) to 1 (opaque)
        public float EndOpacity { get; set; }

        // Time since the last update, in seconds
        float elapsedTime;

        // Update the position and lifespan of each particle in the system
        // Update the position and lifespan of each particle in the system
        public void Update()
        {
            // Decrement the lifespan of each particle by the elapsed time
            foreach (Particle particle in particles)
            {
                particle.Lifespan -= elapsedTime;
            }

            // Remove any particles that have reached the end of their lifespan
            particles = particles.Where(p => p.Lifespan > 0).ToList();

            // Calculate the number of particles to spawn this frame
            int numParticles = (int)(EmissionRate * elapsedTime);

            // Spawn the new particles
            for (int i = 0; i < numParticles; i++)
            {
                // Generate a random position within the emission area
                Vector2 position = new Vector2(
                    //random.Next(EmissionArea.Left, EmissionArea.Right),
                    //random.Next(EmissionArea.Top, EmissionArea.Bottom)
                );

                // Create the particle with the specified properties
                Particle particle = new Particle
                {
                    Position = position,
                    Velocity = Velocity,
                    Acceleration = Acceleration,
                    //Lifespan = random.Next(Particle.MIN_LIFESPAN, Particle.MAX_LIFESPAN),
                    StartColor = StartColor,
                    EndColor = EndColor,
                    StartSize = StartSize,
                    EndSize = EndSize,
                    RotationSpeed = RotationSpeed,
                    //StartOpacity = StartOpacity,
                    //EndOpacity = EndOpacity
                };

                // Add the particle to the list
                particles.Add(particle);
            }
        }
    }
}
