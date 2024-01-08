using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.Particles;
using GameDesignLearningAppPrototype.Scripts.Platformer.Tiles;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Managers
{
    public class TileManager
    {
        public bool tilesModified = false;
        private Tile[,] tiles = new Tile[100, 10];
        public TileManager()
        {
            for (int i = 0; i < TileType.GetValues(typeof(TileType)).Length; i++) //basic test scene
            {
                TrySetTile(i, 3, (TileType)i);
                if (tiles[i, 3] is Flag)
                {
                    Flag flag = (Flag)tiles[i, 3];
                    //flag.SetState(Flag.FlagState.POLE);
                    TrySetTile(i, 4, (TileType)i);
                    TrySetTile(i, 6, (TileType)i);
                    TrySetTile(i, 2, (TileType)i);
                    TrySetTile(i, 5, (TileType)i);
                    TrySetTile(i, 7, (TileType)i);
                }

                if (tiles[i, 3] is Water)
                {
                    Water water = (Water)tiles[i, 3];
                    //flag.SetState(Flag.FlagState.POLE);
                    TrySetTile(i, 4, (TileType)i);
                    TrySetTile(i, 6, (TileType)i);
                    TrySetTile(i, 2, (TileType)i);
                    TrySetTile(i, 5, (TileType)i);
                    TrySetTile(i, 7, (TileType)i);
                    TrySetTile(i+2, 7, (TileType)i);
                    TrySetTile(i-1, 7, (TileType)i);
                    TrySetTile(i-2, 7, (TileType)i);
                    TrySetTile(i+1, 7, (TileType)i);
                }
            }
        }

        float[] verticies;

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            
            foreach (Tile tile in tiles)
            {
                if (tile == null) continue;
                tile.Update(gameWindow, gameTime);
            }
        }

        public void UpdateSurroundingState(int X, int Y)
        {
            //update state on all surrounding tiles with false as parameter
            Tile currentTile = this.GetTile(X - 1, Y);
            if (currentTile != null) currentTile.UpdateState(false);
            currentTile = this.GetTile(X + 1, Y);
            if (currentTile != null) currentTile.UpdateState(false);
            currentTile = this.GetTile(X, Y - 1);
            if (currentTile != null) currentTile.UpdateState(false);
            currentTile = this.GetTile(X, Y + 1);
            if (currentTile != null) currentTile.UpdateState(false);
        }

        public void UpdateSurroundingState(Tile tile)
        {
            UpdateSurroundingState(tile.X, tile.Y);
        }

        public float[] CombineVertexData()
        {
            List<float> listVerticies = new List<float>();
            listVerticies.Clear();
            foreach (Tile tile in tiles)
            {
                if (tile == null) continue;
                float[] vertexData = tile.AssembleVertexData();
                foreach (float vertex in vertexData)
                {
                    listVerticies.Add(vertex);
                }
            }
            verticies = listVerticies.ToArray();
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

        //make new one using enum

        private void SetDefaultTile(int x, int y, TileType tileType)
        {
            tiles[x, y] = new Tile(this, x, y, tileType);
            tilesModified = true;
            //add line to see if indexes (vet length) has changed
        }

        private void SetTile(int x, int y, TileType tileType)
        {
            tilesModified = true;
            int tileID = tileType.GetTileID();
            if (tileID <= 0)
            {
                //remove tile
                tiles[x, y] = null;
                //update surrounding tiles
                return;
            }
            else if (tileID > 0 && !tileType.HasCustomProperties())
            {
                SetDefaultTile(x, y, tileType);
            } else
            {
                switch (tileType)
                {
                    case TileType.FLAG:
                        tiles[x, y] = new Flag(this, x, y);
                        break;
                    case TileType.SNOWMAN:
                        tiles[x, y] = new Snowman(this, x, y);
                        break;
                    case TileType.COIN:
                        tiles[x, y] = new Coin(this, x, y);
                        break;
                    case TileType.WATER:
                        tiles[x, y] = new Water(this, x, y);
                        break;
                    case TileType.CACTUS_PLANT:
                        tiles[x, y] = new CactusPlant(this, x, y);
                        break;
                    case TileType.TREE_PLANT:
                        tiles[x, y] = new TreePlant(this, x, y);
                        break;
                    default:
                        //error tile maybe?
                        break;
                }
            }
            tiles[x, y].UpdateState(true);
        }

        private bool WithinBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < tiles.GetLength(0) && y < tiles.GetLength(1);
        }

        public void TrySetTile(int x, int y, TileType tileType)
        {
            if (!WithinBounds(x,y))
            {
                Console.WriteLine($"Error: tried to place tile out of bounds {x}, {y}, {tileType}");
                return;
            }
            SetTile(x, y, tileType);
        }

        public Tile GetTile(int x, int y)
        {
            //check if out of bounds
            if (!WithinBounds(x, y))
            {
                return null;
            }
            return tiles[x, y];
        }

        public TileType GetTileType(int x, int y)
        {
            //check if out of bounds
            if (!WithinBounds(x, y))
            {
                return TileType.AIR;
            }
            if (GetTile(x, y) == null) return TileType.AIR;
            return GetTile(x, y).TileType;
        }
    }
}
