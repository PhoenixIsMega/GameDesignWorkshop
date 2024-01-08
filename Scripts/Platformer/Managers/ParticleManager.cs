using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using GameDesignLearningAppPrototype.Scripts.Engine.Versions;
using GameDesignLearningAppPrototype.Scripts.Platformer.Particles;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Managers
{
    class ParticleManager
    {
        public List<Particle> particles = new List<Particle>();
        private Random random = new Random();
        private PrototypeVersion1 game;
        public ParticleManager(PrototypeVersion1 game)
        {
            particles.Add(new Particle());
            particles[0].Move(300, 300);
            this.game = game;
        }

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {

            foreach (Particle particle in particles.ToList())
            {
                particle.Update(gameWindow, gameTime);
                particle.Move(particle.GetCoords().Item1, particle.GetCoords().Item2-15);
                particle.lifetime++;
                if (particle.lifetime > 50)
                {
                    particles.Remove(particle);
                    particle.Dispose();
                }
            }

            //if (particles[particles.Count - 1].lifetime == 1 && particles.Count < 200)
            if (particles.Count < 2)
            { //I KNOW THIS IS BAD BUT ILL OPTIMISE LATER
                Particle newparticle = new Particle();
                newparticle.Move(random.Next((int)game.getPlayerLocation().Item1 - ((int)(740 * 2)), (int)game.getPlayerLocation().Item1 + ((int)(740 * 2))), (int)game.getPlayerLocation().Item2 + 430);
                particles.Add(newparticle);
                newparticle = new Particle();
                newparticle.Move(random.Next((int)game.getPlayerLocation().Item1 - ((int)(740 * 2)), (int)game.getPlayerLocation().Item1 + ((int)(740 * 2))), (int)game.getPlayerLocation().Item2 + 430);
                particles.Add(newparticle);
                newparticle = new Particle();
                newparticle.Move(random.Next((int)game.getPlayerLocation().Item1 - ((int)(740 * 2)), (int)game.getPlayerLocation().Item1 + ((int)(740 * 2))), (int)game.getPlayerLocation().Item2 + 430);
                particles.Add(newparticle);
                newparticle = new Particle();
                newparticle.Move(random.Next((int)game.getPlayerLocation().Item1 - ((int)(740 * 2)), (int)game.getPlayerLocation().Item1 + ((int)(740 * 2))), (int)game.getPlayerLocation().Item2 + 430);
                particles.Add(newparticle);
            }
        }

        public float[] CombineVertexData()
        {
            List<float> listVerticies = new List<float>();

            float[] vertexData;

            foreach (Particle particle in particles)
            {
                if (particle == null) continue;
                vertexData = particle.AssembleVertexData();
                if(vertexData == null) { continue; }
                foreach (float vertex in vertexData)
                {
                    listVerticies.Add(vertex);
                }
            }
            float[] verticies = listVerticies.ToArray();
            return verticies;
        }

        public int CountTiles()
        {
            return particles.Count;
        }
    }
}
