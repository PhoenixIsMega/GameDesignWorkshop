using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers;
using GameDesignLearningAppPrototype.Scripts.Menu.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using OpenTK.Graphics.OpenGL;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers.RenderLayerManagers
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
            tileLayer.LoadContent(tileManager.CombineVertexData());
            playerLayer.UpdateIndexBuffer(playerManager.CountTiles());
            playerLayer.LoadContent(playerManager.AssembleVertexData());
            particleLayer.UpdateIndexBuffer(particleManager.CountTiles());
            particleLayer.LoadContent(particleManager.CombineVertexData());
            gizmoLayer.LoadContent(null);
            cursorLayer.UpdateIndexBuffer(cursorManager.CountTiles());
            cursorLayer.LoadContent(cursorManager.AssembleVertexData());
        }

        public void Render(PlayerManager playerManager, ParticleManager particleManager, TileManager tileManager, CursorManager cursorManager)
        {
            tileLayer.Render(tileManager.CombineVertexData());
            tileLayer.UpdateIndexBuffer(tileManager.CountTiles());
            playerLayer.Render(playerManager.AssembleVertexData());
            particleLayer.UpdateIndexBuffer(particleManager.CountTiles());
            particleLayer.Render(particleManager.CombineVertexData());
            gizmoLayer.Render(null);
            cursorLayer.Render(cursorManager.AssembleVertexData());
        }
    }
}
