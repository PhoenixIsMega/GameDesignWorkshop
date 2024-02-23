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
        private readonly ClassManager classManager;
        private readonly TileManager tileManager;
        private readonly PlayerManager playerManager;
        public GizmoManager(ClassManager classManager)
        {
            this.classManager = classManager;
            this.tileManager = classManager.TileManager;
            this.playerManager = classManager.PlayerManager;
        }

        int lineCount = 0;

        public float[] CombineVertexData()
        {
            lineCount = 0;
            List<float> listVerticies = new List<float>();

            float[] tileLines = tileManager.getColliderLines();
            float[] playerLines = playerManager.getColliderLines();

            if (tileLines != null)
            {
                foreach (float vertex in tileLines)
                {
                    listVerticies.Add(vertex);
                    lineCount++;
                }
            }

            if(playerLines != null)
            {
                foreach (float vertex in playerLines)
                {
                    listVerticies.Add(vertex);
                    lineCount++;
                }
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
