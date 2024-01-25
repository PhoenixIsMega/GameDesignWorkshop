using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers
{
    public class GizmoManager
    {
        TileManager tileManager;
        PlayerManager playerManager;
        public GizmoManager(TileManager tileManager, PlayerManager playerManager)
        {
            this.tileManager = tileManager;
            this.playerManager = playerManager;
        }

        int lineCount = 0;

        public float[] CombineVertexData()
        {
            lineCount = 0;
            List<float> listVerticies = new List<float>();

            float[] tileLines = tileManager.getColliderLines();
            float[] playerLines = playerManager.getColliderLines();

            foreach (float vertex in tileLines)
            {
                listVerticies.Add(vertex);
                lineCount++;
            }

            foreach (float vertex in playerLines)
            {
                listVerticies.Add(vertex);
                lineCount++;
            }
            //Console.WriteLine(lineCount);
            float[] verticies = listVerticies.ToArray();
            return verticies;
        }

        public int GetLineCount()
        {
            return lineCount;
        }
    }
}
