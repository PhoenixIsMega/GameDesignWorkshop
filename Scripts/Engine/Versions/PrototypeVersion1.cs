using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers.RenderLayerManagers;
using GameDesignLearningAppPrototype.Scripts.Menu.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Tiles;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Versions
{
    internal class PrototypeVersion1 : EngineBase
    {
        public PrototypeVersion1(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
        {
        }

        private TileManager tileManager;
        private ParticleManager particleManager;
        private PlayerManager playerManager;
        private CursorManager cursorManager;
        private BackgroundManager backgroundManager;
        private GizmoManager gizmoManager;

        private LayerManager layerManager;

        //make camera manager not a singleton
        protected override void Initialise()
        {
            Console.WriteLine("Initalise");
            playerManager = new PlayerManager();
            layerManager = new LayerManager();
            particleManager = new ParticleManager(this);
            tileManager = new TileManager();
            cursorManager = new CursorManager(tileManager);
            backgroundManager = new BackgroundManager();
            gizmoManager = new GizmoManager(tileManager , playerManager);
        }

        protected override void LoadContent()
        {
            Console.WriteLine("Load");
            layerManager.LoadContent(playerManager, particleManager, tileManager, cursorManager, backgroundManager, gizmoManager);
            gameWindow.CursorState = OpenTK.Windowing.Common.CursorState.Hidden;
        }


        protected override void Update(GameTime gameTime)
        {
            KeyboardState input = gameWindow.KeyboardState;
            if (input.IsKeyDown(Keys.Escape))
            {
                gameWindow.Close();
            }

            playerManager.Update(gameWindow, gameTime);
            particleManager.Update(gameWindow, gameTime);
            cursorManager.Update(gameWindow, gameTime);
            tileManager.Update(gameWindow, gameTime);
            CameraManager.Instance.Update(gameWindow, gameTime);

            //please update this later
            CameraManager.Instance.SetCameraPosition(getPlayerLocation().Item1 - gameWindow.Size.X / 2 + playerManager.GetPlayerSize().Item1 / 2, getPlayerLocation().Item2 - gameWindow.Size.Y / 4 + playerManager.GetPlayerSize().Item2 / 2);
        }

        float scale = 1;

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            //Console.WriteLine("Mouse Wheel");

            //ZOOM
            /*
            float delta = e.OffsetY;
            scale *= 1 + (delta / 30);
            //Console.WriteLine(scale);
            CameraManager.Instance.SetCameraScale(scale);
            */

            cursorManager.cursor.tileType = cursorManager.cursor.tileType + (int)e.OffsetY;
            if (cursorManager.cursor.tileType < TileType.AIR)
            {
                cursorManager.cursor.tileType = TileType.AIR;
            } else if (cursorManager.cursor.tileType > TileType.WATERFALL)
            {
                cursorManager.cursor.tileType = TileType.WATERFALL;
            }
        }

        protected override void Render(GameTime gameTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); // Clear the color 

            layerManager.Render(playerManager, particleManager, tileManager, cursorManager, backgroundManager, gizmoManager);


        }

        protected override void UnloadContent()
        {
            //unload and dispose of everything here
            Console.WriteLine("Unload");
        }

        public (float, float) getPlayerLocation()
        {
            return playerManager.GetPlayerPosition();
        }
    }
}
