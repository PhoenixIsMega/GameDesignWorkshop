using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Components;
using GameDesignLearningAppPrototype.Scripts.Platformer.Players;
using GameDesignLearningAppPrototype.Scripts.Platformer.Tiles;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Managers
{
    public class PlayerManager
    {
        private readonly ClassManager classManager;
        private Player player = null;
        public PlayerManager(ClassManager classManager)
        {
            this.classManager = classManager;
            createPlayer();
        }

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            player.Update(gameWindow, gameTime);
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Chunk chunk = classManager.TileManager.GetNearbyChunkAtCoord(player.getWorldPosition().x, player.getWorldPosition().y, x, y);
                    if (chunk == null) continue;
                    foreach (Tile tile in chunk.Tiles)
                    {
                        if (tile == null) continue;
                        if (tile.GetComponent<BoxCollider>() is null) continue;
                        if (tile.GetComponent<BoxCollider>().detectCollision(player))
                        {
                            player.GetComponent<BoxCollider>().resolveCollision(tile);
                        }
                    }
                }
            }
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
            if (vertexData != null)
            {
                foreach (float vertex in vertexData)
                {
                    listVerticies.Add(vertex);
                }
            }
            float[] verticies = listVerticies.ToArray();
            return verticies;
        }

        public Player createPlayer()
        {
            if (player != null)
            {
                Console.WriteLine("Player already exists");
                return player;
            }
            player = new Player();
            player.Move(0, 0); //move to spawn point
            return player;
        }

        public void destroyPlayer()
        {
            if (player == null)
            {
                Console.WriteLine("Player does not exist");
                return;
            }
            player = null; //maybe dispose of player?
        }

        public Player getPlayer()
        {
            if (player == null)
            {
                Console.WriteLine("Player does not exist");
                return null;
            }
            return player;
        }
    }
}
