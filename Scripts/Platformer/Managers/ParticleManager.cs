using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.Particles;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Managers
{
    class ParticleManager
    {
        public List<Particle> particles = new List<Particle>();
        Random random = new Random();
        public ParticleManager()
        {
            particles.Add(new Particle());
            particles[0].Move(300, 300);
        }

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            //Particle newparticle = new Particle();
            //newparticle.Move(400, 500);
            //particles.Add(newparticle);
            foreach (Particle particle in particles.ToList())
            {
                particle.Update(gameWindow, gameTime);
                particle.lifetime++;
            }/*
                //particle.Move(particle.getCoords().Item1, particle.getCoords().Item2 - 10);
                particle.lifetime++;
                if (particle.lifetime == 20)
                {
                    //particle.Move(0, -1000);
                    //particles.Add(new Particle());
                    //particles[particles.Count - 1].Move(300, 300);
                    Particle newparticle = new Particle();
                    //newparticle.Move(particle.getCoords().Item1 + 10, 300);
                    particles.Add(newparticle);
                }
                //Console.WriteLine(particle.ToString() + " :" + particle.getCoords().ToString());
            }*/

            if (particles[particles.Count-1].lifetime == 3 && particles.Count < 200)
            {
                Particle newparticle = new Particle();
                newparticle.Move(particles[particles.Count-1].getCoords().Item1 + 10, 300);
                particles.Add(newparticle);
            }
            //Console.WriteLine("Particles: " + particles.Count);
        }

        public float[] AssembleVertexData()
        {
            List<float> listVerticies = new List<float>();
            foreach (Particle tile in particles)
            {
                if (tile == null) continue;
                float[] vertexData = tile.AssembleVertexData();
                foreach (float value in vertexData)
                {
                    listVerticies.Add(value);
                }
            }
            float[] verticies = listVerticies.ToArray();
            //Console.WriteLine(string.Join(" ", verticies));
            //Console.WriteLine("V count" + verticies.Length);
            return verticies;
        }

        public int CountTiles()
        {
            /*int count = 0;
            foreach (Particle tile in particles)
            {
                if (tile == null) continue;
                count++;
            }
            return count;*/
            return particles.Count;
        }
    }
}
