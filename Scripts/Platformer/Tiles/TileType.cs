using GameDesignLearningAppPrototype.Scripts.Platformer.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Tiles
{
    public enum TileType
    {
        AIR,
        SMALL_ZERO,
        SMALL_ONE,
        SMALL_TWO,
        SMALL_THREE,
        SMALL_FOUR,
        SMALL_FIVE,
        SMALL_SIX,
        SMALL_SEVEN,
        SMALL_EIGHT,
        SMALL_NINE,
        LARGE_ZERO,
        LARGE_ONE,
        LARGE_TWO,
        LARGE_THREE,
        LARGE_FOUR,
        LARGE_FIVE,
        LARGE_SIX,
        LARGE_SEVEN,
        LARGE_EIGHT,
        LARGE_NINE,
        GRASS,
        SNOW_PLANT,
        SNOWMAN,
        WOOD_PLATFORM,
        STONE_PLATFORM,
        BUTTON,
        DOOR,
        COIN,
        CLOUD,
        WHITE_BLOCK,
        DOT,
        TIMES,
        PERCENT,
        PLANT,
        LONG_PLANT,
        TREE_PLANT,
        CACTUS_PLANT,
        RED_MUSHROOM,
        BROWN_MUSHROOM,
        DOOR_WITH_WINDOW,
        FLAG,
        PIPE,
        TREE_TRUNK,
        LEAVES,
        CRATE,
        HIGH_FENCE,
        LOW_FENCE,
        SPRING_PAD,
        ROPE,
        SIGN,
        ARROW,
        LEVER,
        GEM,
        SPIKE,
        LADDER,
        MUSHROOM,
        DIRT,
        PATH,
        SNOW_GRASS,
        WOOD,
        KEY,
        LOCK,
        BLOCK,
        EXCLAMATION_BLOCK,
        O_BLOCK,
        WATER,
        WATERFALL
    }

    public static class TileExtensions
    {
        private static Dictionary<TileType, int> tileID = new Dictionary<TileType, int>()
        {
            { TileType.AIR, 0 },
            { TileType.SMALL_ZERO, 1 },
            { TileType.SMALL_ONE, 2 },
            { TileType.SMALL_TWO, 3 },
            { TileType.SMALL_THREE, 4 },
            { TileType.SMALL_FOUR, 5 },
            { TileType.SMALL_FIVE, 6 },
            { TileType.SMALL_SIX, 7 },
            { TileType.SMALL_SEVEN, 8 },
            { TileType.SMALL_EIGHT, 9 },
            { TileType.SMALL_NINE, 10 },
            { TileType.LARGE_ZERO, 11 },
            { TileType.LARGE_ONE, 12 },
            { TileType.LARGE_TWO, 13 },
            { TileType.LARGE_THREE, 14 },
            { TileType.LARGE_FOUR, 15 },
            { TileType.LARGE_FIVE, 16 },
            { TileType.LARGE_SIX, 17 },
            { TileType.LARGE_SEVEN, 18 },
            { TileType.LARGE_EIGHT, 19 },
            { TileType.LARGE_NINE, 20 },
            { TileType.GRASS, 161 },
            { TileType.SNOW_PLANT, 25 },
            { TileType.SNOWMAN, 26 },
            { TileType.WOOD_PLATFORM, 27 },
            { TileType.STONE_PLATFORM, 28 },
            { TileType.BUTTON, 29 },
            { TileType.DOOR, 31 },
            { TileType.COIN, 32 },
            { TileType.CLOUD, 34 },
            { TileType.WHITE_BLOCK, 37 },
            { TileType.DOT, 38 },
            { TileType.TIMES, 39 },
            { TileType.PERCENT, 40 },
            { TileType.PLANT, 45 },
            { TileType.LONG_PLANT, 46 },
            { TileType.TREE_PLANT, 47 },
            { TileType.CACTUS_PLANT, 48 },
            { TileType.RED_MUSHROOM, 49 },
            { TileType.BROWN_MUSHROOM, 50 },
            { TileType.DOOR_WITH_WINDOW, 51 },
            { TileType.FLAG, 72 },
            { TileType.PIPE, 53 },
            { TileType.TREE_TRUNK, 57 },
            { TileType.LEAVES, 177 },
            { TileType.CRATE, 65 },
            { TileType.HIGH_FENCE, 66 },
            { TileType.LOW_FENCE, 67 },
            { TileType.SPRING_PAD, 68 },
            { TileType.ROPE, 70 },
            { TileType.SIGN, 85 },
            { TileType.ARROW, 88 },
            { TileType.LEVER, 105 },
            { TileType.GEM, 108 },
            { TileType.SPIKE, 109 },
            { TileType.LADDER, 112 },
            { TileType.MUSHROOM, 113 },
            { TileType.DIRT, 167 },
            { TileType.PATH, 121 },
            { TileType.SNOW_GRASS, 81 },
            { TileType.WOOD, 147 },
            { TileType.KEY, 148 },
            { TileType.LOCK, 149 },
            { TileType.BLOCK, 170 },
            { TileType.EXCLAMATION_BLOCK, 171 },
            { TileType.O_BLOCK, 172 },
            { TileType.WATER, 114 },
            { TileType.WATERFALL, 155 }
        };


        public static int GetTileID(this TileType tileType)
        {
            return tileID.GetValueOrDefault(tileType);
        }

        public static bool HasCustomProperties(this TileType tileType)
        {
            switch (tileType)
            {
                case TileType.SNOWMAN:
                case TileType.COIN:
                case TileType.TREE_PLANT:
                case TileType.CACTUS_PLANT:
                case TileType.FLAG:
                case TileType.WATER:
                case TileType.WATERFALL:
                    return true;
                default:
                    return false;
            }
        }

        /*public static bool IsAnimated(this TileType tileType)
        {
            switch (tileType)
            {
                case TileType.COIN:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsButton(this TileType tileType)
        {
            switch (tileType)
            {
                case TileType.BUTTON:
                    return true;
                default:
                    return false;
            }
        }*/
    }
}
