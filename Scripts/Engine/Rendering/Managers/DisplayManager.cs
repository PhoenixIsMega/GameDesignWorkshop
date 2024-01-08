using OpenTK.Windowing.Desktop;
using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers
{
    public sealed class DisplayManager
    {
        private static readonly Lazy<DisplayManager> instance = new Lazy<DisplayManager>(() => new DisplayManager()); // Lazy<DisplayManager> instance for lazy initialization.
        public GameWindow GameWindow; // Represents the game window.

        public static DisplayManager Instance => instance.Value; // Property to access the singleton instance of DisplayManager.

        private DisplayManager()
        {
            // Private constructor to prevent external instantiation.
        }

        public unsafe GameWindow CreateWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        {
            GameWindow = new GameWindow(gameWindowSettings, nativeWindowSettings); // Create a new game window with the provided settings.
            GameWindow.CenterWindow(); // Center the game window on the screen.
            return GameWindow; // Return the created game window.
        }
    }
}
