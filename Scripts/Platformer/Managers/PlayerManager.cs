using GameDesignLearningAppPrototype.Scripts.Engine;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;
using GameDesignLearningAppPrototype.Scripts.Platformer.Players;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Managers
{
    class PlayerManager
    {
        private Player player = new Player();
        public PlayerManager()
        {
            player.Move(500, 200);
        }

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            player.Update(gameWindow, gameTime);
        }

        public float[] AssembleVertexData()
        {
            return player.AssembleVertexData();
        }

        public int CountTiles()
        {
            return 1;
        }

        public (float, float) getPlayerPosition()
        {
            return player.getPosition();
        }

        public (float, float) getPlayerSize()
        {
            return player.getSize();
        }
    }
}
