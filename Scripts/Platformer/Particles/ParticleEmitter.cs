using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Engine.Versions;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.GameObjects;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Particles
{
    public class ParticleEmitter : GameObjectBase
    {
        private readonly ClassManager classManager;

        public List<Particle> particles = new List<Particle>();
        private Particle particleToSpawn = new Particle();
        private Quad quad;
        public float randomVelocityXMin;
        public float randomVelocityXMax;
        public float randomVelocityYMin;
        public float randomVelocityYMax;
        public int maxParticles = 400;
        public bool invertSpawnRate = false;
        private int updateCount;
        public int particleSpawnRate = 4;

        //public bool continuous = true;

        public ParticleEmitter(ClassManager classManager) : base() {
            this.classManager = classManager;
            quad = AddComponent<Quad>();
            quad.Width = 0.01f;
            quad.Height = 0.01f;
        }

        public ParticleEmitter(ClassManager classManager, float sizeX, float sizeY) : this(classManager)
        {
            this.quad.Width = sizeX;
            this.quad.Height = sizeY;
        }

        public override float[] AssembleVertexData()
        {
            throw new NotImplementedException();
        }

        public void killAllParticles()
        {
            foreach (Particle particle in particles.ToList())
            {
                particles.Remove(particle);
                particle.Dispose();
            }
        }

        public void spawnNewParticle()
        {
            float X = getWorldPosition().x;
            float Y = getWorldPosition().y;
            float randomX = classManager.RandomNumberGenerator.GenerateRandomFloat(X - (quad.Width / 2), X + (quad.Width / 2));
            float randomY = classManager.RandomNumberGenerator.GenerateRandomFloat(Y - (quad.Height / 2), Y + (quad.Height / 2));
            Particle newparticle = new Particle(particleToSpawn);
            newparticle.Move(randomX, randomY);
            particles.Add(newparticle);
            float randomVelocityX = classManager.RandomNumberGenerator.GenerateRandomFloat(randomVelocityXMin, randomVelocityXMax);
            float randomVelocityY = classManager.RandomNumberGenerator.GenerateRandomFloat(randomVelocityXMin, randomVelocityXMax);
            newparticle.GetComponent<PhysicsComponent>().ApplyForce(randomVelocityX, randomVelocityY);
        }


        public override void Update(GameWindow gameWindow, GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds * 100;
            //Console.WriteLine(deltaTime);

            //remove old particles and update current
            foreach (Particle particle in particles.ToList())
            {
                particle.Update(gameWindow, gameTime);
                if (particle.lifetime > particle.lifetimeMax)
                {
                    particles.Remove(particle);
                    particle.Dispose();
                }
            }

            //spawn new particles
            if (particles.Count < maxParticles)
            {
                if (!invertSpawnRate)
                {
                    for (int i = 0; i < particleSpawnRate; i++)
                    {
                        spawnNewParticle();
                    }
                }
                else
                {
                    if (updateCount == particleSpawnRate)
                    {
                        spawnNewParticle();
                        updateCount = 0;
                    }
                    else
                    {
                        updateCount++;
                    }
                }
            }
        }

        public void SetParticleToSpawn(Particle particle)
        {
            particleToSpawn = particle;
        }
    }
}
