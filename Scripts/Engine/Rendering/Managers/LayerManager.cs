using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers;
using GameDesignLearningAppPrototype.Scripts.Menu.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using OpenTK.Graphics.OpenGL;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers.RenderLayerManagers
{
    public class LayerManager
    {
        private readonly ClassManager classManager;
        private readonly PlayerLayer playerLayer;
        private readonly ParticleLayer particleLayer;
        private readonly TileLayer tileLayer;
        private readonly GizmoLayer gizmoLayer;
        private readonly CursorLayer cursorLayer;
        private readonly PostProcessLayer postProcessLayer;
        //ScreenTextLayer screenTextLayer;
        private readonly BackgroundLayer backgroundLayer;
        public LayerManager(ClassManager classManager)
        {
            this.classManager = classManager;
            playerLayer = new PlayerLayer(classManager, "Assets/Shaders/ViewportTextureWithTransparency.shader");
            particleLayer = new ParticleLayer(classManager, "Assets/Shaders/MultiColor.shader");
            tileLayer = new TileLayer(classManager, "Assets/Shaders/Tile.shader");
            gizmoLayer = new GizmoLayer(classManager, "Assets/Shaders/SingleColor.shader");
            cursorLayer = new CursorLayer("Assets/Shaders/Cursor.shader");
            postProcessLayer = new PostProcessLayer("Assets/Shaders/FrameBufferBase.shader");
            //screenTextLayer = new ScreenTextLayer("Assets/Shaders/ScreenText.shader");
            backgroundLayer = new BackgroundLayer(classManager, "Assets/Shaders/Background.shader");
        }

        public void LoadContent()
        {
            postProcessLayer.LoadContent(null);
            backgroundLayer.LoadContent(classManager.BackgroundManager.AssembleVertexData());
            backgroundLayer.UpdateIndexBuffer(4);
            tileLayer.UpdateIndexBuffer(classManager.TileManager.CountTiles());
            tileLayer.LoadContent(classManager.TileManager.CombineVertexData());
            playerLayer.UpdateIndexBuffer(classManager.PlayerManager.CountTiles());
            playerLayer.LoadContent(classManager.PlayerManager.AssembleVertexData());
            particleLayer.UpdateIndexBuffer(classManager.ParticleManager.CountTiles());
            particleLayer.LoadContent(classManager.ParticleManager.CombineVertexData());
            gizmoLayer.LoadContent(classManager.GizmoManager.CombineVertexData());
            //screenTextLayer.LoadContent(null);
            cursorLayer.UpdateIndexBuffer(classManager.CursorManager.CountTiles());
            cursorLayer.LoadContent(classManager.CursorManager.AssembleVertexData());
        }

        public void Render(bool renderWireframe)
        {
            postProcessLayer.BindFrameBuffer();
            if (renderWireframe) gizmoLayer.Render(classManager.GizmoManager.CombineVertexData());
            tileLayer.Render(classManager.TileManager.CombineVertexData());
            tileLayer.UpdateIndexBuffer(classManager.TileManager.CountTiles());
            playerLayer.Render(classManager.PlayerManager.AssembleVertexData());
            particleLayer.UpdateIndexBuffer(classManager.ParticleManager.CountTiles());
            particleLayer.Render(classManager.ParticleManager.CombineVertexData());
            //screenTextLayer.Render(null);//text go here
            backgroundLayer.Render(classManager.BackgroundManager.AssembleVertexData());
            postProcessLayer.UnbindFrameBuffer();
            postProcessLayer.Render(null);
            cursorLayer.Render(classManager.CursorManager.AssembleVertexData());
        }
    }
}
