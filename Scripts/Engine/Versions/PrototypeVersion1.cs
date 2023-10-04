using GameDesignLearningAppPrototype.Scripts.Engine.Managers;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.BufferObjects;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Layers;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Menu.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using GameDesignLearningAppPrototype.Scripts.Platformer.Tiles;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using ErrorCode = OpenTK.Graphics.OpenGL4.ErrorCode;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Versions
{
    internal class PrototypeVersion1 : Engine
    {
        public PrototypeVersion1(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
        {
        }

        private TileManager tileManager;
        private ParticleManager particleManager;
        private PlayerManager playerManager;
        private CursorManager cursorManager;

        private LayerManager layerManager;

        //private CameraManager cameraManager;

        //OnEnable
        protected override void Initialise()
        {
            //Console.WriteLine("Initalise");
            playerManager = new PlayerManager();
            layerManager = new LayerManager();
            particleManager = new ParticleManager();
            tileManager = new TileManager();
            cursorManager = new CursorManager();
            //cameraManager = new CameraManager();
        }

        //runs once upon load? use to load in textures i think Occurs before the window is displayed for the first time.

        protected override void LoadContent()
        {
            //Console.WriteLine("Load");
            layerManager.LoadContent(playerManager, particleManager, tileManager, cursorManager);
            gameWindow.CursorState = OpenTK.Windowing.Common.CursorState.Hidden;
        }

        protected override void Update(GameTime gameTime)//text
        {
            //Console.WriteLine("Update");
            playerManager.Update(gameWindow, gameTime);
            particleManager.Update(gameWindow, gameTime);
            cursorManager.Update(gameWindow, gameTime);
            //CameraManager.Instance.Update(gameWindow, gameTime);

            //please for the love of god update this
            CameraManager.Instance.setCameraPosition(playerManager.getPlayerPosition().Item1 - gameWindow.Size.X/2 + playerManager.getPlayerSize().Item1/2, playerManager.getPlayerPosition().Item2 - gameWindow.Size.Y/2 + playerManager.getPlayerSize().Item2/2);
        }

        float scale = 1;

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            //Console.WriteLine("Mouse Wheel");
            float delta = e.OffsetY;
            scale *= 1 + (delta / 30);
            Console.WriteLine(scale);
            CameraManager.Instance.setCameraScale(scale);
        }

        protected override void Render(GameTime gameTime)
        {
            //Console.WriteLine("Render");
            GL.Clear(ClearBufferMask.ColorBufferBit); // Clear the color buffer
            ErrorCode errorCode = GL.GetError();
            if (errorCode != ErrorCode.NoError)
            {
                Console.WriteLine($"OpenGL Error DE: {errorCode}");
                // You can handle the error here, log it, or take other appropriate actions.
            }
            GL.ClearColor(new Color4(0.35f, 0.75f, 0.45f, 1f)); // Set the clear color
            ErrorCode errorCode2 = GL.GetError();
            if (errorCode != ErrorCode.NoError)
            {
                Console.WriteLine($"OpenGL Error DE: {errorCode}");
                // You can handle the error here, log it, or take other appropriate actions.
            }
            layerManager.Render(playerManager, particleManager, tileManager, cursorManager);
            ErrorCode errorCode3 = GL.GetError();
            if (errorCode != ErrorCode.NoError)
            {
                Console.WriteLine($"OpenGL Error DE: {errorCode}");
                // You can handle the error here, log it, or take other appropriate actions.
            }

        }

        protected override void UnloadContent()
        {
            //unload and dispose of everything here
            Console.WriteLine("Unload");
        }
    }
}
