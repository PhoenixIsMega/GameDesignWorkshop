using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
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
        private readonly ClassManager classManager;
        public bool tilesModified = false;
        private readonly int chunkSize;
        private static readonly int chunksX = 10;
        private static readonly int chunksY = 10;
        private Chunk[,] chunks = new Chunk[chunksX, chunksY];
        public TileManager(ClassManager classManager)
        {
            this.classManager = classManager;
            this.chunkSize = Chunk.ChunkSize;
            for (int i= 0; i < 10; i++)
            {
                TrySetTile(i, 0, TileType.GRASS);
            }
            for (int i = 10; i < TileType.GetValues(typeof(TileType)).Length+10; i++) //basic test scene
            {
                TrySetTile(i, 0, (TileType)i);
            }
        }

        float[] verticies;

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            foreach (Chunk chunk in chunks)
            {
                if (chunk == null) continue;
                chunk.Update(gameWindow, gameTime);
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
            foreach (Chunk chunk in chunks)
            {
                if (chunk == null) continue;
                foreach (Tile tile in chunk.Tiles)
                {
                    if (tile == null) continue;
                    float[] vertexData = tile.AssembleVertexData();
                    foreach (float vertex in vertexData)
                    {
                        listVerticies.Add(vertex);
                    }
                }
            }
            verticies = listVerticies.ToArray();
            return verticies;
        }

        public int CountTiles()
        {
            int count = 0;
            foreach (Chunk chunk in chunks)
            {
                if (chunk == null) continue;
                foreach (Tile tile in chunk.Tiles)
                {
                    if (tile == null) continue;
                    count++;
                }
            }
            return count;
        }

        public Chunk GetChunkAtCoord(float x, float y)
        {
            return GetChunkAtTile((int)(x / Tile.TILE_SIZE), (int)(y / Tile.TILE_SIZE));
        }

        public Chunk GetNearbyChunkAtCoord(float x, float y, int offsetX, int offsetY)
        {
            return GetChunkAtTile((int)(x / Tile.TILE_SIZE) + offsetX, (int)(y / Tile.TILE_SIZE) + offsetY);
        }

        private Chunk GetChunkAtTile(int x, int y)
        {
            if (x < 0 || y < 0 || x >= chunksX * chunkSize || y >= chunksY * chunkSize)
            {
                return null;
            }
            Chunk chunk = chunks[x / chunkSize, y / chunkSize];
            if (chunk == null)
            {
                chunk = new Chunk(classManager);
                chunks[x / chunkSize, y / chunkSize] = chunk;
            }
            return chunk;
        }

        //make new one using enum
        private void SetTileObject(int x, int y, Tile tile)
        {
            GetChunkAtTile(x, y).Tiles[x % chunkSize, y % chunkSize] = tile;
        }

        private void SetDefaultTile(int x, int y, TileType tileType)
        {
            SetTileObject(x, y, new Tile(this, x, y, tileType));
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
                SetTileObject(x, y, null);
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
                        SetTileObject(x, y, new Flag(this, x, y));
                        break;
                    case TileType.SNOWMAN:
                        SetTileObject(x, y, new Snowman(this, x, y));
                        break;
                    case TileType.COIN:
                        SetTileObject(x, y, new Coin(this, x, y));
                        break;
                    case TileType.WATER:
                        SetTileObject(x, y, new Water(this, x, y));
                        break;
                    case TileType.CACTUS_PLANT:
                        SetTileObject(x, y, new CactusPlant(this, x, y));
                        break;
                    case TileType.TREE_PLANT:
                        SetTileObject(x, y, new TreePlant(this, x, y));
                        break;
                    case TileType.WATERFALL:
                        SetTileObject(x, y, new Waterfall(this, x, y));
                        break;
                    case TileType.GRASS:
                        SetTileObject(x, y, new GrassBlock(this, x, y));
                        break;
                    default:
                        //error tile maybe?
                        break;
                }
            }
            GetTile(x, y).UpdateState(true);
        }

        private bool WithinBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < chunks.GetLength(0)*chunkSize && y < chunks.GetLength(1)*chunkSize;
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
            return GetChunkAtTile(x, y).Tiles[x% chunkSize, y% chunkSize];
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

        public float[] getColliderLines()
        {
            List<float> listVerticies = new List<float>();
            listVerticies.Clear();
            foreach (Chunk chunk in chunks)
            {
                if (chunk == null) continue;
                foreach (Tile tile in chunk.Tiles)
                {
                    if (tile == null) continue;
                    if (tile.GetComponent<BoxCollider>() is null) continue;
                    float[] vertexData = tile.GetComponent<BoxCollider>().getLines();
                    if (vertexData == null) continue;
                    foreach (float vertex in vertexData)
                    {
                        listVerticies.Add(vertex);
                    }
                }
            }
            verticies = listVerticies.ToArray();
            return verticies;
        }
    }
}
