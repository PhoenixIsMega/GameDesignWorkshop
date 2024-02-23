using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Utility
{
    public class RandomNumberGenerator
    {
        Random random = new Random();

        private readonly ClassManager classManager;
        public RandomNumberGenerator(ClassManager classManager)
        {
            this.classManager = classManager;
        }

        public int GenerateRandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public float GenerateRandomFloat(float min, float max)
        {
            return (float)random.NextDouble() * (max - min) + min;
        }
    }
}
