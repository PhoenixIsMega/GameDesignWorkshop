using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Versions;
using System;

namespace GameDesignLearningAppPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine game = new PrototypeVersion1("Game Design Workshop by Phoenix Thomson", 1280, 720);
            game.Start();
        }
    }
}
