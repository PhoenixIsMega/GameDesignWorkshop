using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Tiles
{
    class CactusPlant : Tile
    {
        public CactusPlant(TileManager tileManager, int x, int y) : base(tileManager, x, y, TileType.CACTUS_PLANT)
        {
            //quad.Width = 120.0f;
            //quad.Height = 120.0f;
            transform.ScaleY = 2.5f;
            transform.ScaleX = 2.5f;
        }
    }
}
