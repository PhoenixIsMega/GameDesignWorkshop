using GameDesignWorkshop.game.managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.Scripts.Engine.Managers
{
    class Test
    {
        public static void Main(string[] args)
        {
            GameClassManager gameClassManager = new GameClassManager();
            gameClassManager.GetSoundEffectManager().LoadSound("test", "Assets/sounds/Exit.wav");
            gameClassManager.GetMusicManager().AddToQueue("Assets/sounds/Dmajor.wav");
            gameClassManager.GetMusicManager().AddToQueue("Assets/sounds/Dmajor7.wav");
            if (gameClassManager.GetMusicManager().music == null)
            {
                gameClassManager.GetMusicManager().music = gameClassManager.GetMusicManager().musicQueue.Dequeue();
            }
            gameClassManager.GetMusicManager().PlayMusic();

            Console.ReadLine();
            float timeStart = DateTime.Now.Second;
            float programRuntime = 30.0f;

            while (DateTime.Now.Second < timeStart + programRuntime)
            {
                //game loop mock

            }
        }
    }
}
