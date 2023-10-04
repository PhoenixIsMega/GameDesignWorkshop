using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers;
using GameDesignLearningAppPrototype.Scripts.Menu.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Tiles;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers
{
    class LayerManager
    {
        PlayerLayer playerLayer;
        ParticleLayer particleLayer;
        TileLayer tileLayer;
        GizmoLayer gizmoLayer;
        CursorLayer cursorLayer;
        public LayerManager()
        {
            playerLayer = new PlayerLayer("Assets/Shaders/ViewportTextureWithTransparency.shader");
            particleLayer = new ParticleLayer("Assets/Shaders/SingleColor.shader");
            tileLayer = new TileLayer("Assets/Shaders/Tile.shader");
            gizmoLayer = new GizmoLayer("Assets/Shaders/SingleColor.shader");
            cursorLayer = new CursorLayer("Assets/Shaders/Cursor.shader");
        }

        public void LoadContent(PlayerManager playerManager, ParticleManager particleManager, TileManager tileManager, CursorManager cursorManager)
        {
            tileLayer.UpdateIndexBuffer(tileManager.CountTiles());
            tileLayer.LoadContent(tileManager.AssembleVertexData());
            playerLayer.UpdateIndexBuffer(playerManager.CountTiles());
            playerLayer.LoadContent(playerManager.AssembleVertexData());
            particleLayer.UpdateIndexBuffer(particleManager.CountTiles());
            particleLayer.LoadContent(particleManager.AssembleVertexData());
            gizmoLayer.LoadContent(null);
            cursorLayer.UpdateIndexBuffer(cursorManager.CountTiles());
            cursorLayer.LoadContent(cursorManager.AssembleVertexData());
        }

        public void Render(PlayerManager playerManager, ParticleManager particleManager, TileManager tileManager, CursorManager cursorManager)
        {
            tileLayer.Render(tileManager.AssembleVertexData());
            playerLayer.Render(playerManager.AssembleVertexData());
            particleLayer.UpdateIndexBuffer(particleManager.CountTiles());
            particleLayer.Render(particleManager.AssembleVertexData());
            // After an OpenGL function call, check for errors
            ErrorCode errorCode = GL.GetError();
            if (errorCode != ErrorCode.NoError)
            {
                Console.WriteLine($"OpenGL Error: {errorCode}");
                // You can handle the error here, log it, or take other appropriate actions.
            }
            gizmoLayer.Render(null);
            cursorLayer.Render(cursorManager.AssembleVertexData());
        }
    }
}
