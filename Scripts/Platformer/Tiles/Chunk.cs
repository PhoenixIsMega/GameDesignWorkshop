using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Tiles
{
    public class Chunk
    {
        private readonly ClassManager classManager;
        private static readonly int chunkSize = 10;
        public bool tilesModified = false;
        private Tile[,] tiles = new Tile[chunkSize, chunkSize];
        public Chunk(ClassManager classManager)
        {
            this.classManager = classManager;
        }

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            foreach (Tile tile in tiles)
            {
                if (tile == null) continue;
                tile.Update(gameWindow, gameTime);
            }
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

        public Tile[,] Tiles => tiles;
        public static int ChunkSize => chunkSize;
    }
}
