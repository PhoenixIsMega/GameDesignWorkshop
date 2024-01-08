using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;

namespace GameDesignLearningAppPrototype.Scripts
{
    public abstract class EngineBase
    {
        protected string WindowTitle { get; set; } // The title of the game window.
        protected int InitialWindowWidth { get; set; } // The initial width of the game window.
        protected int InitialWindowHeight { get; set; } // The initial height of the game window.

        private GameWindowSettings _gameWindowSettings; // Settings for the game window.
        private NativeWindowSettings _nativeWindowSettings; // Settings for the native window.

        protected GameWindow gameWindow; // The game window instance.

        public EngineBase(string windowTitle, int initialWindowWidth, int initialWindowHeight)
        {
            WindowTitle = windowTitle;
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWindowHeight;

            _nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(initialWindowWidth, initialWindowHeight), // Set the size of the game window.
                Title = windowTitle, // Set the title of the game window.
                API = ContextAPI.OpenGL, // Use OpenGL for the graphics API.
                StartFocused = true, // Start the game window in a focused state.
                StartVisible = false // Start the game window as invisible.
            };

            _gameWindowSettings = new GameWindowSettings()
            {
                RenderFrequency = 60.0, // Set the desired render frequency to 60 FPS.
                UpdateFrequency = 60.0 // Set the desired update frequency to 60 FPS.
            };
        }

        public void Start()
        {
            Initialise(); // Perform any necessary initialization.

            gameWindow = DisplayManager.Instance.CreateWindow(_gameWindowSettings, _nativeWindowSettings); // Create the game window using the specified settings.

            GL.ClearColor(new Color4(0.35f, 0.75f, 0.45f, 1f)); // Set the clear color, only needs to be set once as its constant, will be replaced by background at some point anyway

            GameTime gameTime = new GameTime(); // Create a new GameTime instance.

            gameWindow.Load += LoadContent; // Subscribe to the Load event.
            gameWindow.UpdateFrame += (FrameEventArgs eventArgs) =>
            {
                double time = eventArgs.Time; // Get the elapsed time for the frame.
                gameTime.ElapsedGameTime = TimeSpan.FromMilliseconds(time); // Update the elapsed game time.
                gameTime.TotalGameTime += TimeSpan.FromMilliseconds(time); // Update the total game time.
                Update(gameTime); // Call the Update method of the game.

                //if (!gameWindow.IsFocused)
                //{
                //    return; // Skip further processing if the game window is not focused.
                //}
            };

            gameWindow.RenderFrame += (FrameEventArgs eventArgs) =>
            {
                Render(gameTime); // Call the Render method of the game
                
                gameWindow.SwapBuffers(); // Swap the front and back buffers to display the rendered frame.
               
            };

            gameWindow.Resize += (ResizeEventArgs) =>
            {
                GL.Viewport(0, 0, gameWindow.Size.X, gameWindow.Size.Y); // Update the viewport when the window is resized.
            };

            gameWindow.MouseWheel += (MouseWheelEventArgs e) =>
            {
                OnMouseWheel(e);
            };

            gameWindow.Unload += UnloadContent; // Subscribe to the Unload event.

            gameWindow.IsVisible = true; // Set the game window to be visible.

            //gameWindow.VSync = VSyncMode.Off;


            
            gameWindow.Run(); // Start the game loop.
        }

        protected abstract void Initialise(); // Method to perform initialization tasks.

        protected abstract void LoadContent(); // Method to load game content.

        protected abstract void UnloadContent(); // Method to unload game content.

        protected abstract void Update(GameTime gameTime); // Method to update game logic.

        protected abstract void Render(GameTime gameTime); // Method to render the game.

        protected abstract void OnMouseWheel(MouseWheelEventArgs e);
    }
}
