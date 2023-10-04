using GameDesignLearningAppPrototype.Scripts.Platformer.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Managers
{
    class TileManager
    {
        public Tile[,] tiles = new Tile[30, 4];
        public TileManager()
        {
            for(int i = 0; i<30; i++)
            {
                SetTile(i, 0, 43);
                SetTile(i, 1, 143);
            }
        }

        public float[] AssembleVertexData()
        {
            List<float> listVerticies = new List<float>();
            foreach (Tile tile in tiles)
            {
                if (tile == null) continue;
                float[] vertexData = tile.AssembleVertexData();
                foreach (float value in vertexData)
                {
                    listVerticies.Add(value);
                }
            }
            float[] verticies = listVerticies.ToArray();
            return verticies;
        }

        public int CountTiles()
        {
            int count = 0;
            foreach (Tile tile in tiles)
            {
                if (tile == null) continue;
                count++;
            }
            return count;
        }

        public void SetTile(int x, int y, int tileID)
        {
            tiles[x, y] = new Tile(tileID, 90*x, 90*y);
        }
    }
}
