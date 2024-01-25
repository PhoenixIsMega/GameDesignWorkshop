using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.Players;
using GameDesignLearningAppPrototype.Scripts.Platformer.Tiles;
using OpenTK.Windowing.Desktop;
using System.Collections.Generic;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Managers
{
    public class PlayerManager
    {
        private Player player = new Player();
        public PlayerManager()
        {
            player.Move(0, 0);
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

        public (float, float) GetPlayerPosition()
        {
            return player.GetPosition();
        }

        public (float, float) GetPlayerSize()
        {
            return player.GetSize();
        }

        public float[] getColliderLines()
        {
            List<float> listVerticies = new List<float>();
            listVerticies.Clear();
            if (player == null) return null;
            if (player.GetComponent<BoxCollider>() is null) return null;
            float[] vertexData = player.GetComponent<BoxCollider>().getLines();
            foreach (float vertex in vertexData)
            {
                listVerticies.Add(vertex);
            }
            float[] verticies = listVerticies.ToArray();
            return verticies;
        }
    }
}
