using GameDesignLearningAppPrototype.Scripts;
using GameDesignLearningAppPrototype.Scripts.Engine.Versions;

namespace GameDesignLearningAppPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            EngineBase game = new PrototypeVersion1("Game Design Workshop by Phoenix Thomson", 1280, 720);
            game.Start();
        }
    }
}
