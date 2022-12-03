using GameDesignWorkshop.management;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using System;

namespace GameDesignWorkshop
{
    public abstract class Application
    {
        protected string WindowTitle { get; set; }
        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set; }

        private GameWindowSettings _gameWindowSettings = GameWindowSettings.Default;
        private NativeWindowSettings _nativeWindowSettings = NativeWindowSettings.Default;

        public Application(string windowTitle, int initialWindowWidth, int initialWindowHeight)
        {
            this.WindowTitle = windowTitle;
            this.InitialWindowWidth = initialWindowWidth;
            this.InitialWindowHeight = initialWindowHeight;
            this._nativeWindowSettings.Size = new Vector2i(initialWindowWidth, initialWindowHeight);
            this._nativeWindowSettings.Title = windowTitle;
            this._nativeWindowSettings.API = ContextAPI.OpenGL;

            this._gameWindowSettings.RenderFrequency = 60.0; //60 fps
            this._gameWindowSettings.UpdateFrequency = 60.0;
        }

        public void Start()
        {
            Initialise();

            GameWindow gameWindow = DisplayManager.Instance.CreateWindow(_gameWindowSettings, _nativeWindowSettings);
            GameTime gameTime = new GameTime();
            gameWindow.Load += LoadContent;
            gameWindow.UpdateFrame += (FrameEventArgs eventArgs) =>
            {
                double time = eventArgs.Time;
                gameTime.ElapsedGameTime = TimeSpan.FromMilliseconds(time);
                gameTime.TotalGameTime += TimeSpan.FromMilliseconds(time);
                Update(gameTime);
            };
            gameWindow.RenderFrame += (FrameEventArgs eventArgs) =>
            {
                Render(gameTime);
                gameWindow.SwapBuffers(); // could move
            };
            gameWindow.Resize += (ResizeEventArgs) =>
            {
                GL.Viewport(0, 0, gameWindow.Size.X, gameWindow.Size.Y);
            };
            //gameWindow.Cursor = new MouseCursor(0, 0, 10, 1, new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, });
            gameWindow.Run();
        }

        protected abstract void Initialise();

        protected abstract void LoadContent();

        protected abstract void Update(GameTime gameTime);

        protected abstract void Render(GameTime gameTime);
    }
}
