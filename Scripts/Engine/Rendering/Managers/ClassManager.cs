using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers.RenderLayerManagers;
using GameDesignLearningAppPrototype.Scripts.Engine.Utility;
using GameDesignLearningAppPrototype.Scripts.Menu.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers
{
    public class ClassManager
    {
        //game
        public EngineBase game;

        //managers
        private readonly TileManager tileManager;
        private readonly ParticleManager particleManager;
        private readonly PlayerManager playerManager;
        private readonly CursorManager cursorManager;
        private readonly BackgroundManager backgroundManager;
        private readonly GizmoManager gizmoManager;
        private readonly CameraManager cameraManager;

        private readonly RandomNumberGenerator randomNumberGenerator;

        private readonly LayerManager layerManager;

        public ClassManager(EngineBase game)
        {
            this.game = game;
            playerManager = new PlayerManager(this);
            layerManager = new LayerManager(this);
            particleManager = new ParticleManager(this);
            tileManager = new TileManager(this);
            cursorManager = new CursorManager(this);
            backgroundManager = new BackgroundManager(this);
            gizmoManager = new GizmoManager(this);
            cameraManager = new CameraManager(this);
            randomNumberGenerator = new RandomNumberGenerator(this);
        }

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            playerManager.Update(gameWindow, gameTime);
            particleManager.Update(gameWindow, gameTime);
            cursorManager.Update(gameWindow, gameTime);
            tileManager.Update(gameWindow, gameTime);
            cameraManager.Update(gameWindow, gameTime);
        }

        public TileManager TileManager => tileManager;
        public ParticleManager ParticleManager => particleManager;
        public PlayerManager PlayerManager => playerManager;
        public CursorManager CursorManager => cursorManager;
        public BackgroundManager BackgroundManager => backgroundManager;
        public GizmoManager GizmoManager => gizmoManager;
        public LayerManager LayerManager => layerManager;
        public CameraManager CameraManager => cameraManager;
        public RandomNumberGenerator RandomNumberGenerator => randomNumberGenerator;

    }
}
