using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Engine.Versions;
using GameDesignLearningAppPrototype.Scripts.Platformer.Particles;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Managers
{
    public class ParticleManager
    {
        private readonly ClassManager classManager;
        public List<ParticleEmitter> particleEmitters = new List<ParticleEmitter>();

        public ParticleManager(ClassManager classManager)
        {
            this.classManager = classManager;
        }

        public ParticleEmitter createParticleEmitter(params object[] constructorArgs)
        {
            ParticleEmitter particleEmitter = (ParticleEmitter)Activator.CreateInstance(typeof(ParticleEmitter), constructorArgs);
            particleEmitters.Add(particleEmitter); //add children to gameobject to fix this inheritance issue
            //particleEmitters[0].randomVelocityX = 0.1f;
            return particleEmitter;
        }

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            foreach (ParticleEmitter particleEmitter in particleEmitters.ToList())
            {
                particleEmitter.Update(gameWindow, gameTime);
            }
            //Console.WriteLine("Particle count: " + CountTiles());
        }

        public float[] CombineVertexData()
        {
            List<float> listVerticies = new List<float>();

            float[] vertexData;

            foreach (ParticleEmitter particleEmitter in particleEmitters.ToList())
            {
                foreach (Particle particle in particleEmitter.particles)
                {
                    if (particle == null) continue;
                    vertexData = particle.AssembleVertexData();
                    if (vertexData == null) { continue; }
                    foreach (float vertex in vertexData)
                    {
                        listVerticies.Add(vertex);
                    }
                }
            }
            float[] verticies = listVerticies.ToArray();
            return verticies;
        }

        public int CountTiles()
        {
            int count = 0;
            foreach (ParticleEmitter particleEmitter in particleEmitters.ToList())
            {
                if (particleEmitter.particles == null) continue;
                count += particleEmitter.particles.Count;
            }
            return count;
        }
    }
}
