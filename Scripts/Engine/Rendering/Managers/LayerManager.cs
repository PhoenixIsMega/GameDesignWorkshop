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
        PostProcessLayer postProcessLayer;
        //ScreenTextLayer screenTextLayer;
        BackgroundLayer backgroundLayer;
        public LayerManager()
        {
            playerLayer = new PlayerLayer("Assets/Shaders/ViewportTextureWithTransparency.shader");
            particleLayer = new ParticleLayer("Assets/Shaders/SingleColor.shader");
            tileLayer = new TileLayer("Assets/Shaders/Tile.shader");
            gizmoLayer = new GizmoLayer("Assets/Shaders/SingleColor.shader");
            cursorLayer = new CursorLayer("Assets/Shaders/Cursor.shader");
            postProcessLayer = new PostProcessLayer("Assets/Shaders/FrameBufferBase.shader");
            //screenTextLayer = new ScreenTextLayer("Assets/Shaders/ScreenText.shader");
            backgroundLayer = new BackgroundLayer("Assets/Shaders/Background.shader");
        }

        public void LoadContent(PlayerManager playerManager, ParticleManager particleManager, TileManager tileManager, CursorManager cursorManager, BackgroundManager backgroundManager, GizmoManager gizmoManager)
        {
            postProcessLayer.LoadContent(null);
            backgroundLayer.LoadContent(backgroundManager.AssembleVertexData());
            backgroundLayer.UpdateIndexBuffer(4);
            tileLayer.UpdateIndexBuffer(tileManager.CountTiles());
            tileLayer.LoadContent(tileManager.CombineVertexData());
            playerLayer.UpdateIndexBuffer(playerManager.CountTiles());
            playerLayer.LoadContent(playerManager.AssembleVertexData());
            particleLayer.UpdateIndexBuffer(particleManager.CountTiles());
            particleLayer.LoadContent(particleManager.CombineVertexData());
            gizmoLayer.LoadContent(gizmoManager.CombineVertexData());
            //screenTextLayer.LoadContent(null);
            cursorLayer.UpdateIndexBuffer(cursorManager.CountTiles());
            cursorLayer.LoadContent(cursorManager.AssembleVertexData());
        }

        public void Render(PlayerManager playerManager, ParticleManager particleManager, TileManager tileManager, CursorManager cursorManager, BackgroundManager backgroundManager, GizmoManager gizmoManager)
        {
            postProcessLayer.BindFrameBuffer();
            gizmoLayer.Render(gizmoManager.CombineVertexData());
            tileLayer.Render(tileManager.CombineVertexData());
            tileLayer.UpdateIndexBuffer(tileManager.CountTiles());
            playerLayer.Render(playerManager.AssembleVertexData());
            particleLayer.UpdateIndexBuffer(particleManager.CountTiles());
            particleLayer.Render(particleManager.CombineVertexData());
            //screenTextLayer.Render(null);//text go here
            backgroundLayer.Render(backgroundManager.AssembleVertexData());
            postProcessLayer.UnbindFrameBuffer();
            postProcessLayer.Render(null);
            cursorLayer.Render(cursorManager.AssembleVertexData());
        }
    }
}
