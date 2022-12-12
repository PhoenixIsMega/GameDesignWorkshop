using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using System.Drawing;

namespace GameDesignWorkshop.management
{
    public sealed class DisplayManager
    {
        private static DisplayManager instance = null;
        private static readonly object loc = new object();
        public GameWindow GameWindow;

        public static DisplayManager Instance
        {
            get {
                lock (loc)
                {
                    if(instance is null)
                    {
                        instance = new DisplayManager();
                    }
                    return instance;
                }
            }
        }

        public unsafe GameWindow CreateWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        {
            GameWindow = new GameWindow(gameWindowSettings, nativeWindowSettings);
            //int x, y;
            GameWindow.CenterWindow();
            /*
            MonitorInfo currentMonitor = Monitors.GetMonitorFromWindow(GameWindow.WindowPtr); //pointer = ptr
            Rectangle monitorRectangle = new Rectangle(0, 0, currentMonitor.ClientArea.Size.X, currentMonitor.ClientArea.Size.Y);
            x = (monitorRectangle.Right + monitorRectangle.Left - nativeWindowSettings.Size.X) / 2; //centres app
            y = (monitorRectangle.Bottom + monitorRectangle.Top - nativeWindowSettings.Size.Y) / 2;
            if (x < monitorRectangle.Left)
            {
                x = monitorRectangle.Left;// prevents out of bounds
            }
            if (y < monitorRectangle.Top)
            {
                y = monitorRectangle.Top;
            }
            GameWindow.ClientRectangle = new Box2i(x, y, x + nativeWindowSettings.Size.X, y + nativeWindowSettings.Size.Y);*/
            return GameWindow;
        }
    }
}
